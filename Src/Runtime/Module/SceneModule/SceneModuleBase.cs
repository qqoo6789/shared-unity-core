using UnityEngine;

/// <summary>
/// 场景功能模块基类
/// </summary>
public class SceneModuleBase : MonoBehaviour
{
    /// <summary>
    /// 模块销毁
    /// 因为OnDestroy顺序无法控制，所以销毁模块请复写这里实现
    /// </summary>
    public virtual void Dispose()
    {
        //
    }
}