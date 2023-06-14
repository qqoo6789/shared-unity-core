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
    /// <summary>
    /// 等级
    /// </summary>
    /// <value></value>
    int Lv { get; }

    /// 检查是否支持当前复合动作
    /// </summary>
    /// <param name="action">复合动作</param>
    /// <returns></returns>
    bool CheckSupportAction(eAction action);
    /// <summary>
    /// 获取当前支持的复合动作
    /// </summary>
    /// <value></value>
    eAction SupportAction { get; }
    /// <summary>
    /// 执行动作
    /// </summary>
    /// <param name="action"></param>
    /// <param name="toolCid">工具id 可能是种子 肥料 装备</param>
    /// <param name="itemValid">道具是否有效 种子肥料专精不够使也许会无效</param>
    /// <param name="extraWateringNum">额外浇水次数</param>
    /// <param name="skillId">技能id</param>
    void ExecuteAction(eAction action, int toolCid, int skillId, object actionData);
    /// <summary>
    /// 执行了一次进度 最后一次进度也会调用 再去调用执行动作
    /// </summary>
    /// <param name="targetCurAction"></param>
    /// <param name="skillId">技能id</param>
    void ExecuteProgress(eAction targetCurAction, int skillId);
}