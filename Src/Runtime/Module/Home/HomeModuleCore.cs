using UnityEngine;

/// <summary>
/// 家园总模块core部分
/// </summary>
public abstract class HomeModuleCore : MonoBehaviour
{
    /// <summary>
    /// 土地管理
    /// </summary>
    /// <value></value>
    public static IHomeSoilMgr SoilMgr { get; private set; }

    /// <summary>
    /// 资源区域管理
    /// </summary>
    /// <value></value>
    public static HomeResourcesAreaMgrCore HomeResourcesAreaMgr { get; private set; }

    private void Start()
    {
        InitModule();

        StartInitLogic();
    }

    private void OnDestroy()
    {
        UnInitModule();
    }

    protected virtual void InitModule()
    {
        //
    }

    protected virtual void UnInitModule()
    {
        SoilMgr = null;
        HomeResourcesAreaMgr = null;
    }

    /// <summary>
    /// 整个家园初始化完成后 开始的初始化逻辑
    /// </summary>
    protected virtual void StartInitLogic()
    {
        SoilMgr = AddHomeSoilMgr();

        _ = gameObject.AddComponent<InitHomeLogicCore>();
    }

    /// <summary>
    /// 共享库中的土地管理器需要子类实现提供 子类将具体的土地管理器返回即可
    /// </summary>
    /// <returns></returns>
    protected abstract IHomeSoilMgr AddHomeSoilMgr();
}