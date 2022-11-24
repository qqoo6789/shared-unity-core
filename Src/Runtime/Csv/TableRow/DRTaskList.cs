﻿//------------------------------------------------------------
// 此文件由工具自动生成
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;


/// <summary>
/** __DATA_TABLE_COMMENT__*/
/// </summary>
public class DRTaskList : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取任务链ID。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取任务链解锁等级。*/
    /// </summary>
    public int Level
    {
        get;
        private set;
    }

    /// <summary>
  /**获取任务链体系。*/
    /// </summary>
    public int System
    {
        get;
        private set;
    }

    /// <summary>
  /**获取随机任务池及权重。*/
    /// </summary>
    public int[][] TaskPool
    {
        get;
        private set;
    }

    /// <summary>
  /**获取任务序列。*/
    /// </summary>
    public int[] TaskSequence
    {
        get;
        private set;
    }

    /// <summary>
  /**获取任务链道具奖励。*/
    /// </summary>
    public int ItemReward
    {
        get;
        private set;
    }

    /// <summary>
  /**获取任务链Exp奖励。*/
    /// </summary>
    public int ExpReward
    {
        get;
        private set;
    }

    /// <summary>
  /**获取刷新是否重置进度。*/
    /// </summary>
    public int ProgressReset
    {
        get;
        private set;
    }

    /// <summary>
  /**获取领取花费（MELD）。*/
    /// </summary>
    public int CostMELD
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        Level = DataTableParseUtil.ParseInt(columnStrings[index++]);
        System = DataTableParseUtil.ParseInt(columnStrings[index++]);
        TaskPool = DataTableParseUtil.ParseArrayList<int>(columnStrings[index++]);
        TaskSequence = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        ItemReward = DataTableParseUtil.ParseInt(columnStrings[index++]);
        ExpReward = DataTableParseUtil.ParseInt(columnStrings[index++]);
        ProgressReset = DataTableParseUtil.ParseInt(columnStrings[index++]);
        CostMELD = DataTableParseUtil.ParseInt(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                Level = binaryReader.Read7BitEncodedInt32();
                System = binaryReader.Read7BitEncodedInt32();
                TaskPool = binaryReader.ReadArrayList<Int32>();
                TaskSequence = binaryReader.ReadArray<Int32>();
                ItemReward = binaryReader.Read7BitEncodedInt32();
                ExpReward = binaryReader.Read7BitEncodedInt32();
                ProgressReset = binaryReader.Read7BitEncodedInt32();
                CostMELD = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

