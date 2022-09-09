/* 
 * @Author XQ
 * @Date 2022-08-15 11:15:06
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/DirectionMoveStatusCore.cs
 */
/** 
 * @Author XQ
 * @Date 2022-08-10 16:27:01
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/DirectionMoveStatusCore.cs
 */
using System;
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
    protected override Type[] EventFunctionTypes => new Type[] {
        typeof(BeHitMoveEventFunc),
        typeof(WaitToBattleStatusEventFunc)
    };
    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        _inputData = StatusCtrl.GetComponent<EntityInputData>();
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();
        if (StatusCtrl.TryGetComponent(out _directionMove))
        {
            _directionMove.SetMoveSpeed(StatusCtrl.RefEntity.MoveData.Speed);
            _directionMove.StartMove();
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
            _directionMove.StopMove();
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

    public bool CheckCanMove()
    {
        return true;
    }

    public bool CheckCanSkill(int skillId)
    {
        return true;
    }
}