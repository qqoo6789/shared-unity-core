using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 自定义的一些数学工具
/// </summary>
public static class MathUtilCore
{
    /// <summary>
    /// 米转厘米
    /// </summary>
    public static readonly float M2CM = 100;
    /// <summary>
    /// 厘米转米
    /// </summary>
    public static readonly float CM2M = 1 / M2CM;
    /// <summary>
    /// 两个int转成一个ulong 方便将二维坐标转成一个key 类似对之前egret中的r_c的字符串优化
    /// </summary>
    public static ulong TwoIntToUlong(int a, int b)
    {
        return ((ulong)a << 32) | (uint)b;
    }

    /// <summary>
    /// 二维转的key还原回之前的二维坐标
    /// </summary>
    public static (int a, int b) UlongToTwoInt(ulong value)
    {
        return ((int)(value >> 32), (int)value);
    }

    /// <summary>
    /// string转long
    /// </summary>
    public static long StringToLong(string valueStr)
    {
        if (long.TryParse(valueStr, out long value))
        {
            return value;
        }
        return 0;
    }
}