/* 
 * @Author XQ
 * @Date 2022-08-15 11:15:06
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/EntityEvent.cs
 */
using System;
using UnityEngine;

/// <summary>
/// 实体上的内部消息组件 必然存在在实体上 里面都是各种消息Action 具体消息执行时需要加上?Invoke 判断是否为空 为了节省性能初始化不会分配事件初始值
/// </summary>
public class EntityEvent : EntityBaseComponent
{
    /// <summary>
    /// 输入的移动路径变化了 T0:path
    /// </summary>
    public Action<Vector3[]> InputMovePathChanged;
    /// <summary>
    /// 输入技能释放 发送消息前需要自行检查配置 后面不会再重复检查 T0:skillID T1:dir技能朝向 T2:targets技能目标列表 T3:isTry 是否尝试释放技能不是尝试代表一定要放
    /// </summary>
    public Action<int, Vector3, long[], bool> InputSkillRelease;
    /// <summary>
    /// 实体路径移动到达目标点 中途停止不会广播 只有通过实体本身路径移动才会广播 单纯使用通用移动脚本直接移动的不会广播
    /// </summary>
    public Action OnEntityPathMoveArrived;
    /// <summary>
    /// 实体重生
    /// </summary>
    public Action EntityBeReborn;
    /// <summary>
    /// 受击移动 T0:持续时间
    /// </summary>
    public Action<int> EntityBeHitMove;
    /// <summary>
    /// 非移动状态的特殊移动开始 往往是技能效果的强制移动等触发
    /// </summary>
    public Action SpecialMoveStartNotMoveStatus;
}