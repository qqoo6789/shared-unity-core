using UnityEngine;

/// <summary>
/// 家园动物core
/// </summary>
public abstract class HomeAnimalCore : EntityBaseComponent
{
    /// <summary>
    /// 动物数据
    /// </summary>
    /// <value></value>
    public AnimalDataCore Data { get; private set; }

    protected virtual void Awake()
    {
        Data = gameObject.AddComponent<AnimalDataCore>();
    }

    protected virtual void Update()
    {
        TickHunger();
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
                if ((TimeUtil.GetTimeStamp() - saveData.LastCompleteHungerStamp) * TimeUtil.MS2S >= HomeDefine.ANIM_HUNGER_DEAD_TIME)
                {
                    saveData.LastCompleteHungerStamp = 0;
                    OnAnimDead();
                }
            }
        }
    }

    /// <summary>
    /// 动物死亡
    /// </summary>
    protected virtual void OnAnimDead()
    {
    }

    /// <summary>
    /// 吃下食物后设置饥饿值
    /// </summary>
    protected virtual void EatenSetHunger(float progress)
    {
        Data.SaveData.HungerProgress = progress;
    }
}