using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 技能前摇状态
/// </summary>
public abstract class SkillForwardStatusCore : ListenEventStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name => "skillForward";

    public override string StatusName => Name;
    protected override Type[] EventFunctionTypes => new Type[]
    {
        typeof(OnInputSkillInBattleStatusEventFunc),
        typeof(BeHitMoveEventFunc)
    };

    private CancellationTokenSource _forwardTimeToken;

    protected DRSkill CurSkillCfg;
    private EntityInputData _inputData;
    protected long[] Targets;
    protected UnityEngine.Vector3 SkillDir;

    /// <summary>
    /// 是否正常继续战斗状态离开的 false以为着是打断离开的
    /// </summary>
    protected bool IsContinueBattleLeave;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        IsContinueBattleLeave = false;

        int skillID = OwnerFsm.GetData<VarInt32>(StatusDataDefine.SKILL_ID).Value;
        SkillDir = fsm.GetData<VarVector3>(StatusDataDefine.SKILL_DIR).Value;
        Targets = fsm.GetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS).Value;
        CurSkillCfg = GFEntryCore.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);

        try
        {
            SkillForwardExecute(CurSkillCfg);
        }
        catch (Exception e)
        {
            Log.Error($"skill forward execute error ={e}");
        }

        if (CurSkillCfg.AccuBreakable)
        {
            _inputData = StatusCtrl.GetComponent<EntityInputData>();
        }

        TimeForwardFinish();

        StatusCtrl.RefEntity.EntityEvent.EnterSkillForward?.Invoke(CurSkillCfg);
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        StatusCtrl.RefEntity.EntityEvent.ExitSkillForward?.Invoke(!IsContinueBattleLeave);

        CancelTimeForwardFinish();

        _inputData = null;
        CurSkillCfg = null;
        Targets = null;
        SkillDir = UnityEngine.Vector3.zero;
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

    //前摇完成定时
    private async void TimeForwardFinish()
    {
        CancelTimeForwardFinish();

        try
        {
            _forwardTimeToken = new();
            await UniTask.Delay(CurSkillCfg.ForwardReleaseTime, false, PlayerLoopTiming.Update, _forwardTimeToken.Token);
        }
        catch (Exception)
        {
            return;
        }
        _forwardTimeToken = null;

        IsContinueBattleLeave = true;
        ChangeState(OwnerFsm, SkillCastStatusCore.Name);
    }

    // 取消前摇完成定时
    private void CancelTimeForwardFinish()
    {
        if (_forwardTimeToken != null)
        {
            _forwardTimeToken.Cancel();
            _forwardTimeToken = null;
        }
    }

    /// <summary>
    /// 技能前摇开始执行 子类覆写
    /// </summary>
    /// <param name="drSkill"></param>
    protected abstract void SkillForwardExecute(DRSkill drSkill);

    /// <summary>
    /// 执行前摇效果 子类调用
    /// </summary>
    /// <param name="drSkill">技能表数据</param>
    /// <param name="entity">实体</param>
    protected void SkillForwardEffectExecute(DRSkill drSkill, EntityBase entity)
    {

        _ = SkillUtil.EntitySkillEffectExecute(drSkill, SkillDir, Targets, drSkill.EffectForward, entity, entity);
    }

    public bool CheckCanMove()
    {
        return _inputData && CurSkillCfg.AccuBreakable;//能打断 时有移动输入时切换移动
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