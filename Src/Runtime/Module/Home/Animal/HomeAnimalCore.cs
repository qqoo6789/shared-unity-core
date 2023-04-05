using UnityEngine;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 家园动物core
/// </summary>
public abstract class HomeAnimalCore : EntityBaseComponent, ICollectResourceCore
{
    public ulong Id => Data.AnimalId;

    public eResourceType ResourceType => eResourceType.Animal;

    public GameObject LogicRoot => RefEntity.EntityRoot;

    public Vector3 Position => RefEntity.Position;

    public int Lv => Data.DRMonster.Lv;

    public eAction SupportAction { get; set; } = eAction.Appease | eAction.LastWords;

    private eAction _harvestAction = eAction.None;//收获动作

    /// <summary>
    /// 动物数据
    /// </summary>
    /// <value></value>
    public AnimalDataCore Data { get; private set; }

    /// <summary>
    /// 自动收获的掉落实体
    /// </summary>
    protected GameObject DropEntity { get; private set; }

    protected virtual void Awake()
    {
        Data = gameObject.AddComponent<AnimalDataCore>();
    }

    protected virtual void Start()
    {
        if (Data.DRMonster != null)
        {
            _harvestAction = TableUtil.ToHomeAction(Data.DRMonster.HarvestAction);
            SupportAction |= _harvestAction;//收获动作添加到支持列表
        }
        else
        {
            Log.Error($"动物配置表为空 cid:{Data.BaseData.Cid}");
        }

        InitStatus();
    }

    protected virtual void OnDestroy()
    {
        if (DropEntity != null)
        {
            Destroy(DropEntity);
            DropEntity = null;
        }
    }

    private void InitStatus()
    {
        if (Data.SaveData.IsDead)
        {
            EnterAnimalDeadStatus(true);
        }
        else
        {
            if (Data.IsCanHarvest && !Data.DRMonster.AutoHarvest)
            {
                OnEnterHarvestStatus(true);
            }
            else
            {
                gameObject.GetComponent<HomeActionProgressData>().StartProgressAction(eAction.Appease, ANIMAL_APPEASE_ACTION_MAX_PROGRESS);
            }
        }
    }

    protected virtual void Update()
    {
        TickHunger();

        TickHarvest();
    }

    private void TickHarvest()
    {
        if (Data.SaveData.HungerProgress > 0)
        {
            bool oldCanHarvest = Data.IsCanHarvest;
            float addHarvestProgress = Time.deltaTime / Data.DRMonster.HarvestTime * 100;//配置的是多少秒收获能收获 这个收获进度时百分比0~100
            Data.SaveData.SetHarvestProgress(Data.SaveData.HarvestProgress + addHarvestProgress);
            if (Data.IsCanHarvest != oldCanHarvest && Data.IsCanHarvest)
            {
                if (!Data.DRMonster.AutoHarvest)
                {
                    OnEnterHarvestStatus(false);
                }
            }
        }
    }

    /// <summary>
    /// 饥饿值变化
    /// </summary>
    private void TickHunger()
    {
        AnimalSaveData saveData = Data.SaveData;
        if (saveData.HungerProgress > 0)
        {
            saveData.HungerProgress -= Data.DRMonster.HungerSpeed * Time.deltaTime;

            if (saveData.HungerProgress <= 0)//开始完全饥饿
            {
                saveData.HungerProgress = 0;
                saveData.LastCompleteHungerStamp = TimeUtil.GetTimeStamp();
            }
            else
            {
                saveData.LastCompleteHungerStamp = 0;
            }
        }
        else
        {
            if (saveData.LastCompleteHungerStamp > 0)
            {
                if ((TimeUtil.GetTimeStamp() - saveData.LastCompleteHungerStamp) * TimeUtil.MS2S >= ANIMAL_HUNGER_DEAD_TIME)
                {
                    saveData.LastCompleteHungerStamp = 0;
                    OnTimerHungerDead();
                }
            }
        }
    }

    /// <summary>
    /// 检测到饥饿到了饿死的时候 客户端不处理饿死状态 由服务器主动告知
    /// </summary>
    protected virtual void OnTimerHungerDead()
    {
    }

    /// <summary>
    /// 吃下食物后设置饥饿值
    /// </summary>
    public virtual void EatenSetHunger(float progress)
    {
        Data.SaveData.HungerProgress = progress;
    }

    public bool CheckSupportAction(eAction action)
    {

        if ((SupportAction & action) == 0)
        {
            return false;
        }

        if (Data.SaveData.IsDead)//死了
        {
            if (action == eAction.LastWords)
            {
                return true;
            }

            return false;
        }

        //活着
        if (action == eAction.LastWords)
        {
            return false;
        }

        if (action == eAction.Appease)
        {
            return !Data.IsCanHarvest;//不在收获状态下可以随时安抚 不一定有效果而已
        }

        //收获动作
        return Data.IsCanHarvest;
    }

