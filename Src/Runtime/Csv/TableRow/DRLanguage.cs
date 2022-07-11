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
public class DRLanguage : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取值。*/
    /// </summary>
    public string Value
    {
        get;
        private set;
    }

    /// <summary>
  /**获取模块。*/
    /// </summary>
    public string Module
    {
        get;
        private set;
    }

    /// <summary>
  /**获取特殊描述。*/
    /// </summary>
    public string Describe
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        Value = columnStrings[index++];
        Module = columnStrings[index++];
        Describe = columnStrings[index++];

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                Value = binaryReader.ReadString();
                Module = binaryReader.ReadString();
                Describe = binaryReader.ReadString();
            }
        }

        return true;
    }
}

