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

    protected DRSkill CurSkillCfg;
    private CancellationTokenSource _castTimeToken;
    private EntityBattleDataCore _battleData;

    private bool _continueNextSkill;//是否继续下一个技能

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        int skillID = fsm.GetData<VarInt32>(StatusDataDefine.SKILL_ID).Value;
        SkillDir = fsm.GetData<VarVector3>(StatusDataDefine.SKILL_DIR).Value;
        Targets = fsm.GetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS).Value;

        CurSkillCfg = GFEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();

        try
        {
            SkillCastExecute(CurSkillCfg);
        }
        catch (System.Exception e)
        {
            Log.Error($"skill cast execute error ={e}");
        }

        TimeCastFinish();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        CancelTimeCastFinish();
        CurSkillCfg = null;
        _battleData = null;
        Targets = null;
        SkillDir = Vector3.zero;

        if (!_continueNextSkill)
        {
            _ = fsm.RemoveData(StatusDataDefine.SKILL_ID);
            _ = fsm.RemoveData(StatusDataDefine.SKILL_DIR);
            _ = fsm.RemoveData(StatusDataDefine.SKILL_TARGETS);
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

    protected virtual void OnInputSkillRelease(int skillID, Vector3 dir, long[] targets, bool isTry)
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
            await UniTask.Delay(CurSkillCfg.ReleaseTime - CurSkillCfg.ForwardReleaseTime, false, PlayerLoopTiming.Update, _castTimeToken.Token);
        }
        catch (System.Exception)
        {
            return;
        }
        _castTimeToken = null;
        if (_battleData && !_battleData.IsLive())
        {
            ChangeState(OwnerFsm, DeathStatusCore.Name);
            return;
        }
        ChangeState(OwnerFsm, IdleStatusCore.Name);
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