    public void ExecuteAction(eAction action, int toolCid, bool itemValid, int extraWateringNum, int skillId)
    {
        if (action == eAction.LastWords)//触碰遗言
        {
            OnExecuteLastWords();
        }
        else if (action == eAction.Appease)//安抚
        {
            OnExecuteAppease(Data.SaveData.IsComforted == false);
        }
        else if (action == _harvestAction)//收获 能执行的都是手动收货的
        {
            OnExecuteHarvest();
        }
    }

    public virtual void ExecuteProgress(eAction targetCurAction, int skillId)
    {

    }

    /// <summary>
    /// 进入收获状态 只有手动收获才会触发
    /// </summary>
    protected virtual void OnEnterHarvestStatus(bool isInit)
    {
        gameObject.GetComponent<HomeActionProgressData>().StartProgressAction(_harvestAction, ANIMAL_HARVEST_ACTION_MAX_PROGRESS);
    }

    /// <summary>
    /// 进入动物死亡状态
    /// </summary>
    public virtual void EnterAnimalDeadStatus(bool isInit)
    {
        if (!isInit)
        {
            gameObject.GetComponent<HomeActionProgressData>().EndProgressAction();
            Data.SaveData.IsDead = true;
        }

        RefEntity.BattleDataCore.SetHP(0);

        ClearDropProduct();//TODO: home 暂时这样
    }

    /// <summary>
    /// 死亡后触发遗言
    /// </summary>
    protected virtual void OnExecuteLastWords() { }

    /// <summary>
    /// 抚摸操作
    /// </summary>
    /// <param name="appeaseValid">是否有效抚摸 无效代表一个成熟阶段重复抚摸</param>
    protected virtual void OnExecuteAppease(bool appeaseValid)
    {
        if (appeaseValid)
        {
            Data.SaveData.IsComforted = true;
            Data.BaseData.Favorability += ANIMAL_APPEASE_ADD_VALUE;
            Data.MsgFavorabilityChanged?.Invoke(Data.BaseData.Favorability);
        }
        gameObject.GetComponent<HomeActionProgressData>().StartProgressAction(eAction.Appease, ANIMAL_APPEASE_ACTION_MAX_PROGRESS);
    }

    /// <summary>
    /// 主动操作的收获
    /// </summary>
    protected virtual void OnExecuteHarvest()
    {
        if (Data.DRMonster.AutoHarvest)
        {
            Log.Error($"动物配置表错误 自动收获的动物不能手动收获 cid:{Data.BaseData.Cid}");
            return;
        }

        Data.SaveData.SetHarvestProgress(0);
        Data.SaveData.IsComforted = false;
        gameObject.GetComponent<HomeActionProgressData>().StartProgressAction(eAction.Appease, ANIMAL_APPEASE_ACTION_MAX_PROGRESS);
    }

    /// <summary>
    /// 创建一个掉落的产品
    /// </summary>
    /// <param name="productSaveData"></param>
    /// <typeparam name="TDrop"></typeparam>
    protected void CreateDropProduct<TDrop>(AnimalProductSaveData productSaveData, Transform parent, bool isInit) where TDrop : AnimalDropCore
    {
        if (productSaveData == null)
        {
            Log.Error($"动物收获的产品数据为空 cid:{Data.BaseData.Cid}");
            return;
        }

        if (Data.SaveData.ProductSaveData != null && !isInit)
        {
            Log.Error($"动物收获的产品数据不为空 cid:{Data.BaseData.Cid}");
            return;
        }

        if (DropEntity != null)
        {
            Log.Error($"动物掉落实体不为空 cid:{Data.BaseData.Cid}");
        }

        if (productSaveData.ProductId == 0)
        {
            Log.Error($"动物掉落产品数据错误 id==0  product :{productSaveData}");
            return;
        }

        Data.SaveData.ProductSaveData = productSaveData;
        DropEntity = GameObjectUtil.CreateGameObject($"{Data.BaseData.AnimalId}_{productSaveData.ProductId}", parent);
        DropEntity.transform.position = NetUtilCore.Vector3FromNet(productSaveData.Pos);
        AnimalDropCore drop = DropEntity.AddComponent<TDrop>();
        drop.InitAnimalDrop(productSaveData, Data.AnimalId);
    }

    /// <summary>
    /// 清除掉落的产品
    /// </summary>
    public void ClearDropProduct()
    {
        if (DropEntity != null)
        {
            Destroy(DropEntity);
            DropEntity = null;
        }
        Data.SaveData.ProductSaveData = null;
    }
}