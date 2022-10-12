/// <summary>
/// 移动相关的定义
/// </summary>
public static class MoveDefine
{
    /// <summary>
    /// 走直线时的最大请求间隔时间 s
    /// </summary>
    public const float MOVE_MAX_SYNC_INTERVAL_TIME = 0.5f;
    /// <summary>
    /// 允许的网络延迟时间 s
    /// </summary>
    public const float MOVE_ALLOW_NETWORK_DELAY_TIME = 0.1f;
    /// <summary>
    /// 移动的步高 也就是能移动的直线高度
    /// </summary>
    public const float MOVE_STEP_HEIGHT = 0.3f;
    /// <summary>
    /// 最大爬坡度数
    /// </summary>
    public const float MOVE_SLOPE_LIMIT = 45f;
}