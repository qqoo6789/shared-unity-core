using UnityEngine;
using static HomeDefine;

/// <summary>
/// 采集资源接口
/// </summary>
public interface ICollectResource
{
    /// <summary>
    /// 资源逻辑上的root节点 别人有可能会从这里拿组件
    /// </summary>
    /// <value></value>
    GameObject LogicRoot { get; }
    /// <summary>
    /// 资源类型 分为土地作物 矿石等
    /// </summary>
    /// <value></value>
    eResourceType ResourceType { get; }

    /// <summary>
    /// 位置
    /// </summary>
    /// <value></value>
    Vector3 Position { get; }

    /// <summary>
    /// 获取显示大小 会涉及到预选中时的表现等
    /// </summary>
    /// <returns></returns>
    Vector3 GetDisplaySize();
    /// <summary>
    /// 检查是否支持当前操作
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    bool CheckSupportAction(eAction action);
    /// <summary>
    /// 当前是否在进度型操作中 None表示不在
    /// </summary>
    /// <value></value>
    eAction CurProgressAction { get; }
    float CurProgressActionValue { get; set; }
    float CurProgressActionMaxValue { get; }
    /// <summary>
    /// 临时用来标记是否有更新的资源 用来节约GC 具体可以看使用的地方
    /// </summary>
    /// <value></value>
    bool BuffIsUpdate { get; set; }
}