using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 土地的空白状态 也是初始状态 需要根据数据恢复跳转到最开始的状态
/// </summary>
public class SoilIdleStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Idle;

    protected override eAction SupportAction => eAction.Hoeing;

    protected override float AutoEnterNextStatusTime => 0;

    protected override void OnEnter(IFsm<SoilStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        StatusCtrl.SoilEvent.MsgInitStatus += OnMsgInitStatus;
    }

    protected override void OnLeave(IFsm<SoilStatusCtrl> fsm, bool isShutdown)
    {
        StatusCtrl.SoilEvent.MsgInitStatus -= OnMsgInitStatus;

        base.OnLeave(fsm, isShutdown);
    }

    //收到数据恢复初始化状态的消息
    private void OnMsgInitStatus(SoilSaveData soilSaveData)
    {
        SoilData.SetSaveData(soilSaveData);

        if (soilSaveData.SoilStatus == eSoilStatus.Idle)//如果初始化也是idle状态就不用切换了
        {
            return;
        }

        OwnerFsm.SetData<VarBoolean>(SoilStatusDataName.IS_INIT_STATUS, true);//告知下一个状态进入是初始化进入
        ChangeState(soilSaveData.SoilStatus);
    }

    protected override void OnExecuteHomeAction(eAction action, object actionData)
    {
        base.OnExecuteHomeAction(action, actionData);

        ChangeState(eSoilStatus.Loose);
    }
}