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

    public static readonly int SecondsOfMinute = 60;
    public static readonly int SecondsOfHour = 3600;
    public static readonly int SecondsOfDay = 86400;

    public static long GetTimeStamp()
    {
        long currentTicks = DateTime.UtcNow.Ticks;
        long curMs = (currentTicks - DateForm.Ticks) / 10000;
        return curMs;
    }

    public static long GetServerTimeStamp()
    {
        return GetTimeStamp();// todo 
        // TimeSpan ts = DateTime.UtcNow - DateForm;
        // return Convert.ToInt64(ts.TotalMilliseconds);
    }


    public static long DataTime2TimeStamp(DateTime dateTime)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(DateForm);
        TimeSpan ts = dateTime - startTime;
        return Convert.ToInt64(ts.TotalMilliseconds);
    }

    public static DateTime TimeStamp2DataTime(long timestamp)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(DateForm);
        DateTime curDateTime = startTime.AddMilliseconds(timestamp);
        return curDateTime;
    }


    // 获取当天结束的时间
    public static DateTime GetDayEndTime()
    {
        long curServerTimestamp = GetServerTimeStamp();
        DateTime curDateTime = TimeStamp2DataTime(curServerTimestamp);
        DateTime endDataTime = new(curDateTime.Year, curDateTime.Month, curDateTime.Day, 23, 59, 59);
        return endDataTime;
    }

    // 秒 转 时:分:秒 
    public static string SecondsToHMS(int nSeconds)
    {
        if (nSeconds < 0)
        {
            nSeconds = 0;
        }
        // int day = Convert.ToInt32(decimal.Floor(nSeconds / SecondsOfDay));
        nSeconds %= SecondsOfDay;
        int hour = Convert.ToInt32(decimal.Floor(nSeconds / SecondsOfHour));
        nSeconds %= SecondsOfHour;
        int minute = Convert.ToInt32(decimal.Floor(nSeconds / SecondsOfMinute));
        nSeconds %= SecondsOfMinute;
        return $"{hour.ToString().PadLeft(2, '0')}:{minute.ToString().PadLeft(2, '0')}:{nSeconds.ToString().PadLeft(2, '0')}";
    }
}