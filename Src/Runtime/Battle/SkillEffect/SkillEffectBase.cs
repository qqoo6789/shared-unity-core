/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:08:06
 * @Description: 技能效果球基础, 用了引用池，记住继承Clear清除数据
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Battle/SkillEffect/SkillEffectBase.cs
 * 
 */
using GameFramework;
using UnityEngine;

public class SkillEffectBase : IReference
{
    protected int _duration = 0;
    /// <summary>
    /// 持续时间 小于零代表一致持续  0代表立即执行销毁  大于0即到时自动销毁
    /// </summary>
    public int Duration => _duration;
    protected string _fromID;//技能施法者 有可能为null  也可能不在视野内
    /// <summary>
    /// 技能释放者ID
    /// </summary>
    public string FromID => _fromID;
    protected string _targetID;//技能接收者 一般就是最后应用效果的实体
    /// <summary>
    /// 技能接受者ID
    /// </summary>
    public string TargetID => _targetID;
    protected long _destroyTimestamp;
    /// <summary>
    /// 效果销毁时间 ms
    /// </summary>
    public long DestroyTimestamp { set; get; }
    protected int _skillID;
    /// <summary>
    /// 技能ID
    /// </summary>
    public int SkillID => _skillID;
    protected int _effectID;
    /// <summary>
    /// 效果ID
    /// </summary>
    public int EffectID => _effectID;
    /// <summary>
    /// 效果是否重复叠加
    /// </summary>
    public virtual bool IsRepeat => false;
    private GameObject _refOwner;
    /// <summary>
    /// 宿主对象
    /// </summary>
    public GameObject RefOwner => _refOwner;
    /// <summary>
    /// 设置效果数据
    /// </summary>
    /// <param name="skillID">技能ID</param>
    /// <param name="效果ID">是否循环</param>
    /// <param name="fromID">技能释放者ID</param>
    /// <param name="targetID">技能接受者ID</param>
    /// <param name="duration">持续时间</param>
    public virtual void SetData(int skillID, int effectID, string fromID, string targetID, int duration)
    {
        _skillID = skillID;
        _effectID = effectID;
        _fromID = fromID;
        _targetID = targetID;
        _duration = duration;

    }
    /// <summary>
    /// 设置自定义数据
    /// </summary>
    /// <param name="data">自定义数据</param>
    public virtual void SetCustomData(System.Object data)
    {
        //
    }
    public virtual void Clear()
    {
        _duration = 0;
        _fromID = null;
        _targetID = null;
        _destroyTimestamp = 0;
        _effectID = 0;
        _skillID = 0;
        _refOwner = null;
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
        _destroyTimestamp = newEffect.DestroyTimestamp;
    }

    //添加效果
    public void AddEffect(GameObject owner)
    {
        if (_refOwner)
        {
            return;
        }
        _refOwner = owner;
        OnAdd();
    }

    //删除效果
    public void RemoveEffect()
    {
        if (_refOwner == null)
        {
            return;
        }

        OnRemove();
        _refOwner = null;
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