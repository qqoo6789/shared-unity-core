using UnityEngine;

/// <summary>
/// 实体上的内部消息组件 必然存在在实体上 里面都是各种消息Action 具体消息执行时需要加上?Invoke 判断是否为空 为了节省性能初始化不会分配事件初始值
/// </summary>
public class EntityEvent : MonoBehaviour
{
}