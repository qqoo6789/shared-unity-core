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
public class DRItem : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取物品ID。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取物品名称。*/
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
  /**获取物品描述。*/
    /// </summary>
    public string Desc
    {
        get;
        private set;
    }

    /// <summary>
  /**获取物品类型。*/
    /// </summary>
    public int Type
    {
        get;
        private set;
    }

    /// <summary>
  /**获取物品品质。*/
    /// </summary>
    public int[] Quality
    {
        get;
        private set;
    }

    /// <summary>
  /**获取物品图标。*/
    /// </summary>
    public string Icon
    {
        get;
        private set;
    }

    /// <summary>
  /**获取背包显示分类。*/
    /// </summary>
    public int BagShowType
    {
        get;
        private set;
    }

    /// <summary>
  /**获取使用等级。*/
    /// </summary>
    public int UseLv
    {
        get;
        private set;
    }

    /// <summary>
  /**获取使用者类型。*/
    /// </summary>
    public int UserType
    {
        get;
        private set;
    }

    /// <summary>
  /**获取是否NFT。*/
    /// </summary>
    public int CanMint
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
        index++;
        Desc = columnStrings[index++];
        Type = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Quality = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        Icon = columnStrings[index++];
        BagShowType = DataTableParseUtil.ParseInt(columnStrings[index++]);
        UseLv = DataTableParseUtil.ParseInt(columnStrings[index++]);
        UserType = DataTableParseUtil.ParseInt(columnStrings[index++]);
        CanMint = DataTableParseUtil.ParseInt(columnStrings[index++]);

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
                Type = binaryReader.Read7BitEncodedInt32();
                Quality = binaryReader.ReadArray<Int32>();
                Icon = binaryReader.ReadString();
                BagShowType = binaryReader.Read7BitEncodedInt32();
                UseLv = binaryReader.Read7BitEncodedInt32();
                UserType = binaryReader.Read7BitEncodedInt32();
                CanMint = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

