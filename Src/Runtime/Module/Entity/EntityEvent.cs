/* 
 * @Author XQ
 * @Date 2022-08-15 11:15:06
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/EntityEvent.cs
 */
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
    /// 输入技能释放 发送消息前需要自行检查配置 后面不会再重复检查 T0:skillID T1:技能朝向 T2:技能目标列表
    /// </summary>
    public Action<int, Vector3, long[]> InputSkillRelease;
    /// <summary>
    /// 开始翻滚技能
    /// </summary>
    public Action StartJumpRoll;
    /// <summary>
    /// 实体路径移动到达目标点 中途停止不会广播 只有通过实体本身路径移动才会广播 单纯使用通用移动脚本直接移动的不会广播
    /// </summary>
    public Action OnEntityPathMoveArrived;
    /// <summary>
    /// 实体重生
    /// </summary>
    public Action EntityBeReborn;
}