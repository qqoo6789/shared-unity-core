/*
 * @Author: xiang huan
 * @Date: 2022-07-25 15:56:56
 * @Description: 受击移动
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/BeHitMoveStatusCore.cs
 * 
 */
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

/// <summary>
/// 受击移动状态基类 
/// </summary>
public class BeHitMoveStatusCore : EntityStatusCore
{

    protected CancellationTokenSource CancelToken;

    public static new string Name => "beHitMove";
    public override string StatusName => Name;
    private EntityBattleDataCore _battleData;
    private int _moveTime;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();
        _moveTime = OwnerFsm.GetData<VarInt32>(StatusDataDefine.BE_HIT_MOVE_TIME).Value;
        MoveStart();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        CancelTime();
        _battleData = null;
        _moveTime = 0;
        _ = OwnerFsm.RemoveData(StatusDataDefine.BE_HIT_MOVE_TIME);
        base.OnLeave(fsm, isShutdown);
    }

    // 取消蓄力
    private void CancelTime()
    {
        if (CancelToken != null)
        {
            CancelToken.Cancel();
            CancelToken = null;
        }
    }

    protected virtual async void MoveStart()
    {
        CancelTime();
        try
        {
            CancelToken = new();
            await UniTask.Delay(_moveTime, false, PlayerLoopTiming.Update, CancelToken.Token);
            CancelToken = null;
        }
        catch (System.Exception)
        {
            Log.Error("BeHitMove MoveStart Error");
        }
        MoveEnd();
    }
    protected virtual void MoveEnd()
    {
        if (_battleData && !_battleData.IsLive())
        {
            ChangeState(OwnerFsm, DeathStatusCore.Name);
            return;
        }
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }
}