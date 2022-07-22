using GameFramework.Fsm;

/// <summary>
/// 移动状态通用状态基类
/// </summary>
public abstract class MoveStatusCore : EntityStatusCore
{
    public static new string Name = "move";

    public override string StatusName => Name;

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
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }
}