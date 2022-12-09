using UnityEngine;

/// <summary>
/// 采集资源接口
/// </summary>
public interface ICollectResource
{
    /// <summary>
    /// 资源类型 分为土地作物 矿石等
    /// </summary>
    /// <value></value>
    HomeDefine.eResourceType ResourceType { get; }

    /// <summary>
    /// 位置
    /// </summary>
    /// <value></value>
    Vector3 Position { get; }

    /// <summary>
    /// 当前是否可以采集收获
    /// </summary>
    /// <returns></returns>
    bool CanCollect();

    /// <summary>
    /// 获取显示大小 会涉及到预选中时的表现等
    /// </summary>
    /// <returns></returns>
    Vector3 GetDisplaySize();
}