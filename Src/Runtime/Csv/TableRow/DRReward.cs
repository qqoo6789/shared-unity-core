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
public class DRReward : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取奖励ID。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取物品1。*/
    /// </summary>
    public int Item1ObjectId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取数量1。*/
    /// </summary>
    public int Item1Quality
    {
        get;
        private set;
    }

    /// <summary>
  /**获取物品2。*/
    /// </summary>
    public int Item2ObjectId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取数量2。*/
    /// </summary>
    public int Item2Quality
    {
        get;
        private set;
    }

    /// <summary>
  /**获取物品3。*/
    /// </summary>
    public int Item3ObjectId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取数量3。*/
    /// </summary>
    public int Item3Quality
    {
        get;
        private set;
    }

    /// <summary>
  /**获取物品4。*/
    /// </summary>
    public int Item4ObjectId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取数量4。*/
    /// </summary>
    public int Item4Quality
    {
        get;
        private set;
    }

    /// <summary>
  /**获取exp。*/
    /// </summary>
    public int Exp
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        index++;
        index++;
        index++;
        Item1ObjectId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Item1Quality = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Item2ObjectId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Item2Quality = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Item3ObjectId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Item3Quality = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Item4ObjectId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Item4Quality = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Exp = DataTableParseUtil.ParseInt(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                Item1ObjectId = binaryReader.Read7BitEncodedInt32();
                Item1Quality = binaryReader.Read7BitEncodedInt32();
                Item2ObjectId = binaryReader.Read7BitEncodedInt32();
                Item2Quality = binaryReader.Read7BitEncodedInt32();
                Item3ObjectId = binaryReader.Read7BitEncodedInt32();
                Item3Quality = binaryReader.Read7BitEncodedInt32();
                Item4ObjectId = binaryReader.Read7BitEncodedInt32();
                Item4Quality = binaryReader.Read7BitEncodedInt32();
                Exp = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

