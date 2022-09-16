/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:08:06
 * @Description: 技能效果球基础, 用了引用池，记住继承Clear清除数据
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SkillEffectBase.cs
 * 
 */
using System;
using GameFramework;
using MelandGame3;

public class SkillEffectBase : IReference
{
    /// <summary>
    /// 持续时间 小于零代表一致持续  0代表立即执行销毁  大于0即到时自动销毁 单位ms
    /// </summary>
    public int Duration { get; private set; }

    /// <summary>
    /// 技能释放者ID
    /// </summary>
    public long FromID { get; private set; }

    /// <summary>
    /// 技能接受者ID
    /// </summary>
    public long TargetID { get; private set; }

    /// <summary>
    /// 效果销毁时间 ms
    /// </summary>
    public long DestroyTimestamp { get; set; }
    /// <summary>
    /// 技能ID
    /// </summary>
    public int SkillID { get; private set; }
    /// <summary>
    /// 效果ID
    /// </summary>
    public int EffectID { get; private set; }
    /// <summary>
    /// 效果配置
    /// </summary>
    public DRSkillEffect EffectCfg { get; private set; }
    /// <summary>
    /// 效果是否重复叠加
    /// </summary>
    public virtual bool IsRepeat => false;
    /// <summary>
    /// 效果数据
    /// </summary>
    public DamageEffect EffectData { get; private set; }

    /// <summary>
    /// 宿主对象
    /// </summary>
    public EntityBase RefEntity { get; private set; }
    /// <summary>
    /// 设置效果数据
    /// </summary>
    /// <param name="skillID">技能ID</param>
    /// <param name="效果ID">是否循环</param>
    /// <param name="fromID">技能释放者ID</param>
    /// <param name="targetID">技能接受者ID</param>
    /// <param name="duration">持续时间</param>
    public virtual void SetData(int skillID, DRSkillEffect effectCfg, long fromID, long targetID, int duration)
    {
        SkillID = skillID;
        EffectID = effectCfg.Id;
        EffectCfg = effectCfg;
        FromID = fromID;
        TargetID = targetID;
        Duration = duration;

    }

    /// <summary>
    /// 设置效果数据
    /// </summary>
    /// <param name="data"效果数据</param>
    public virtual void SetEffectData(DamageEffect data)
    {
        EffectData = data;
    }
    private EntityEvent _entityEvent;//缓存实体上的事件组件 节省性能
    public EntityEvent EntityEvent
    {
        get
        {
            if (_entityEvent == null)
            {
                _entityEvent = RefEntity.GetComponent<EntityEvent>();
            }
            return _entityEvent;
        }
    }

    /// <summary>
    /// 检测能否应用效果
    /// </summary>
    /// <param name="fromEntity">发送方</param>
    /// <param name="targetEntity">接受方</param>
    public virtual bool CheckApplyEffect(EntityBase fromEntity, EntityBase targetEntity)
    {
        return true;
    }

    /// <summary>
    /// 创建技能效果数据 子类复写
    /// </summary>
    /// <param name="fromEntity">发送方</param>
    /// <param name="targetEntity">接受方</param>
    /// <param name="skillDir">技能方向</param>
    public virtual DamageEffect CreateEffectData(EntityBase fromEntity, EntityBase targetEntity, UnityEngine.Vector3 skillDir)
    {
        return new DamageEffect();//现在网络传递时通过DamageEffect传递 id 这个不能为空
    }

    public virtual void Clear()
    {
        Duration = 0;
        FromID = 0;
        TargetID = 0;
        DestroyTimestamp = 0;
        EffectID = 0;
        EffectCfg = null;
        SkillID = 0;
        RefEntity = null;
        EffectData = null;
        _entityEvent = null;
    }
    /// <summary>
    /// 添加后执行第一次
    /// </summary>
    public virtual void Start()
    {

    }
    /// <summary>
    /// 刷新效果
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// 效果被添加
    /// </summary>
    public virtual void OnAdd()
    {
    }

    /// <summary>
    /// 效果被移除
    /// </summary>
    public virtual void OnRemove()
    {

    }

    /// <summary>
    /// 效果球不允许重复时，新的效果球会覆盖老的，会调用OnRefreshRepeat用老的效果去重新刷新新效果，具体如何刷新，根据你实现的效果而定
    /// </summary>
    public virtual void OnRefreshRepeat(SkillEffectBase oldEffect)
    {
        //
    }

    //添加效果
    public void AddEffect(EntityBase owner)
    {
        if (RefEntity != null)
        {
            return;
        }
        RefEntity = owner;
        OnAdd();
    }

    //删除效果
    public void RemoveEffect()
    {
        if (RefEntity == null)
        {
            return;
        }

        OnRemove();
        RefEntity = null;
    }

    public static SkillEffectBase Create(Type skillEffectClass)
    {
        SkillEffectBase effect = ReferencePool.Acquire(skillEffectClass) as SkillEffectBase;
        return effect;
    }

    /// <summary>
    /// 效果被销毁
    /// </summary>
    public void Dispose()
    {
        RemoveEffect();
        ReferencePool.Release(this);
    }
}