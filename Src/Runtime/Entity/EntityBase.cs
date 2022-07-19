using UnityEngine;

/// <summary>
/// 场景实体,前后端可以直接使用，或者根据差异化派生出新的类
/// </summary>
public class EntityBase
{
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
    /// entity base data
    /// </summary>
    public EntityBaseData BaseData { get; protected set; }

    public EntityBase()
    {
        InitRoot();
        BaseData = Root.AddComponent<EntityBaseData>();
    }

    public virtual void Init()
    {
        InitToScene();
    }

    public virtual void Dispose()
    {
        UnInitFromScene();
        BaseData.Reset();
        Object.Destroy(Root);
        Root = null;
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

    public void InitBaseInfo(long id, eEntityType type)
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
}