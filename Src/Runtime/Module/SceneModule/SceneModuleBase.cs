using UnityEngine;

/// <summary>
/// 场景功能模块基类
/// </summary>
public class SceneModuleBase : MonoBehaviour
{
    /// <summary>
    /// 卸载场景之前
    /// </summary>
    public virtual void UnloadSceneBefore()
    {
        //
    }
    /// <summary>
    /// 卸载场景之后
    /// </summary>
    public virtual void UnloadSceneAfter()
    {
        //
    }
}