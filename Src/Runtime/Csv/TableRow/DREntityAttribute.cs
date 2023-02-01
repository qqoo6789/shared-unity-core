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
public class DREntityAttribute : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取属性ID。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取属性名字。*/
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
  /**获取属性类型 。*/
    /// </summary>
    public int Type
    {
        get;
        private set;
    }

    /// <summary>
  /**获取属性值类型。*/
    /// </summary>
    public int ValueType
    {
        get;
        private set;
    }

    /// <summary>
  /**获取默认值。*/
    /// </summary>
    public int DefaultValue
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        Name = columnStrings[index++];
        Type = DataTableParseUtil.ParseInt(columnStrings[index++]);
        ValueType = DataTableParseUtil.ParseInt(columnStrings[index++]);
        DefaultValue = DataTableParseUtil.ParseInt(columnStrings[index++]);
        index++;

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                Name = binaryReader.ReadString();
                Type = binaryReader.Read7BitEncodedInt32();
                ValueType = binaryReader.Read7BitEncodedInt32();
                DefaultValue = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

