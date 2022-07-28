using UnityEngine;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 路径移动状态
/// </summary>
public class PathMoveStatusCore : ListenEventStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name = "pathMove";

    public override string StatusName => Name;

    private EntityInputData _inputData;
    private PathMove _pathMove;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        _inputData = StatusCtrl.GetComponent<EntityInputData>();

        Vector3[] path = _inputData.InputMovePath;
        if (path == null || path.Length == 0)
        {
            Log.Error($"path move status enter,not find path ,data empth={path == null}");
            ChangeState(fsm, IdleStatusCore.Name);
            return;
        }

        if (!StatusCtrl.TryGetComponent<PathMove>(out _pathMove))
        {
            Log.Error($"path move status enter,not find PathMove,name={StatusCtrl.gameObject.name}");
            ChangeState(fsm, IdleStatusCore.Name);
            return;
        }

        _pathMove.enabled = true;
        _pathMove.SetMoveSpeed(StatusCtrl.GetComponent<EntityMoveData>().Speed);
        _pathMove.MovePath(_inputData.InputMovePath, OnMoveFinish);
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        if (_inputData != null)
        {
            _inputData.SetInputMovePath(null, false);
            _inputData = null;
        }

        if (_pathMove != null)
        {
            _pathMove.StopMove();
            _pathMove.enabled = false;
            _pathMove = null;
        }

        base.OnLeave(fsm, isShutdown);
    }

    protected override void AddEvent(EntityEvent entityEvent)
    {
        entityEvent.InputMovePathChanged += OnPathChanged;
        entityEvent.InputSkillRelease += OnInputSkillRelease;
    }

    protected override void RemoveEvent(EntityEvent entityEvent)
    {
        entityEvent.InputMovePathChanged -= OnPathChanged;
        entityEvent.InputSkillRelease -= OnInputSkillRelease;
    }

    private void OnMoveFinish(PathMove pathMove)
    {
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }

    //路径改了
    private void OnPathChanged(Vector3[] path)
    {
        if (_inputData.InputMovePath == null)
        {
            ChangeState(OwnerFsm, IdleStatusCore.Name);
            return;
        }

        _pathMove.MovePath(_inputData.InputMovePath, OnMoveFinish);
    }

    private void OnInputSkillRelease(int skillID)
    {
        if (!CheckCanSkill())
        {
            return;
        }

        OwnerFsm.SetData<VarInt32>(StatusDataDefine.SKILL_ID, skillID);
        ChangeState(OwnerFsm, SkillForwardStatusCore.Name);
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