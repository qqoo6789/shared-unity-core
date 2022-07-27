using System;
using UnityEngine;

/// <summary>
/// 实体上的内部消息组件 必然存在在实体上 里面都是各种消息Action 具体消息执行时需要加上?Invoke 判断是否为空 为了节省性能初始化不会分配事件初始值
/// </summary>
public class EntityEvent : MonoBehaviour
{
    /// <summary>
    /// 输入的移动路径变化了 T0:path
    /// </summary>
    public Action<Vector3[]> InputMovePathChanged;
    /// <summary>
    /// 输入技能释放 发送消息前需要自行检查配置 后面不会再重复检查 T0:skillID
    /// </summary>
    public Action<int> InputSkillRelease;
}