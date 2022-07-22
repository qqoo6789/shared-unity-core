using GameFramework.Fsm;

/// <summary>
/// 闲置状态通用状态基类
/// </summary>
public class IdleStatusCore : EntityStatusCore
{
    public static new string Name = "idle";

    public override string StatusName => Name;

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
        ChangeState(OwnerFsm, MoveStatusCore.Name);
    }
}