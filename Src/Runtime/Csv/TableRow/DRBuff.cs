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
public class DRBuff : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id-int。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取buffEffect-int。*/
    /// </summary>
    public int BuffEffect
    {
        get;
        private set;
    }

    /// <summary>
  /**获取buffGroupId-int。*/
    /// </summary>
    public int BuffGroupId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取buffIcon-string。*/
    /// </summary>
    public string BuffIcon
    {
        get;
        private set;
    }

    /// <summary>
  /**获取buffPara-int[]。*/
    /// </summary>
    public int[] BuffPara
    {
        get;
        private set;
    }

    /// <summary>
  /**获取buffPriority-int。*/
    /// </summary>
    public int BuffPriority
    {
        get;
        private set;
    }

    /// <summary>
  /**获取totleTime-int。*/
    /// </summary>
    public int TotleTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取triggerInterval-int。*/
    /// </summary>
    public int TriggerInterval
    {
        get;
        private set;
    }

    /// <summary>
  /**获取buffAnimation-string。*/
    /// </summary>
    public string BuffAnimation
    {
        get;
        private set;
    }

    /// <summary>
  /**获取buffDesc-string。*/
    /// </summary>
    public string BuffDesc
    {
        get;
        private set;
    }

    /// <summary>
  /**获取buffName-string。*/
    /// </summary>
    public string BuffName
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        BuffEffect = DataTableParseUtil.ParseInt(columnStrings[index++]);
        BuffGroupId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        BuffIcon = columnStrings[index++];
        BuffPara = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        BuffPriority = DataTableParseUtil.ParseInt(columnStrings[index++]);
        _id = int.Parse(columnStrings[index++]);
        TotleTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        TriggerInterval = DataTableParseUtil.ParseInt(columnStrings[index++]);
        BuffAnimation = columnStrings[index++];
        BuffDesc = columnStrings[index++];
        BuffName = columnStrings[index++];

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                BuffEffect = binaryReader.Read7BitEncodedInt32();
                BuffGroupId = binaryReader.Read7BitEncodedInt32();
                BuffIcon = binaryReader.ReadString();
                BuffPara = binaryReader.ReadArray<Int32>();
                BuffPriority = binaryReader.Read7BitEncodedInt32();
                _id = binaryReader.Read7BitEncodedInt32();
                TotleTime = binaryReader.Read7BitEncodedInt32();
                TriggerInterval = binaryReader.Read7BitEncodedInt32();
                BuffAnimation = binaryReader.ReadString();
                BuffDesc = binaryReader.ReadString();
                BuffName = binaryReader.ReadString();
            }
        }

        return true;
    }
}

