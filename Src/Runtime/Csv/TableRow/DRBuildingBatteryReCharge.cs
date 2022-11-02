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
public class DRBuildingBatteryReCharge : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取充值电量。*/
    /// </summary>
    public int Battery
    {
        get;
        private set;
    }

    /// <summary>
  /**获取赠送电量。*/
    /// </summary>
    public int PresentBattery
    {
        get;
        private set;
    }

    /// <summary>
  /**获取token消耗。*/
    /// </summary>
    public int TokenCost
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        Battery = DataTableParseUtil.ParseInt(columnStrings[index++]);
        PresentBattery = DataTableParseUtil.ParseInt(columnStrings[index++]);
        TokenCost = DataTableParseUtil.ParseInt(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                Battery = binaryReader.Read7BitEncodedInt32();
                PresentBattery = binaryReader.Read7BitEncodedInt32();
                TokenCost = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

