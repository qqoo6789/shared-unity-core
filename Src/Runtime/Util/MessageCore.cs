using System;

/// <summary>
/// 共享库的消息中心
/// </summary>
public static class MessageCore
{
    /// <summary>
    /// 家园已使用的土地肥沃度发生变化 T0:土地唯一ID T1:变化的值 负数代表减少
    /// </summary>
    public static Action<ulong, int> HomeUsedSoilFertileChanged = delegate { };
}