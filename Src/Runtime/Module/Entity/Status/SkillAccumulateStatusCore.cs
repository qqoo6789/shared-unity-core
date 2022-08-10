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

/// <summary>
/// 蓄力状态通用状态基类 
/// </summary>
public abstract class SkillAccumulateStatusCore : EntityStatusCore, IEntityCanMove
{
    protected int SkillID;
    protected long TargetID;
    protected DRSkill DRSkill;
    private EntityInputData _inputData;
    protected CancellationTokenSource CancelToken;

    public static new string Name => "skillAccumulate";
    public override string StatusName => Name;
    private EntityBattleDataCore _battleData;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();
        SkillID = fsm.GetData<VarInt32>(StatusDataDefine.SKILL_ID).Value;
        TargetID = fsm.GetData<VarInt64>(StatusDataDefine.SKILL_TARGET_ID).Value;
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
        ChangeState(OwnerFsm, SkillForwardStatusCore.Name);
    }

    public bool CheckCanMove()
    {
        return _inputData && DRSkill.AccuBreakable;
    }
}