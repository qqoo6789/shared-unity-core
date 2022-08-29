using UnityEngine;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using System;

/// <summary>
/// 路径移动状态
/// </summary>
public class PathMoveStatusCore : ListenEventStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name = "pathMove";

    public override string StatusName => Name;

    private EntityInputData _inputData;
    private PathMove _pathMove;
    protected PathMove PathMove => _pathMove;
    private EntityBattleDataCore _battleData;
    protected override Type[] EventFunctionTypes => new Type[] {
        typeof(BeHitMoveEventFunc),
        typeof(WaitToBattleStatusEventFunc)
    };
    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        _inputData = StatusCtrl.GetComponent<EntityInputData>();
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();

        Vector3[] path = _inputData.InputMovePath;
        if (path == null || path.Length == 0)
        {
            Log.Error($"path move status enter,not find path ,data empth={path == null}");
            ChangeState(fsm, IdleStatusCore.Name);
            return;
        }

        if (!StatusCtrl.TryGetComponent(out _pathMove))
        {
            Log.Error($"path move status enter,not find PathMove,name={StatusCtrl.gameObject.name}");
            ChangeState(fsm, IdleStatusCore.Name);
            return;
        }

        _pathMove.enabled = true;
        _pathMove.SetMoveSpeed(StatusCtrl.GetComponent<EntityMoveData>().Speed);
        _pathMove.MovePath(_inputData.InputMovePath, OnMoveFinish);

        PathMove.OnWaypointChangedEvent += OnWaypointChanged;
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        PathMove.OnWaypointChangedEvent -= OnWaypointChanged;

        _battleData = null;
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
        entityEvent.SpecialMoveStartNotMoveStatus += OnSpecialMoveStart;
    }

    protected override void RemoveEvent(EntityEvent entityEvent)
    {
        entityEvent.InputMovePathChanged -= OnPathChanged;
        entityEvent.SpecialMoveStartNotMoveStatus -= OnSpecialMoveStart;
    }

    private void OnSpecialMoveStart()
    {
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }

    private void OnMoveFinish(PathMove pathMove)
    {
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }

    /// <summary>
    /// 到了路径拐点
    /// </summary>
    /// <param name="nextPoint">下一个准备去的点</param>
    protected virtual void OnWaypointChanged(Vector3 nextPoint)
    {
        //改变朝向
        Transform transform = StatusCtrl.transform;
        Vector3 moveDirVector3 = nextPoint - transform.position;
        transform.forward = new Vector3(moveDirVector3.x, transform.forward.y, moveDirVector3.z);
    }

    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (_battleData && !_battleData.IsLive())
        {
            ChangeState(fsm, DeathStatusCore.Name);
            return;
        }
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

    public bool CheckCanMove()
    {
        return true;
    }

    public bool CheckCanSkill(int skillId)
    {
        return true;
    }
}