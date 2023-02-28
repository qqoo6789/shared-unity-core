//------------------------------------------------------------
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
public class DRTask : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id-int。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取level-int。*/
    /// </summary>
    public int Level
    {
        get;
        private set;
    }

    /// <summary>
  /**获取name-string。*/
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
  /**获取decs-string。*/
    /// </summary>
    public string Decs
    {
        get;
        private set;
    }

    /// <summary>
  /**获取details-string。*/
    /// </summary>
    public string Details
    {
        get;
        private set;
    }

    /// <summary>
  /**获取subSystem-int[]。*/
    /// </summary>
    public int[] SubSystem
    {
        get;
        private set;
    }

    /// <summary>
  /**获取designateOptions-string。*/
    /// </summary>
    public string DesignateOptions
    {
        get;
        private set;
    }

    /// <summary>
  /**获取chanceOptions-string。*/
    /// </summary>
    public string ChanceOptions
    {
        get;
        private set;
    }

    /// <summary>
  /**获取itemReward-int。*/
    /// </summary>
    public int ItemReward
    {
        get;
        private set;
    }

    /// <summary>
  /**获取expReward-int。*/
    /// </summary>
    public int ExpReward
    {
        get;
        private set;
    }

    /// <summary>
  /**获取difficulty-int。*/
    /// </summary>
    public int Difficulty
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
        Name = columnStrings[index++];
        Decs = columnStrings[index++];
        Details = columnStrings[index++];
        SubSystem = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        DesignateOptions = columnStrings[index++];
        ChanceOptions = columnStrings[index++];
        ItemReward = DataTableParseUtil.ParseInt(columnStrings[index++]);
        ExpReward = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Difficulty = DataTableParseUtil.ParseInt(columnStrings[index++]);

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
                Name = binaryReader.ReadString();
                Decs = binaryReader.ReadString();
                Details = binaryReader.ReadString();
                SubSystem = binaryReader.ReadArray<Int32>();
                DesignateOptions = binaryReader.ReadString();
                ChanceOptions = binaryReader.ReadString();
                ItemReward = binaryReader.Read7BitEncodedInt32();
                ExpReward = binaryReader.Read7BitEncodedInt32();
                Difficulty = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

