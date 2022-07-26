using System;
using GameFramework.Fsm;

/// <summary>
/// 移动状态通用状态基类
/// </summary>
public abstract class MoveStatusCore : EntityStatusCore
{
    /// <summary>
    /// 子类确定Idle状态类型
    /// </summary>
    /// <value></value>
    protected virtual Type IdleStatusType => typeof(IdleStatusCore);

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        StatusCtrl.EntityEvent.StopMove += OnStopMove;
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        StatusCtrl.EntityEvent.StopMove -= OnStopMove;

        base.OnLeave(fsm, isShutdown);
    }

    private void OnStopMove()
    {
        ChangeState(OwnerFsm, IdleStatusType);
    }
}