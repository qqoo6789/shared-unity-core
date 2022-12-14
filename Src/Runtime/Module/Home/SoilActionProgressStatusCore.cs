using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 有动作进度的土地状态基类
/// </summary>
public abstract class SoilActionProgressStatusCore : SoilStatusCore
{
    /// <summary>
    /// 当前动作的效果值绝对值
    /// </summary>
    /// <value></value>
    public int CurActionEffectValue
    {
        get
        {
            if (SoilData.SaveData.LastActionEffectValue <= 0)
            {
                return 0;
            }

            float costTime = TimeUtil.MS2S * (GetNowTimestamp() - SoilData.SaveData.LastActionStamp);//距离上次动作已经过去的时间 秒
            float lostValue = costTime * _lostActionEffectValueSpeed;
            int remain = SoilData.SaveData.LastActionEffectValue - (int)lostValue;
            return Mathf.Clamp(remain, 0, _needActionEffectValue);
        }
    }

    /// <summary>
    /// 当前动作的效果进度 0~1
    /// </summary>
    /// <returns></returns>
    public float ActionProgress => Mathf.Clamp((float)CurActionEffectValue / _needActionEffectValue, 0, 1);

    private int _needActionEffectValue;
    private int _lostActionEffectValueSpeed;//每秒自动减少的效果值

    /// <summary>
    /// 指定需要进度的动作类型 目前只允许一个动作 主要是用来校验报错使用
    /// </summary>
    /// <value></value>
    protected abstract HomeDefine.eAction NeedEffectValueActionType { get; }
    /// <summary>
    /// 需要的动作效果值最大值
    /// </summary>
    /// <value></value>
    protected abstract int NeedActionEffectValue { get; }
    /// <summary>
    /// 动作效果值自然流逝速度 /秒
    /// </summary>
    /// <value></value>
    protected abstract int LostActionEffectValueSpeed { get; }

    protected override void OnEnter(IFsm<SoilStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        _needActionEffectValue = NeedActionEffectValue;
        _lostActionEffectValueSpeed = LostActionEffectValueSpeed;
    }

    protected sealed override void OnExecuteHomeAction(HomeDefine.eAction action, int effectValue, object actionData)
    {
        base.OnExecuteHomeAction(action, effectValue, actionData);

        if (NeedEffectValueActionType != action)
        {
            OnActionComplete(action, actionData);
            return;
        }

        if (effectValue == 0)
        {
            Log.Error("需要进度的动作必须发效果值");
            return;
        }

        int curRemain = CurActionEffectValue;
        curRemain += effectValue;

        curRemain = Mathf.Clamp(curRemain, 0, _needActionEffectValue);

        SoilData.SaveData.LastActionEffectValue = curRemain;
        SoilData.SaveData.LastActionStamp = GetNowTimestamp();

        if (curRemain >= _needActionEffectValue)
        {
            OnActionComplete(action, actionData);
        }
    }

    /// <summary>
    /// 动作完成了  也许是进度动作 也许是其他一次性动作 不过都是自身支持的动作才会回调
    /// </summary>
    /// <param name="action">完成的动作</param>
    /// <param name="actionData">动作附带参数 比如种子cid</param>
    /// 
    protected abstract void OnActionComplete(HomeDefine.eAction action, object actionData);
}