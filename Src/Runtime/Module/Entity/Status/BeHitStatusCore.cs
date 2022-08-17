/*
 * @Author: xiang huan
 * @Date: 2022-07-25 15:56:56
 * @Description: 受击状态 理论上受击状态只有表现,服务器用不到
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/BeHitStatusCore.cs
 * 
 */
using System;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 受击状态通用状态基类
/// </summary>
public abstract class BeHitStatusCore : ListenEventStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name => "beHit";
    private EntityInputData _inputData;
    private EntityBattleDataCore _battleData;

    protected override Type[] EventFunctionTypes => new Type[] {
        typeof(OnInputSkillInBattleStatusEventFunc),
        typeof(BeHitMoveEventFunc)
    };

    public override string StatusName => Name;
    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
        _inputData = StatusCtrl.GetComponent<EntityInputData>();
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        _inputData = null;
        _battleData = null;
        base.OnLeave(fsm, isShutdown);
    }

    protected virtual void OnBeHitComplete()
    {
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }
    protected override void AddEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease += OnInputSkillRelease;
    }

    protected override void RemoveEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease -= OnInputSkillRelease;
    }
    private void OnInputSkillRelease(int skillID, UnityEngine.Vector3 dir, long[] targets)
    {
        if (!CheckCanSkill())
        {
            return;
        }

        OwnerFsm.SetData<VarInt32>(StatusDataDefine.SKILL_ID, skillID);
        OwnerFsm.SetData<VarVector3>(StatusDataDefine.SKILL_DIR, dir);
        OwnerFsm.SetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS, targets);
        ChangeState(OwnerFsm, SkillAccumulateStatusCore.Name);
    }

    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (_battleData && !_battleData.IsLive())
        {
            ChangeState(fsm, DeathStatusCore.Name);
            return;
        }
        if (CheckCanMove())
        {
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

    public bool CheckCanMove()
    {
        return true;
    }

    public bool CheckCanSkill(int skillId)
    {
        return true;
    }
}