using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 技能施法状态 正式产生伤害阶段
/// </summary>
public class SkillCastStatusCore : EntityStatusCore
{
    public static new string Name => "skillCast";

    public override string StatusName => Name;

    protected DRSkill CurSkillCfg;
    private CancellationTokenSource _castTimeToken;
    private EntityBattleDataCore _battleData;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        int skillID = OwnerFsm.GetData<VarInt32>(StatusDataDefine.SKILL_ID).Value;
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