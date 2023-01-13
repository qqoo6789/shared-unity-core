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
public class DRTalentTree : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取节点id。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取专精类型。*/
    /// </summary>
    public int Type
    {
        get;
        private set;
    }

    /// <summary>
  /**获取isTrunk。*/
    /// </summary>
    public bool IsTrunk
    {
        get;
        private set;
    }

    /// <summary>
  /**获取解锁所需的天赋树等级
非主干技能配置都为0。*/
    /// </summary>
    public int UpgradeRequireTreeLv
    {
        get;
        private set;
    }

    /// <summary>
  /**获取节点层级。*/
    /// </summary>
    public int Layer
    {
        get;
        private set;
    }

    /// <summary>
  /**获取节点等级上限。*/
    /// </summary>
    public int LvLimit
    {
        get;
        private set;
    }

    /// <summary>
  /**获取天赋收益。*/
    /// </summary>
    public int Gains
    {
        get;
        private set;
    }

    /// <summary>
  /**获取收益参数。*/
    /// </summary>
    public int[][] GainsArgs
    {
        get;
        private set;
    }

    /// <summary>
  /**获取节点升级经验。*/
    /// </summary>
    public int[] UpgradeEXP
    {
        get;
        private set;
    }

    /// <summary>
  /**获取前置节点。*/
    /// </summary>
    public int[] PreNode
    {
        get;
        private set;
    }

    /// <summary>
  /**获取后置技能。*/
    /// </summary>
    public int[] PostNode
    {
        get;
        private set;
    }

    /// <summary>
  /**获取节点列号。*/
    /// </summary>
    public int Col
    {
        get;
        private set;
    }

    /// <summary>
  /**获取name。*/
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
  /**获取desc。*/
    /// </summary>
    public string Desc
    {
        get;
        private set;
    }

    /// <summary>
  /**获取icon。*/
    /// </summary>
    public string Icon
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        Type = DataTableParseUtil.ParseInt(columnStrings[index++]);
        IsTrunk = DataTableParseUtil.ParseBool(columnStrings[index++]);
        UpgradeRequireTreeLv = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Layer = DataTableParseUtil.ParseInt(columnStrings[index++]);
        LvLimit = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Gains = DataTableParseUtil.ParseInt(columnStrings[index++]);
        GainsArgs = DataTableParseUtil.ParseArrayList<int>(columnStrings[index++]);
        UpgradeEXP = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        PreNode = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        PostNode = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        Col = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Name = columnStrings[index++];
        Desc = columnStrings[index++];
        Icon = columnStrings[index++];

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                Type = binaryReader.Read7BitEncodedInt32();
                IsTrunk = binaryReader.ReadBoolean();
                UpgradeRequireTreeLv = binaryReader.Read7BitEncodedInt32();
                Layer = binaryReader.Read7BitEncodedInt32();
                LvLimit = binaryReader.Read7BitEncodedInt32();
                Gains = binaryReader.Read7BitEncodedInt32();
                GainsArgs = binaryReader.ReadArrayList<Int32>();
                UpgradeEXP = binaryReader.ReadArray<Int32>();
                PreNode = binaryReader.ReadArray<Int32>();
                PostNode = binaryReader.ReadArray<Int32>();
                Col = binaryReader.Read7BitEncodedInt32();
                Name = binaryReader.ReadString();
                Desc = binaryReader.ReadString();
                Icon = binaryReader.ReadString();
            }
        }

        return true;
    }
}

