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
public class DRCraftSkill : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id-int。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取skillId-int。*/
    /// </summary>
    public int SkillId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取level-int。*/
    /// </summary>
    public int Level
    {
        get;
        private set;
    }

    /// <summary>
  /**获取icon-string。*/
    /// </summary>
    public string Icon
    {
        get;
        private set;
    }

    /// <summary>
  /**获取roleLevel-int。*/
    /// </summary>
    public int RoleLevel
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillExp-int。*/
    /// </summary>
    public int SkillExp
    {
        get;
        private set;
    }

    /// <summary>
  /**获取useMELD-int。*/
    /// </summary>
    public int UseMELD
    {
        get;
        private set;
    }

    /// <summary>
  /**获取useExp-int。*/
    /// </summary>
    public int UseExp
    {
        get;
        private set;
    }

    /// <summary>
  /**获取recipeIds-int[]。*/
    /// </summary>
    public int[] RecipeIds
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        SkillId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Level = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Icon = columnStrings[index++];
        RoleLevel = DataTableParseUtil.ParseInt(columnStrings[index++]);
        SkillExp = DataTableParseUtil.ParseInt(columnStrings[index++]);
        UseMELD = DataTableParseUtil.ParseInt(columnStrings[index++]);
        UseExp = DataTableParseUtil.ParseInt(columnStrings[index++]);
        RecipeIds = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                SkillId = binaryReader.Read7BitEncodedInt32();
                Level = binaryReader.Read7BitEncodedInt32();
                Icon = binaryReader.ReadString();
                RoleLevel = binaryReader.Read7BitEncodedInt32();
                SkillExp = binaryReader.Read7BitEncodedInt32();
                UseMELD = binaryReader.Read7BitEncodedInt32();
                UseExp = binaryReader.Read7BitEncodedInt32();
                RecipeIds = binaryReader.ReadArray<Int32>();
            }
        }

        return true;
    }
}

