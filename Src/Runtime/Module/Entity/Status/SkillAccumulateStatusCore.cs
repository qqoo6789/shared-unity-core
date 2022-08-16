/*
 * @Author: xiang huan
 * @Date: 2022-07-25 15:56:56
 * @Description: 蓄力状态
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/SkillAccumulateStatusCore.cs
 * 
 */
using System.Threading;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// 蓄力状态通用状态基类 
/// </summary>

public class SkillAccumulateStatusCore : ListenEventStatusCore, IEntityCanMove
{
    protected int SkillID;
    protected long[] Targets;
    protected UnityEngine.Vector3 SkillDir;
    protected DRSkill DRSkill;
    private EntityInputData _inputData;
    protected CancellationTokenSource CancelToken;

    public static new string Name => "skillAccumulate";
    public override string StatusName => Name;
    private EntityBattleDataCore _battleData;
    /// <summary>
    /// 是否正常继续战斗状态离开的 false以为着是打断离开的
    /// </summary>
    protected bool IsContinueBattleLeave;

    protected override Type[] EventFunctionTypes => new Type[] { typeof(OnInputSkillInBattleStatusEventFunc) };

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
        IsContinueBattleLeave = false;

        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();
        SkillID = fsm.GetData<VarInt32>(StatusDataDefine.SKILL_ID).Value;
        SkillDir = fsm.GetData<VarVector3>(StatusDataDefine.SKILL_DIR).Value;
        Targets = fsm.GetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS).Value;
        DRSkill = GFEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(SkillID);

        if (DRSkill == null)
        {
            Log.Error($"AccumulateStatusCore DRSkill is null skillID = {SkillID}");
            return;
        }
        if (DRSkill.AccuBreakable)
        {
            _inputData = StatusCtrl.GetComponent<EntityInputData>();
        }

        if (DRSkill.AccuTime > 0)
        {
            AccumulateStart();
        }
        else
        {
            AccumulateEnd();
        }
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        CancelTimeAccumulate();
        _inputData = null;
        _battleData = null;
        Targets = null;
        SkillDir = UnityEngine.Vector3.zero;
        IsContinueBattleLeave = false;

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

    // 取消蓄力
    private void CancelTimeAccumulate()
    {
        if (CancelToken != null)
        {
            CancelToken.Cancel();
            CancelToken = null;
        }
    }

    protected virtual async void AccumulateStart()
    {
        CancelTimeAccumulate();
        try
        {
            CancelToken = new();
            await UniTask.Delay(DRSkill.AccuTime, false, PlayerLoopTiming.Update, CancelToken.Token);
            CancelToken = null;
        }
        catch (System.Exception)
        {
            return;
        }
        AccumulateEnd();
    }
    protected virtual void AccumulateEnd()
    {
        IsContinueBattleLeave = true;
        ChangeState(OwnerFsm, SkillForwardStatusCore.Name);
    }

    public bool CheckCanMove()
    {
        return _inputData && DRSkill.AccuBreakable;
    }
}