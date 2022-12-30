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
public class DRSkillTree : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id。*/
    /// </summary>
    public override int Id => _id;

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

    /// <summary>
  /**获取preSkill。*/
    /// </summary>
    public int[] PreSkill
    {
        get;
        private set;
    }

    /// <summary>
  /**获取nextSkill。*/
    /// </summary>
    public int[] NextSkill
    {
        get;
        private set;
    }

    /// <summary>
  /**获取pos。*/
    /// </summary>
    public int[] Pos
    {
        get;
        private set;
    }

    /// <summary>
  /**获取turnPoint。*/
    /// </summary>
    public int[] TurnPoint
    {
        get;
        private set;
    }

    /// <summary>
  /**获取unlockLayer。*/
    /// </summary>
    public int UnlockLayer
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

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        Name = columnStrings[index++];
        Desc = columnStrings[index++];
        Icon = columnStrings[index++];
        PreSkill = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        NextSkill = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        Pos = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        TurnPoint = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        UnlockLayer = DataTableParseUtil.ParseInt(columnStrings[index++]);
        IsTrunk = DataTableParseUtil.ParseBool(columnStrings[index++]);

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
                PreSkill = binaryReader.ReadArray<Int32>();
                NextSkill = binaryReader.ReadArray<Int32>();
                Pos = binaryReader.ReadArray<Int32>();
                TurnPoint = binaryReader.ReadArray<Int32>();
                UnlockLayer = binaryReader.Read7BitEncodedInt32();
                IsTrunk = binaryReader.ReadBoolean();
            }
        }

        return true;
    }
}

