using GameFramework.Fsm;

/// <summary>
/// 实体上的各种状态的基类 实体状态类直接可以在内部任意时间切换状态 不要求再update 切换时取OwnerFsm
/// </summary>
public class EntityStatusBase : FsmState<EntityStatusCtrl>
{
    /// <summary>
    /// 状态的owner状态机 用来切换状态和查看状态
    /// </summary>
    /// <value></value>
    protected IFsm<EntityStatusCtrl> OwnerFsm { get; private set; }
    /// <summary>
    /// 状态控制器 挂载EntityBase上的EntityStatusCtrl组件  事件监听就是使用这个组件上的功能
    /// </summary>
    /// <value></value>
    protected EntityStatusCtrl StatusCtrl { get; private set; }

    protected override void OnInit(IFsm<EntityStatusCtrl> fsm)
    {
        OwnerFsm = fsm;
        StatusCtrl = fsm.Owner;
    }
}