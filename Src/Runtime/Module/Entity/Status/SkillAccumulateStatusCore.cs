/*
 * @Author: xiang huan
 * @Date: 2022-07-25 15:56:56
 * @Description: 蓄力状态
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/SkillAccumulateStatusCore.cs
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

public class SkillAccumulateStatusCore : ListenEventStatusCore, IEntityCanMove, IEntityCanSkill
{
    protected int SkillID;
    protected long[] Targets;
    protected UnityEngine.Vector3 SkillDir;
    protected DRSkill CurSkillCfg;
    private EntityInputData _inputData;
    protected CancellationTokenSource CancelToken;

    public static new string Name => "skillAccumulate";
    public override string StatusName => Name;
    /// <summary>
    /// 是否正常继续战斗状态离开的 false以为着是打断离开的
    /// </summary>
    protected bool IsContinueBattleLeave;

    protected override Type[] EventFunctionTypes => new Type[] {
        typeof(OnInputSkillInBattleStatusEventFunc),
        typeof(BeHitMoveEventFunc)
    };

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
        IsContinueBattleLeave = false;

        SkillID = fsm.GetData<VarInt32>(StatusDataDefine.SKILL_ID).Value;
        SkillDir = fsm.GetData<VarVector3>(StatusDataDefine.SKILL_DIR).Value;
        Targets = fsm.GetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS).Value;
        CurSkillCfg = GFEntryCore.DataTable.GetDataTable<DRSkill>().GetDataRow(SkillID);

        if (CurSkillCfg == null)
        {
            Log.Error($"AccumulateStatusCore DRSkill is null skillID = {SkillID}");
            return;
        }
        if (CurSkillCfg.AccuBreakable)
        {
            _inputData = StatusCtrl.GetComponent<EntityInputData>();
        }

        if (CurSkillCfg.AccuTime > 0)
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
        Targets = null;
        SkillDir = UnityEngine.Vector3.zero;
        CurSkillCfg = null;
        IsContinueBattleLeave = false;

        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (StatusCtrl.RefEntity.BattleDataCore != null && !StatusCtrl.RefEntity.BattleDataCore.IsLive())
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
            else if (_inputData.InputMovePath.Count > 0)
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
            await UniTask.Delay(CurSkillCfg.AccuTime, false, PlayerLoopTiming.Update, CancelToken.Token);
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
        return _inputData && CurSkillCfg.AccuBreakable;
    }

    public bool CheckCanSkill(int skillID)
    {
        if (skillID == 0)
        {
            return false;
        }

        //现在没有强校验技能具体技能
        return true;
    }
}