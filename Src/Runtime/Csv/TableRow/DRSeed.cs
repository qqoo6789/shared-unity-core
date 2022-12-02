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
public class DRSeed : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取描述。*/
    /// </summary>
    public string Desc
    {
        get;
        private set;
    }

    /// <summary>
  /**获取道具ID。*/
    /// </summary>
    public int ItemId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取生长阶段形象。*/
    /// </summary>
    public string[] GrowRes
    {
        get;
        private set;
    }

    /// <summary>
  /**获取总生长时间 秒。*/
    /// </summary>
    public int GrowTotalTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取枯萎时间 秒。*/
    /// </summary>
    public int WitherTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取成熟形象。*/
    /// </summary>
    public string HarvestRes
    {
        get;
        private set;
    }

    /// <summary>
  /**获取收获产物。*/
    /// </summary>
    public int[] Product
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        Desc = columnStrings[index++];
        ItemId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        GrowRes = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        GrowTotalTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        WitherTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HarvestRes = columnStrings[index++];
        Product = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                Desc = binaryReader.ReadString();
                ItemId = binaryReader.Read7BitEncodedInt32();
                GrowRes = binaryReader.ReadArray<String>();
                GrowTotalTime = binaryReader.Read7BitEncodedInt32();
                WitherTime = binaryReader.Read7BitEncodedInt32();
                HarvestRes = binaryReader.ReadString();
                Product = binaryReader.ReadArray<Int32>();
            }
        }

        return true;
    }
}

