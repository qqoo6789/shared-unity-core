using UnityEngine;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 路径移动状态
/// </summary>
public abstract class PathMoveStatusCore : EntityStatusCore
{
    public static new string Name = "pathMove";

    public override string StatusName => Name;

    private EntityInputData _inputData;
    private PathMove _pathMove;
    private EntityEvent _entityEvent;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        _entityEvent = StatusCtrl.GetComponent<EntityEvent>();
        _entityEvent.InputMovePathChanged += OnPathChanged;

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
        _entityEvent.InputMovePathChanged -= OnPathChanged;
        _entityEvent = null;

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
}