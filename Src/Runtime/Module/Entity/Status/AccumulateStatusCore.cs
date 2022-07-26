/*
 * @Author: xiang huan
 * @Date: 2022-07-25 15:56:56
 * @Description: 蓄力状态
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/AccumulateStatusCore.cs
 * 
 */
using System;
using System.Threading;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using Cysharp.Threading.Tasks;

/// <summary>
/// 蓄力状态通用状态基类 
/// </summary>
public abstract class AccumulateStatusCore : EntityStatusCore
{
    protected int SkillID;
    protected long TargetID;
    protected DRSkill DRSkill;
    protected CancellationTokenSource CancelToken;
    /// <summary>
    /// 子类确定技能状态类型
    /// </summary>
    /// <value></value>
    protected virtual Type SkillStatusType => typeof(object);

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
        SkillID = fsm.GetData<VarInt32>("SkillID").Value;
        TargetID = fsm.GetData<VarInt64>("SkillTargetID").Value;
        DRSkill = GFEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(SkillID);
        if (DRSkill == null)
        {
            Log.Error($"AccumulateStatusCore DRSkill is null skillID = {SkillID}");
            return;
        }
        AccumulateStart();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        //被打断
        if (CancelToken != null)
        {
            CancelToken.Cancel();
            CancelToken = null;
        }
        base.OnLeave(fsm, isShutdown);
    }
    protected virtual async void AccumulateStart()
    {
        if (CancelToken != null)
        {
            CancelToken.Cancel();
        }
        CancelToken = new();
        await UniTask.Delay(TimeSpan.FromMilliseconds(DRSkill.AccuTime), false, PlayerLoopTiming.Update, CancelToken.Token);
        CancelToken = null;
        AccumulateEnd();
    }
    protected virtual void AccumulateEnd()
    {
        ChangeState(OwnerFsm, SkillStatusType);
    }
}