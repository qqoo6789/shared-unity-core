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

    public static long GetTimeStampByInputString(string inputStr)
    {
        string[] strList = inputStr.Split("-");
        int[] timeList = new int[6];
        for (int i = 0; i < timeList.Length; i++)
        {
            if (i < strList.Length && int.TryParse(strList[i], out int time))
            {
                timeList[i] = time;
            }
            else
            {
                timeList[i] = 0;
            }
        }
        DateTime curDateTime = new(timeList[0], timeList[1], timeList[2], timeList[3], timeList[4], timeList[5], 0, DateTimeKind.Utc);
        return DataTime2TimeStamp(curDateTime);
    }
    public static long DataTime2TimeStamp(DateTime dateTime)
    {
        TimeSpan ts = dateTime - DateForm;
        return Convert.ToInt64(ts.TotalMilliseconds);
    }

    public static DateTime TimeStamp2DataTime(long timestamp)
    {
        DateTime curDateTime = DateForm.AddMilliseconds(timestamp);
        return curDateTime;
    }

    public static long GetServerTimeStamp()
    {
        // return GetTimeStamp();// todo 
        TimeSpan ts = DateTime.UtcNow - DateForm;
        return Convert.ToInt64(ts.TotalMilliseconds);
    }

    // 获取当天结束的时间
    public static DateTime GetDayEndTime()
    {
        long curServerTimestamp = GetServerTimeStamp();
        DateTime curDateTime = TimeStamp2DataTime(curServerTimestamp);
        DateTime endDataTime = new(curDateTime.Year, curDateTime.Month, curDateTime.Day, 23, 59, 59);
        return endDataTime;
    }

    // 获取本周结束的时间(周六)
    // public static DateTime GetWeekEndTime()
    // {
    //     long curServerTimestamp = GetServerTimeStamp();
    //     DateTime dayEndDataTime = TimeStamp2DataTime(curServerTimestamp);
    //     int offsetDay = DayOfWeek.Saturday - dayEndDataTime.DayOfWeek;
    //     DateTime weekEndDataTime = dayEndDataTime.AddDays(offsetDay);
    //     return weekEndDataTime;
    // }

    // 秒 转 时:分:秒 
    public static string SecondsToHMS(int nSeconds)
    {
        if (nSeconds < 0)
        {
            nSeconds = 0;
        }
        // int day = Convert.ToInt32(decimal.Floor(nSeconds / SecondsOfDay));

        int hour = Convert.ToInt32(decimal.Floor(nSeconds / SecondsOfHour));
        nSeconds %= SecondsOfHour;
        int minute = Convert.ToInt32(decimal.Floor(nSeconds / SecondsOfMinute));
        nSeconds %= SecondsOfMinute;
        return $"{hour.ToString().PadLeft(2, '0')}:{minute.ToString().PadLeft(2, '0')}:{nSeconds.ToString().PadLeft(2, '0')}";
    }

}