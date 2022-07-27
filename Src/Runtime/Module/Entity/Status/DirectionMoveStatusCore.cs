using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 方向移动状态
/// </summary>
public class DirectionMoveStatusCore : ListenEventStatusCore
{
    public static new string Name = "directionMove";

    public override string StatusName => Name;

    private EntityInputData _inputData;
    private DirectionMove _directionMove;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        _inputData = StatusCtrl.GetComponent<EntityInputData>();
        if (StatusCtrl.TryGetComponent<DirectionMove>(out _directionMove))
        {
            _directionMove.enabled = true;//开启方向移动组件去移动
            _directionMove.MoveSpeed = StatusCtrl.GetComponent<EntityMoveData>().Speed;
            return;
        }
        else
        {
            Log.Error($"direction move status enter,not find PathMove,name={StatusCtrl.gameObject.name}");
        }
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        _inputData = null;

        if (_directionMove)
        {
            _directionMove.enabled = false;
            _directionMove = null;
        }

        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        if (_inputData.InputMoveDirection == null)
        {
            ChangeState(fsm, IdleStatusCore.Name);
        }
    }


    protected override void AddEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease += OnInputSkillRelease;
    }

    protected override void RemoveEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease -= OnInputSkillRelease;
    }

    private void OnInputSkillRelease(int skillID)
    {
        OwnerFsm.SetData<VarInt32>(StatusDataDefine.SKILL_ID, skillID);
        ChangeState(OwnerFsm, SkillForwardStatusCore.Name);
    }
}