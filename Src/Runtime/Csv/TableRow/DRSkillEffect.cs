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
public class DRSkillEffect : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取效果ID。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取效果类型 。*/
    /// </summary>
    public int EffectType
    {
        get;
        private set;
    }

    /// <summary>
  /**获取最大层数。*/
    /// </summary>
    public int MaxLayer
    {
        get;
        private set;
    }

    /// <summary>
  /**获取能否重复。*/
    /// </summary>
    public bool IsRepeat
    {
        get;
        private set;
    }

    /// <summary>
  /**获取效果标识。*/
    /// </summary>
    public int[] EffectFlag
    {
        get;
        private set;
    }

    /// <summary>
  /**获取效果免疫标识。*/
    /// </summary>
    public int[] EffectImmuneFlag
    {
        get;
        private set;
    }

    /// <summary>
  /**获取效果触发间隔（ms）。*/
    /// </summary>
    public int EffectInterval
    {
        get;
        private set;
    }

    /// <summary>
  /**获取效果参数。*/
    /// </summary>
    public int[] Parameters
    {
        get;
        private set;
    }

    /// <summary>
  /**获取效果参数2。*/
    /// </summary>
    public int[][] Parameters2
    {
        get;
        private set;
    }

    /// <summary>
  /**获取持续时间 <0为永久 =0为瞬间 >0持续时间。*/
    /// </summary>
    public int Duration
    {
        get;
        private set;
    }

    /// <summary>
  /**获取是否显示buff icon。*/
    /// </summary>
    public bool ShowBuffIcon
    {
        get;
        private set;
    }

    /// <summary>
  /**获取buff icon。*/
    /// </summary>
    public string BuffIcon
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        EffectType = DataTableParseUtil.ParseInt(columnStrings[index++]);
        MaxLayer = DataTableParseUtil.ParseInt(columnStrings[index++]);
        IsRepeat = DataTableParseUtil.ParseBool(columnStrings[index++]);
        EffectFlag = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        EffectImmuneFlag = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        EffectInterval = DataTableParseUtil.ParseInt(columnStrings[index++]);
        index++;
        Parameters = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        Parameters2 = DataTableParseUtil.ParseArrayList<int>(columnStrings[index++]);
        Duration = DataTableParseUtil.ParseInt(columnStrings[index++]);
        index++;
        ShowBuffIcon = DataTableParseUtil.ParseBool(columnStrings[index++]);
        BuffIcon = columnStrings[index++];

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                EffectType = binaryReader.Read7BitEncodedInt32();
                MaxLayer = binaryReader.Read7BitEncodedInt32();
                IsRepeat = binaryReader.ReadBoolean();
                EffectFlag = binaryReader.ReadArray<Int32>();
                EffectImmuneFlag = binaryReader.ReadArray<Int32>();
                EffectInterval = binaryReader.Read7BitEncodedInt32();
                Parameters = binaryReader.ReadArray<Int32>();
                Parameters2 = binaryReader.ReadArrayList<Int32>();
                Duration = binaryReader.Read7BitEncodedInt32();
                ShowBuffIcon = binaryReader.ReadBoolean();
                BuffIcon = binaryReader.ReadString();
            }
        }

        return true;
    }
}

