using System;
using System.Collections.Generic;
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
    /// 千分位转整形
    /// </summary>
    public static readonly int T2I = 1000;
    /// <summary>
    /// 整形转千分位
    /// </summary>
    public static readonly float I2T = 1f / T2I;
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

    /// <summary>
    /// 不同权重的列表中随机一个索引
    /// </summary>
    public static int RandomWeightListIndex(List<int> weightList)
    {

        int total = 0;

        foreach (int elem in weightList)
        {
            total += elem;
        }

        int randomNum = UnityEngine.Random.Range(0, total);

        for (int i = 0; i < weightList.Count; i++)
        {
            if (randomNum < weightList[i])
            {
                return i;
            }
            else
            {
                randomNum -= weightList[i];
            }
        }
        return weightList.Count - 1;
    }

    /// <summary>
    /// 区域ID转土地格子ID
    /// </summary>
    public static ulong AreaToSoil(int areaId, int x, int z)
    {
        return ((ulong)areaId << 32) | ((ulong)x << 16) | (uint)z;
    }

    /// <summary>
    /// 土地格子ID转区域ID
    /// </summary>
    public static int SoilToArea(ulong pointId)
    {
        return (int)(pointId >> 32);
    }

    public static List<T> RandomSortList<T>(List<T> listT)
    {
        for (int i = 0; i < listT.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, listT.Count);
            (listT[randomIndex], listT[i]) = (listT[i], listT[randomIndex]);
        }
        return listT;
    }
}