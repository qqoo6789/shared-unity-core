using GameFramework.Fsm;
using UnityGameFramework.Runtime;
/// <summary>
/// 方向移动的浮空状态
/// </summary>
public class FloatInAirDirectionMoveStatusCore : FloatInAirStatusCore
{
    private DirectionMove _directionMove;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        if (MoveDefine.ENABLE_MOVE_IN_AIR)
        {
            if (StatusCtrl.TryGetComponent(out _directionMove))
            {
                _directionMove.SetMoveSpeed(StatusCtrl.RefEntity.MoveData.Speed);
                _directionMove.StartMove();
                return;
            }
            else
            {
                Log.Error($"FloatInAirDirectionMoveStatusCore enter,not find PathMove,name={StatusCtrl.gameObject.name}");
            }
        }
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        if (MoveDefine.ENABLE_MOVE_IN_AIR)
        {
            if (_directionMove)
            {
                _directionMove.StopMove();
                _directionMove = null;
            }
        }

        base.OnLeave(fsm, isShutdown);
    }
}