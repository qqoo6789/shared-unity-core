using UnityEngine;
using GameFramework.Fsm;
using GameFramework;
using UnityGameFramework.Runtime;

/// <summary>
/// 实体上控制自身状态的功能组件 使用之前需要初始化状态机
/// </summary>
public class EntityStatusCtrl : MonoBehaviour
{
    private EntityEvent _entityEvent;//缓存实体上的事件组件 节省性能
    private IFsm<EntityStatusCtrl> _fsm;//当前状态机 状态机名字就是EntityBase Root GetInstanceID()

    private static IFsmManager _cacheFsmMgr;//不用每次去GameFrameworkEntry获取 获取代码底层是遍历 有性能损耗 后续优化成字典后就可以不用缓存了

    private void OnDestroy()
    {
        if (_fsm != null)
        {
            GetFsmManager().DestroyFsm(_fsm);
            _fsm = null;
        }
        _entityEvent = null;
    }

    /// <summary>
    /// 初始化状态机 需要给定固定的状态实例
    /// </summary>
    /// <param name="states"></param>
    public void InitFsm(params FsmState<EntityStatusCtrl>[] states)
    {
        _fsm = GetFsmManager().CreateFsm(GetHashCode().ToString(), this, states);
    }

    /// <summary>
    /// 启动状态 需要给定启动状态
    /// </summary>
    /// <typeparam name="TStartStatus"></typeparam>
    public void StartStatus<TStartStatus>() where TStartStatus : FsmState<EntityStatusCtrl>
    {
        if (_fsm == null)
        {
            Log.Error($"start status when not init fsm name={gameObject.name}");
            return;
        }

        _fsm.Start<TStartStatus>();
    }

    /// <summary>
    /// 获取实体事件组件 专门给状态用的 外部不要使用
    /// </summary>
    /// <returns></returns>
    public EntityEvent GetEvent()
    {
        if (_entityEvent == null)
        {
            _entityEvent = gameObject.GetComponent<EntityEvent>();
        }
        return _entityEvent;
    }

    private IFsmManager GetFsmManager()
    {
        if (_cacheFsmMgr == null)
        {
            _cacheFsmMgr = GameFrameworkEntry.GetModule<IFsmManager>();
        }

        return _cacheFsmMgr;
    }

    /// <summary>
    /// 统一创建实体状态的方法 后面方便统一走对象池什么的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T CreateStatus<T>() where T : EntityStatusBase, new()
    {
        return new T();
    }

    /// <summary>
    /// 统一销毁实体状态的方法 后面方便统一走对象池什么的
    /// </summary>
    /// <param name="status"></param>
    public static void DestroyStatus(EntityStatusBase status)
    {

    }
}