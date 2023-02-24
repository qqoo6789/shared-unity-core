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
public class DRAvatar : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id-int。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取resouceBoy-string。*/
    /// </summary>
    public string ResouceBoy
    {
        get;
        private set;
    }

    /// <summary>
  /**获取avatarType-int。*/
    /// </summary>
    public int AvatarType
    {
        get;
        private set;
    }

    /// <summary>
  /**获取resouceBoyIcon-string。*/
    /// </summary>
    public string ResouceBoyIcon
    {
        get;
        private set;
    }

    /// <summary>
  /**获取resouceGirl-string。*/
    /// </summary>
    public string ResouceGirl
    {
        get;
        private set;
    }

    /// <summary>
  /**获取resouceGirlIcon-string。*/
    /// </summary>
    public string ResouceGirlIcon
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        ResouceBoy = columnStrings[index++];
        AvatarType = DataTableParseUtil.ParseInt(columnStrings[index++]);
        ResouceBoyIcon = columnStrings[index++];
        ResouceGirl = columnStrings[index++];
        ResouceGirlIcon = columnStrings[index++];

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                ResouceBoy = binaryReader.ReadString();
                AvatarType = binaryReader.Read7BitEncodedInt32();
                ResouceBoyIcon = binaryReader.ReadString();
                ResouceGirl = binaryReader.ReadString();
                ResouceGirlIcon = binaryReader.ReadString();
            }
        }

        return true;
    }
}

