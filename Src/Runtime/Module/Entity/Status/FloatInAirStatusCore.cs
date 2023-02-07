using GameFramework.Fsm;

/// <summary>
/// 在空中的浮空状态
/// </summary>
public class FloatInAirStatusCore : ListenEventStatusCore
{
    public static new string Name = "floatInAir";

    public override string StatusName => Name;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        if (RefEntityIsDead())
        {
            ChangeState(fsm, DeathStatusCore.Name);
            return;
        }

        if (StatusCtrl.RefEntity.MoveData != null && StatusCtrl.RefEntity.MoveData.IsGrounded)
        {
            ChangeState(fsm, IdleStatusCore.Name);
        }
    }
}