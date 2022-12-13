using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using static HomeDefine;
/// <summary>
/// 土地上的各种状态的基类 实体状态类直接可以在内部任意时间切换状态 不要求再update 切换时取OwnerFsm
/// </summary>
public abstract class SoilStatusCore : ComponentStatusCore<SoilStatusCtrl>
{
    private static TimeUtil.DelegateGetTimeStamp s_getNowTimeStamp = TimeUtil.GetTimeStamp;
    /// <summary>
    /// 客户端需要设置这个时间戳方法为获取服务器时间 TODO:
    /// </summary>
    /// <param name="getNowTimeStampFunc"></param>
    public static void SetGetNowTimestampFunc(TimeUtil.DelegateGetTimeStamp getNowTimeStampFunc)
    {
        s_getNowTimeStamp = getNowTimeStampFunc;
    }

    protected SoilData SoilData { get; private set; }
    /// <summary>
    /// 当前状态枚举标记
    /// </summary>
    /// <value></value>
    public abstract eSoilStatus StatusFlag { get; }

    /// <summary>
    /// 当前状态支持的家园动作 可以是多个动作二进制
    /// </summary>
    /// <value></value>
    protected abstract eAction SupportAction { get; }
    /// <summary>
    /// 设置支持动作时必选 子类实现具体的动作逻辑 如果不支持该动作则不会触发该事件
    /// </summary>
    /// <param name="action">当前执行的动作</param>
    protected virtual void OnExecuteHomeAction(eAction action) { }

    /// <summary>
    /// 自动进入下一个状态的时间 等于0不会自动进入 秒
    /// </summary>
    /// <value></value>
    protected abstract float AutoEnterNextStatusTime { get; }
    /// <summary>
    /// 设置自动进入时必选 自动进入下一个的具体状态
    /// </summary>
    /// <value></value>
    protected virtual eSoilStatus AutoEnterNextStatusFlag { get; }

    public sealed override string StatusName => StatusFlag.ToString();

    private bool _isTimerNextStatus;//有开始定时去下个状态

    protected override void OnEnter(IFsm<SoilStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        SoilData = StatusCtrl.GetComponent<SoilData>();
        SoilData.SaveData.SoilStatus = StatusFlag;

        if (SupportAction != eAction.None)
        {
            StatusCtrl.SoilEvent.MsgExecuteAction += OnMsgExecuteAction;
        }

        float nextStatusRemainTime = 0;
        //如果是初始化状态就保留初始化数据中的进入时间戳 否则都要重新设置
        if (fsm.HasData(SoilStatusDataName.IS_INIT_STATUS))
        {
            if (AutoEnterNextStatusTime > 0)
            {
                float initRemainTime = (s_getNowTimeStamp() - SoilData.SaveData.StatusStartStamp) * TimeUtil.MS2S;
                if (initRemainTime < 0)//已经到了下一个状态的时间  直接跳到下一个状态
                {
                    SoilData.SaveData.StatusStartStamp += (long)(AutoEnterNextStatusTime * TimeUtil.S2MS);//下个状态的开始时间会延后本次自动切换的时间
                    ChangeState(AutoEnterNextStatusFlag);
                    //不去掉出初始化标记 让下个状态继续走初始化状态
                }
                else//还没到下一个状态的时间 继续等待 并删掉初始化标记
                {
                    nextStatusRemainTime = initRemainTime;
                    _ = fsm.RemoveData(SoilStatusDataName.IS_INIT_STATUS);
                }
            }
            else
            {
                _ = fsm.RemoveData(SoilStatusDataName.IS_INIT_STATUS);
            }
        }
        else
        {
            ResetEnterStatusStamp();

            if (AutoEnterNextStatusTime > 0)
            {
                nextStatusRemainTime = AutoEnterNextStatusTime;
            }
        }

        if (nextStatusRemainTime > 0)
        {
            _isTimerNextStatus = true;
            TimerMgr.AddTimer(GetHashCode(), nextStatusRemainTime * TimeUtil.MS2S, OnAutoEnterNextStatus);
        }
    }

    protected override void OnLeave(IFsm<SoilStatusCtrl> fsm, bool isShutdown)
    {
        SoilData = null;

        if (SupportAction != eAction.None)
        {
            StatusCtrl.SoilEvent.MsgExecuteAction -= OnMsgExecuteAction;
        }

        if (_isTimerNextStatus)
        {
            _ = TimerMgr.RemoveTimer(GetHashCode());
            _isTimerNextStatus = false;
        }

        base.OnLeave(fsm, isShutdown);
    }

    //自动进入下一个状态计时到了
    private void OnAutoEnterNextStatus()
    {
        ChangeState(AutoEnterNextStatusFlag);
    }

    private void OnMsgExecuteAction(eAction action)
    {
        if (!CheckSupportAction(action))
        {
            Log.Error("当前状态不支持该动作，不应该触发该事件");
            return;
        }

        OnExecuteHomeAction(action);
    }

    /// <summary>
    /// 检查是否支持某个家园动作
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool CheckSupportAction(eAction action)
    {
        return (SupportAction & action) != 0;
    }

    /// <summary>
    /// 统一使用突然状态枚举来切换状态 一一对应
    /// </summary>
    /// <param name="status"></param>
    protected void ChangeState(eSoilStatus status)
    {
        ChangeState(OwnerFsm, status.ToString());
    }

    /// <summary>
    /// 重置进入状态时间戳
    /// </summary>
    private void ResetEnterStatusStamp()
    {
        SoilData.SaveData.StatusStartStamp = s_getNowTimeStamp();
    }
}