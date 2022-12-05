using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 家园总模块core部分
/// </summary>
public abstract class HomeModuleCore : MonoBehaviour
{
    /// <summary>
    /// 家园模块根 获取模块再这个上面获取
    /// </summary>
    /// <value></value>
    public static GameObject Root { get; private set; }

    /// <summary>
    /// 家园模块是否初始化完成
    /// </summary>
    public static bool IsInited => Root != null;

    /// <summary>
    /// 土地管理
    /// </summary>
    /// <value></value>
    public static IHomeSoilMgr SoilMgr { get; private set; }


    private void Start()
    {
        if (Root != null)
        {
            Log.Error("HomeModuleCore已经初始化过了");
            Destroy(Root);
            return;
        }

        Root = gameObject;

        InitModule();

        StartInitLogic();
    }

    private void OnDestroy()
    {
        UnInitModule();

        if (Root == gameObject)
        {
            Root = null;
        }
    }

    protected virtual void InitModule()
    {
        //
    }

    protected virtual void UnInitModule()
    {
        SoilMgr = null;
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