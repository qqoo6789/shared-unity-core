using UnityEngine;
using static HomeDefine;

/// <summary>
/// 采集资源共用接口
/// </summary>
public interface ICollectResourceCore
{
    public ulong Id { get; }
    /// <summary>
    /// 资源类型 分为土地作物 矿石等
    /// </summary>
    /// <value></value>
    eResourceType ResourceType { get; }
    /// <summary>
    /// 资源逻辑上的root节点 别人有可能会从这里拿组件
    /// </summary>
    /// <value></value>
    GameObject LogicRoot { get; }
    /// <summary>
    /// 位置
    /// </summary>
    /// <value></value>
    Vector3 Position { get; }

    /// 检查是否支持当前操作
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    bool CheckSupportAction(eAction action);
}