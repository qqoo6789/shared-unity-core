using System;
public static class TimeUtil
{
    /// <summary>
    /// 秒转毫秒
    /// </summary>
    public static readonly float S2MS = 1000;
    /// <summary>
    /// 毫秒转秒
    /// </summary>
    public static readonly float MS2S = 1 / S2MS;

    public static readonly DateTime DateForm = new(1970, 1, 1, 0, 0, 0, 0);

    public static long GetTimeStamp()
    {
        long currentTicks = DateTime.Now.Ticks;
        long curMs = (currentTicks - DateForm.Ticks) / 10000;
        return curMs;
    }
}