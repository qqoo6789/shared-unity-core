/** 
 * @Author XQ
 * @Date 2022-08-10 16:27:01
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/DirectionMoveStatusCore.cs
 */
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 方向移动状态
/// </summary>
public class DirectionMoveStatusCore : ListenEventStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name = "directionMove";

    public override string StatusName => Name;

    private EntityInputData _inputData;
    private DirectionMove _directionMove;
    private EntityBattleDataCore _battleData;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        _inputData = StatusCtrl.GetComponent<EntityInputData>();
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();
        if (StatusCtrl.TryGetComponent(out _directionMove))
        {
            _directionMove.ChangeMoveStatus(true);
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
        _battleData = null;

        if (_directionMove)
        {
            _directionMove.ChangeMoveStatus(false);
            _directionMove = null;
        }

        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (_battleData && !_battleData.IsLive())
        {
            ChangeState(fsm, DeathStatusCore.Name);
            return;
        }
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
        if (!CheckCanSkill())
        {
            return;
        }

        OwnerFsm.SetData<VarInt32>(StatusDataDefine.SKILL_ID, skillID);
        ChangeState(OwnerFsm, SkillAccumulateStatusCore.Name);
    }

    public bool CheckCanMove()
    {
        return true;
    }

    public bool CheckCanSkill()
    {
        return true;
    }
}