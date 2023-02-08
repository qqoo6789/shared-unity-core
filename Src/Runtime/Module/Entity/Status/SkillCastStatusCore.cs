/** 
 * @Author XQ
 * @Date 2022-08-10 16:27:01
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/SkillCastStatusCore.cs
 */
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 技能施法状态 正式产生伤害阶段
/// </summary>
public class SkillCastStatusCore : ListenEventStatusCore, IEntityCanSkill
{
    public static new string Name => "skillCast";

    public override string StatusName => Name;
    protected long[] Targets;
    protected UnityEngine.Vector3 SkillDir;
    protected double SkillTimeScale;

    protected DRSkill CurSkillCfg;
    private CancellationTokenSource _castTimeToken;
    protected float ReleaseTimeScale;

    private bool _continueNextSkill;//是否继续下一个技能
    protected override Type[] EventFunctionTypes => new Type[] {
        typeof(BeHitMoveEventFunc)
    };

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        int skillID = fsm.GetData<VarInt32>(StatusDataDefine.SKILL_ID).Value;
        SkillDir = fsm.GetData<VarVector3>(StatusDataDefine.SKILL_DIR).Value;
        Targets = fsm.GetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS).Value;
        SkillTimeScale = fsm.GetData<VarDouble>(StatusDataDefine.SKILL_TIME_SCALE).Value;

        CurSkillCfg = GFEntryCore.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);
        float releaseSpd = StatusCtrl.RefEntity.EntityAttributeData.GetRealValue((eAttributeType)CurSkillCfg.ReleaseSpd);
        ReleaseTimeScale = Math.Max(1 + releaseSpd, 0.1f);
        try
        {
#if UNITY_EDITOR
            if (CurSkillCfg.SkillRange != null && CurSkillCfg.SkillRange.Length > 0)//锁定目标技能 不一定配置范围
            {
                if (StatusCtrl.TryGetComponent(out SkillShapeGizmos skillShapeGizmos))
                {
                    skillShapeGizmos.StartDraw(CurSkillCfg.SkillRange, StatusCtrl.gameObject, SkillDir);
                }
            }
#endif
            SkillCastExecute(CurSkillCfg);
        }
        catch (System.Exception e)
        {
            Log.Error($"skill cast execute error ={e}");
        }

        TimeCastFinish();

        StatusCtrl.RefEntity.EntityEvent.EnterSkillCast?.Invoke(CurSkillCfg);
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        StatusCtrl.RefEntity.EntityEvent.ExitSkillCast?.Invoke();

        CancelTimeCastFinish();
        CurSkillCfg = null;
        Targets = null;
        SkillDir = Vector3.zero;
#if UNITY_EDITOR
        if (StatusCtrl.TryGetComponent(out SkillShapeGizmos skillShapeGizmos))
        {
            skillShapeGizmos.StopDraw();
        }
#endif

        if (!_continueNextSkill)
        {
            _ = fsm.RemoveData(StatusDataDefine.SKILL_ID);
            _ = fsm.RemoveData(StatusDataDefine.SKILL_DIR);
            _ = fsm.RemoveData(StatusDataDefine.SKILL_TARGETS);
            _ = fsm.RemoveData(StatusDataDefine.SKILL_TIME_SCALE);
            _continueNextSkill = false;
        }
        base.OnLeave(fsm, isShutdown);
    }

    protected override void AddEvent(EntityEvent entityEvent)
    {
        base.AddEvent(entityEvent);

        entityEvent.InputSkillRelease += OnInputSkillRelease;
    }

    protected override void RemoveEvent(EntityEvent entityEvent)
    {
        base.RemoveEvent(entityEvent);

        entityEvent.InputSkillRelease -= OnInputSkillRelease;
    }
    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (StatusCtrl.RefEntity.BattleDataCore != null && !StatusCtrl.RefEntity.BattleDataCore.IsLive())
        {
            OnBeforeChangeToDeath();
            ChangeState(fsm, DeathStatusCore.Name);
            return;
        }
    }
    protected virtual void OnInputSkillRelease(int skillID, Vector3 dir, long[] targets, bool isTry, double timeScale)
    {
        bool valid = false;

        if (!isTry)
        {
            valid = true;
        }
        else//尝试释放
        {
            //是翻滚动作
            if (StatusCtrl.TryGetComponent(out PlayerRoleDataCore playerData) && playerData.DRRole.JumpRollSkill == skillID)
            {
                valid = true;
            }
        }

        if (valid)
        {
            SeContinueNextSkill(true);

            StatusCtrl.transform.forward = dir;

            OwnerFsm.SetData<VarInt32>(StatusDataDefine.SKILL_ID, skillID);
            OwnerFsm.SetData<VarVector3>(StatusDataDefine.SKILL_DIR, dir);
            OwnerFsm.SetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS, targets);
            OwnerFsm.SetData<VarDouble>(StatusDataDefine.SKILL_TIME_SCALE, timeScale);
            ChangeState(OwnerFsm, SkillAccumulateStatusCore.Name);
        }
    }

    //施法完成定时
    private async void TimeCastFinish()
    {
        CancelTimeCastFinish();

        try
        {
            _castTimeToken = new();
            int delayTime = (int)((CurSkillCfg.ReleaseTime - CurSkillCfg.ForwardReleaseTime) / SkillTimeScale);
            await UniTask.Delay(delayTime, false, PlayerLoopTiming.Update, _castTimeToken.Token);
        }
        catch (System.Exception)
        {
            return;
        }
        _castTimeToken = null;
        OnFinishChangeToNextStatus();
    }

    // 取消施法完成定时
    private void CancelTimeCastFinish()
    {
        if (_castTimeToken != null)
        {
            _castTimeToken.Cancel();
            _castTimeToken = null;
        }
    }

    /// <summary>
    /// 技能施法开始执行 子类覆写
    /// </summary>
    /// <param name="curSkillCfg"></param>
    protected virtual void SkillCastExecute(DRSkill curSkillCfg) { }
    /// <summary>
    /// 后摇完成需要切换到下一个状态的时候覆写
    /// </summary>
    protected virtual void OnFinishChangeToNextStatus()
    {
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }

    /// <summary>
    /// 在死亡了 需要切换到死亡状态前执行的额外操作
    /// </summary>
    protected virtual void OnBeforeChangeToDeath()
    {
        SeContinueNextSkill(false);
    }

    /// <summary>
    /// 设置是否继续下一个技能 如果有下一个技能 基类就不会离开状态清理技能数据
    /// </summary>
    /// <param name="isContinue"></param>
    protected void SeContinueNextSkill(bool isContinue)
    {
        _continueNextSkill = isContinue;
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