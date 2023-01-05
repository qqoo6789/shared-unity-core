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
    /// <summary>
    /// 执行动作
    /// </summary>
    /// <param name="action"></param>
    /// <param name="toolCid">工具id 可能是种子 肥料 装备</param>
    /// <param name="itemValid">道具是否有效 种子肥料专精不够使也许会无效</param>
    void ExecuteAction(eAction action, int toolCid, bool itemValid);
}