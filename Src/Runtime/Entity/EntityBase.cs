using UnityEngine;

/// <summary>
/// 场景实体,前后端可以直接使用，或者根据差异化派生出新的类
/// </summary>
public class EntityBase
{
    /// <summary>
    /// 实体是否初始化，dispose后变为false
    /// </summary>
    /// <value></value>
    public bool Inited { get; protected set; } = false;
    /// <summary>
    /// 逻辑实体根节点 可以挂载逻辑实体相关逻辑 一定不为空
    /// </summary>
    /// <value></value>
    protected GameObject Root;
    /// <summary>
    /// 逻辑实体的root节点的id，用于通过root节点id来获取逻辑实体
    /// </summary>
    /// <returns></returns>
    public int RootID => Root.GetInstanceID();
    /// <summary>
    /// 场景实体变换 也是Root节点的变换 一定不为空
    /// </summary>
    public Transform Transform => Root.transform;

    /// <summary>
    /// 加速获取 缓存了 entity最基础的数据组件引用
    /// </summary>
    public readonly EntityBaseData BaseData;

    /// <summary>
    /// 加速获取 缓存了 enitty 内部事件的组件 子类构建时就需要添加好对应组件
    /// </summary>
    public EntityEvent EntityEvent { get; protected set; }

    public EntityBase()
    {
        InitRoot();
        BaseData = Root.AddComponent<EntityBaseData>();
    }

    public virtual void Dispose()
    {
        Inited = false;
        UnInitFromScene();
        BaseData.Reset();

        Object.Destroy(Root);
    }

    public virtual void Init()
    {
        Inited = true;
        InitToScene();
    }

    protected virtual void InitRoot()
    {
        Root = new GameObject();
    }

    /// <summary>
    /// 添加到场景中
    /// </summary>
    protected virtual void InitToScene()
    {
        //todo:
    }

    protected virtual void UnInitFromScene()
    {
        //todo;
    }

    public void InitBaseInfo(long id, MelandGame3.EntityType type)
    {
        BaseData.Init(id, type);
        Root.name = $"{type}_{id}";
    }

    public void SetRootName(string name)
    {
        Root.name = name;
    }

    public void SetRootParent(Transform parent)
    {
        Transform.SetParent(parent, false);
    }

    public T GetComponent<T>()
    {
        return Root.GetComponent<T>();
    }

    public T AddComponent<T>() where T : Component
    {
        return Root.AddComponent<T>();
    }

    public bool TryGetComponent<T>(out T component)
    {
        return Root.TryGetComponent(out component);
    }

    public bool HasComponent<T>()
    {
        return Root.TryGetComponent(out T t);
    }

    /// <summary>
    /// 慎用 会导致维护性下降 不能明确知道添加组件的位置 获取组件 如果没有会添加一个保证一定有组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetOrAddComponent<T>() where T : Component
    {
        if (Root.TryGetComponent(out T t))
        {
            return t;
        }

        return Root.AddComponent<T>();
    }
}