using GameFramework.Fsm;

/// <summary>
/// 可以监听EntityEvent的状态
/// </summary>
public abstract class ListenEventStatusCore : EntityStatusCore
{
    protected EntityEvent EntityEvent { get; private set; }

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        EntityEvent = StatusCtrl.GetComponent<EntityEvent>();
        AddEvent(EntityEvent);
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        RemoveEvent(EntityEvent);
        EntityEvent = null;

        base.OnLeave(fsm, isShutdown);
    }

    /// <summary>
    /// 进入状态时的添加事件
    /// </summary>
    /// <param name="entityEvent"></param>
    protected abstract void AddEvent(EntityEvent entityEvent);

    /// <summary>
    /// 离开状态时移除事件
    /// </summary>
    /// <param name="entityEvent"></param>
    protected abstract void RemoveEvent(EntityEvent entityEvent);
}