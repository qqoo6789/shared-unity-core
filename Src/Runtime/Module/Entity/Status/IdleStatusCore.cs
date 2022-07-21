using System;
using GameFramework.Fsm;

/// <summary>
/// 闲置状态通用状态基类
/// </summary>
public abstract class IdleStatusCore : EntityStatusCore
{
    /// <summary>
    /// 子类确定Move状态类型
    /// </summary>
    /// <returns></returns>
    protected virtual Type MoveStatusType => typeof(MoveStatusCore);

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        StatusCtrl.EntityEvent.StartMove += OnStartMove;
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        StatusCtrl.EntityEvent.StartMove -= OnStartMove;

        base.OnLeave(fsm, isShutdown);
    }

    private void OnStartMove()
    {
        ChangeState(OwnerFsm, MoveStatusType);
    }
}