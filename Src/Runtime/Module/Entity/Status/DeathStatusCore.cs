/*
 * @Author: xiang huan
 * @Date: 2022-08-07 10:29:02
 * @Description: 死亡状态
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/DeathStatusCore.cs
 * 
 */
using System.Threading;
using Cysharp.Threading.Tasks;
using GameFramework.Fsm;

/// <summary>
/// 死亡状态通用状态基类
/// </summary>
public class DeathStatusCore : ListenEventStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name = "death";

    public override string StatusName => Name;
    protected CancellationTokenSource CancelToken;
    protected virtual int DeathTime => 3000;
    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
        OnDeathStart();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        CancelTimeDeath();
        base.OnLeave(fsm, isShutdown);
    }

    protected override void AddEvent(EntityEvent entityEvent)
    {
        entityEvent.EntityBeReborn += OnBeReborn;
    }

    protected override void RemoveEvent(EntityEvent entityEvent)
    {
        entityEvent.EntityBeReborn -= OnBeReborn;
    }
    private void CancelTimeDeath()
    {
        if (CancelToken != null)
        {
            CancelToken.Cancel();
            CancelToken = null;
        }
    }

    /// <summary>
    /// 死亡开始
    /// </summary>
    /// <value></value>
    protected virtual async void OnDeathStart()
    {
        CancelTimeDeath();
        try
        {
            CancelToken = new();
            await UniTask.Delay(DeathTime, false, PlayerLoopTiming.Update, CancelToken.Token);
            CancelToken = null;
        }
        catch (System.Exception)
        {
            return;
        }
        OnDeathEnd();
    }

    /// <summary>
    /// 死亡结束
    /// </summary>
    /// <value></value>
    protected virtual void OnDeathEnd()
    {

    }

    /// <summary>
    /// 复活
    /// </summary>
    /// <value></value>
    protected virtual void OnBeReborn()
    {
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }

    public bool CheckCanMove()
    {
        return false;
    }

    public bool CheckCanSkill(int skillId)
    {
        return false;
    }
}