using GameFramework.Fsm;
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

    public sealed override string StatusName => StatusFlag.ToString();

    protected override void OnEnter(IFsm<SoilStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        SoilData = StatusCtrl.GetComponent<SoilData>();
        SoilData.SaveData.SoilStatus = StatusFlag;

        //如果是初始化状态就保留初始化数据中的进入时间戳 否则都要重新设置
        if (fsm.HasData(SoilStatusDataName.IS_INIT_STATUS))
        {
            _ = fsm.RemoveData(SoilStatusDataName.IS_INIT_STATUS);
        }
        else
        {
            ResetEnterStatusStamp();
        }
    }

    protected override void OnLeave(IFsm<SoilStatusCtrl> fsm, bool isShutdown)
    {
        SoilData = null;

        base.OnLeave(fsm, isShutdown);
    }

    protected void ChangeState(eSoilStatus status)
    {
        ChangeState(OwnerFsm, status.ToString());
    }

    /// <summary>
    /// 重置进入状态时间戳
    /// </summary>
    private void ResetEnterStatusStamp()
    {
        SoilData.SaveData.CurStatusStartStamp = s_getNowTimeStamp();
    }
}