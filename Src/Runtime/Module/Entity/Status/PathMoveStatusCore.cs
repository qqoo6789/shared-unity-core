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

    protected EntityInputData InputData { get; private set; }
    private DistanceMove _distanceMove;
    private EntityBattleDataCore _battleData;
    protected override Type[] EventFunctionTypes => new Type[] {
        typeof(BeHitMoveEventFunc),
        typeof(WaitToBattleStatusEventFunc)
    };
    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        InputData = StatusCtrl.GetComponent<EntityInputData>();
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();

        if (InputData.InputMovePath.Count == 0)
        {
            Log.Error($"path move status enter,not find path ,data empth");
            ChangeState(fsm, IdleStatusCore.Name);
            return;
        }

        if (!StatusCtrl.TryGetComponent(out _distanceMove))
        {
            Log.Error($"path move status enter,not find DistanceMove,name={StatusCtrl.gameObject.name}");
            ChangeState(fsm, IdleStatusCore.Name);
            return;
        }

        MoveToNextPoint();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        _battleData = null;
        if (InputData != null)
        {
            InputData.ClearInputMovePath(false);
            InputData = null;
        }

        if (_distanceMove != null)
        {
            _distanceMove.StopMove();
            _distanceMove = null;
        }

        base.OnLeave(fsm, isShutdown);
    }

    protected virtual void MoveToNextPoint()
    {
        Vector3 nextPos = InputData.InputMovePath.Peek();
        Vector3 offset = nextPos - StatusCtrl.RefEntity.Position;

        //改变朝向
        StatusCtrl.RefEntity.SetForward(new Vector3(offset.x, 0, offset.z));

        //移动
        float speed = StatusCtrl.RefEntity.MoveData.Speed;
        _distanceMove.MoveTo(offset, offset.magnitude, speed, OnNextPointArrived);
    }

    /// <summary>
    /// 下一个拐点走到了
    /// </summary>
    private void OnNextPointArrived()
    {
        _ = InputData.InputMovePath.Dequeue();

        if (InputData.InputMovePath.Count == 0)
        {
            OnMoveFinish();
        }
        else
        {
            MoveToNextPoint();
        }
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

    private void OnMoveFinish()
    {
        ChangeState(OwnerFsm, IdleStatusCore.Name);
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
    private void OnPathChanged()
    {
        if (InputData.InputMovePath.Count == 0)
        {
            ChangeState(OwnerFsm, IdleStatusCore.Name);
            return;
        }

        MoveToNextPoint();
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