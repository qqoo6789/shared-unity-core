using GameFramework.Fsm;

/// <summary>
/// 闲置状态通用状态基类
/// </summary>
public class IdleStatusCore : EntityStatusCore
{
    public static new string Name = "idle";

    public override string StatusName => Name;

    private EntityInputData _inputData;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        _inputData = StatusCtrl.GetComponent<EntityInputData>();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        _inputData = null;

        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        if (_inputData.InputMoveDirection != null)
        {
            ChangeState(fsm, DirectionMoveStatusCore.Name);
        }
        else if (_inputData.InputMovePath != null)
        {
            ChangeState(fsm, PathMoveStatusCore.Name);
        }
    }
}