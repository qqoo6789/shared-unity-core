using System.Threading;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 技能前摇状态
/// </summary>
public abstract class SkillForwardStatusCore : EntityStatusCore
{
    public static new string Name => "skillForward";

    public override string StatusName => Name;

    private CancellationTokenSource _forwardTimeToken;

    protected DRSkill CurSkillCfg;
    private EntityInputData _inputData;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        int skillID = OwnerFsm.GetData<VarInt32>(StatusDataDefine.SKILL_ID).Value;
        CurSkillCfg = TemporaryInterface.GetDataTable<DRSkill>().GetDataRow(skillID);

        try
        {
            SkillForwardExecute(CurSkillCfg);
        }
        catch (System.Exception e)
        {
            Log.Error($"skill forward execute error ={e}");
        }

        if (CurSkillCfg.AccuBreakable)
        {
            _inputData = StatusCtrl.GetComponent<EntityInputData>();
        }

        TimeForwardFinish();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        CancelTimeForwardFinish();

        _inputData = null;
        CurSkillCfg = null;

        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        if (_inputData)
        {
            //能打断 时有移动输入时切换移动
            if (CurSkillCfg.AccuBreakable)
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
    }

    //前摇完成定时
    private async void TimeForwardFinish()
    {
        CancelTimeForwardFinish();

        try
        {
            _forwardTimeToken = new();
            await TemporaryInterface.Delay(CurSkillCfg.ForwardReleaseTime, false, PlayerLoopTiming.Update, _forwardTimeToken.Token);
        }
        catch (System.Exception)
        {
            return;
        }
        _forwardTimeToken = null;
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
}