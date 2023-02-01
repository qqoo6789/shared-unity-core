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
public class DRHomeResources : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取名字。*/
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
  /**获取资源的描述。*/
    /// </summary>
    public string Desc
    {
        get;
        private set;
    }

    /// <summary>
  /**获取资源的Icon。*/
    /// </summary>
    public string Icon
    {
        get;
        private set;
    }

    /// <summary>
  /**获取资产名称。*/
    /// </summary>
    public string AssetRes
    {
        get;
        private set;
    }

    /// <summary>
  /**获取掉落Id。*/
    /// </summary>
    public int DropId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取采集家园动作。*/
    /// </summary>
    public int HomeAction
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
        Desc = columnStrings[index++];
        Icon = columnStrings[index++];
        AssetRes = columnStrings[index++];
        DropId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HomeAction = DataTableParseUtil.ParseInt(columnStrings[index++]);

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
                Desc = binaryReader.ReadString();
                Icon = binaryReader.ReadString();
                AssetRes = binaryReader.ReadString();
                DropId = binaryReader.Read7BitEncodedInt32();
                HomeAction = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

