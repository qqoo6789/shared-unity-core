using System;
using GameFramework.Fsm;

/// <summary>
/// 土地的空白状态 也是初始状态 需要根据数据恢复跳转到最开始的状态
/// </summary>
public class SoilIdleStatusCore : SoilStatusCore
{
    public static new string Name = "soilIdle";

    public override string StatusName => Name;

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
        throw new NotImplementedException();
    }
}