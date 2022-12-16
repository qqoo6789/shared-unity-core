/*
 * @Author: xiang huan
 * @Date: 2022-12-02 13:54:41
 * @Description: 区域定义
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/Define/SceneAreaDefine.cs
 * 
 */


/// <summary>
/// 场景区域定义
/// </summary>
public enum eSceneArea : int
{
    world = 10001,  //大世界
    home = 10002,   //家园
}

/// <summary>
/// 区域优先级
/// 用于区域重叠时的优先级判断
/// 枚举值越大，优先级越高
/// </summary>
public enum eSceneAreaPriority
{
    none,
    low,
    middle,
    high,
}

/// <summary>
/// 区域改变类型
/// </summary>
public enum eAreaChangedType
{
    enter,
    exit,
}