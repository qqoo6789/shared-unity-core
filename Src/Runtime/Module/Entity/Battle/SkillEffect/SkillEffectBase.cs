/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:08:06
 * @Description: 技能效果球基础, 用了引用池，记住继承Clear清除数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SkillEffectBase.cs
 * 
 */
using System;
using GameFramework;
using UnityEngine;

public class SkillEffectBase : IReference
{
    /// <summary>
    /// 持续时间 小于零代表一致持续  0代表立即执行销毁  大于0即到时自动销毁
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
    /// 效果是否重复叠加
    /// </summary>
    public virtual bool IsRepeat => false;
    /// <summary>
    /// 效果数据
    /// </summary>
    public object EffectData { get; private set; }

    /// <summary>
    /// 宿主对象
    /// </summary>
    public GameObject RefOwner { get; private set; }
    /// <summary>
    /// 设置效果数据
    /// </summary>
    /// <param name="skillID">技能ID</param>
    /// <param name="效果ID">是否循环</param>
    /// <param name="fromID">技能释放者ID</param>
    /// <param name="targetID">技能接受者ID</param>
    /// <param name="duration">持续时间</param>
    public virtual void SetData(int skillID, int effectID, long fromID, long targetID, int duration)
    {
        SkillID = skillID;
        EffectID = effectID;
        FromID = fromID;
        TargetID = targetID;
        Duration = duration;

    }

    /// <summary>
    /// 设置效果数据
    /// </summary>
    /// <param name="data"效果数据</param>
    public virtual void SetEffectData(object data)
    {
        EffectData = data;
    }

    /// <summary>
    /// 创建技能效果数据 子类复写
    /// </summary>
    /// <param name="parameters">参数数组</param>
    /// <param name="fromEntity">发送方</param>
    /// <param name="targetEntity">接受方</param>
    public virtual object CreateEffectData(int[] parameters, EntityBase fromEntity, EntityBase targetEntity)
    {
        return null;
    }

    public virtual void Clear()
    {
        Duration = 0;
        FromID = 0;
        TargetID = 0;
        DestroyTimestamp = 0;
        EffectID = 0;
        SkillID = 0;
        RefOwner = null;
        EffectData = null;
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
    /// 效果球不允许重复时，效果球不会重复添加，而会调用OnRefreshRepeat去重新刷新效果，具体如何刷新，根据你实现的效果而定
    /// </summary>
    public virtual void OnRefreshRepeat(SkillEffectBase newEffect)
    {
        DestroyTimestamp = newEffect.DestroyTimestamp;
    }

    //添加效果
    public void AddEffect(GameObject owner)
    {
        if (RefOwner)
        {
            return;
        }
        RefOwner = owner;
        OnAdd();
    }

    //删除效果
    public void RemoveEffect()
    {
        if (RefOwner == null)
        {
            return;
        }

        OnRemove();
        RefOwner = null;
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