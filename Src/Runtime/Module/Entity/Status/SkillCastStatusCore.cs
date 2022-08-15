/** 
 * @Author XQ
 * @Date 2022-08-10 16:27:01
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/SkillCastStatusCore.cs
 */
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 技能施法状态 正式产生伤害阶段
/// </summary>
public class SkillCastStatusCore : ListenEventStatusCore
{
    public static new string Name => "skillCast";

    public override string StatusName => Name;
    protected long[] Targets;
    protected UnityEngine.Vector3 SkillDir;

    protected DRSkill CurSkillCfg;
    private CancellationTokenSource _castTimeToken;
    private EntityBattleDataCore _battleData;

    protected override Type[] EventFunctionTypes => new Type[] { typeof(OnInputSkillInBattleStatusEventFunc) };


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
        SkillDir = UnityEngine.Vector3.zero;
        _ = fsm.RemoveData(StatusDataDefine.SKILL_ID);
        _ = fsm.RemoveData(StatusDataDefine.SKILL_DIR);
        _ = fsm.RemoveData(StatusDataDefine.SKILL_TARGETS);
        base.OnLeave(fsm, isShutdown);
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
}