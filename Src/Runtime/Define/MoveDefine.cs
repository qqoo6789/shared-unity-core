/// <summary>
/// 移动相关的定义
/// </summary>
public static class MoveDefine
{
    /// <summary>
    /// 移动的步高 也就是能移动的直线高度
    /// </summary>
    public const float MOVE_STEP_HEIGHT = 0.3f;
    /// <summary>
    /// 最大爬坡度数
    /// </summary>
    public const float MOVE_SLOPE_LIMIT = 45f;
    /// <summary>
    /// 移动碰撞间隙 直接体现在了浮在地表的高度
    /// </summary>
    public const float MOVE_SKIN_WIDTH = 0.08f;

}