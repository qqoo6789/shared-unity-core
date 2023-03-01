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
public class DRMonster : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id-int。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取att-int。*/
    /// </summary>
    public int Att
    {
        get;
        private set;
    }

    /// <summary>
  /**获取attSpd-int。*/
    /// </summary>
    public int AttSpd
    {
        get;
        private set;
    }

    /// <summary>
  /**获取bodyCapacity-int。*/
    /// </summary>
    public int BodyCapacity
    {
        get;
        private set;
    }

    /// <summary>
  /**获取combatDist-int。*/
    /// </summary>
    public int CombatDist
    {
        get;
        private set;
    }

    /// <summary>
  /**获取critDmg-int。*/
    /// </summary>
    public int CritDmg
    {
        get;
        private set;
    }

    /// <summary>
  /**获取critRate-int。*/
    /// </summary>
    public int CritRate
    {
        get;
        private set;
    }

    /// <summary>
  /**获取def-int。*/
    /// </summary>
    public int Def
    {
        get;
        private set;
    }

    /// <summary>
  /**获取desc-string。*/
    /// </summary>
    public string Desc
    {
        get;
        private set;
    }

    /// <summary>
  /**获取dropId-int。*/
    /// </summary>
    public int DropId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取exp-int。*/
    /// </summary>
    public int Exp
    {
        get;
        private set;
    }

    /// <summary>
  /**获取harvestDropId-int。*/
    /// </summary>
    public int HarvestDropId
    {
        get;
        private set;
    }

    /// <summary>
  /**获取harvestTime-int。*/
    /// </summary>
    public int HarvestTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取hitPoint-int。*/
    /// </summary>
    public int HitPoint
    {
        get;
        private set;
    }

    /// <summary>
  /**获取hp-int。*/
    /// </summary>
    public int Hp
    {
        get;
        private set;
    }

    /// <summary>
  /**获取hungerSpeed-int。*/
    /// </summary>
    public int HungerSpeed
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
  /**获取lockEnemyRange-int。*/
    /// </summary>
    public int LockEnemyRange
    {
        get;
        private set;
    }

    /// <summary>
  /**获取lv-int。*/
    /// </summary>
    public int Lv
    {
        get;
        private set;
    }

    /// <summary>
  /**获取maxHunger-int。*/
    /// </summary>
    public int MaxHunger
    {
        get;
        private set;
    }

    /// <summary>
  /**获取missPoint-int。*/
    /// </summary>
    public int MissPoint
    {
        get;
        private set;
    }

    /// <summary>
  /**获取moveSpeed-int。*/
    /// </summary>
    public int MoveSpeed
    {
        get;
        private set;
    }

    /// <summary>
  /**获取name-string。*/
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
  /**获取pushDist-int。*/
    /// </summary>
    public int PushDist
    {
        get;
        private set;
    }

    /// <summary>
  /**获取pushDmg-int。*/
    /// </summary>
    public int PushDmg
    {
        get;
        private set;
    }

    /// <summary>
  /**获取roleAssetID-int。*/
    /// </summary>
    public int RoleAssetID
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillCastPool-int[][]。*/
    /// </summary>
    public int[][] SkillCastPool
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        Att = DataTableParseUtil.ParseInt(columnStrings[index++]);
        AttSpd = DataTableParseUtil.ParseInt(columnStrings[index++]);
        BodyCapacity = DataTableParseUtil.ParseInt(columnStrings[index++]);
        CombatDist = DataTableParseUtil.ParseInt(columnStrings[index++]);
        CritDmg = DataTableParseUtil.ParseInt(columnStrings[index++]);
        CritRate = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Def = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Desc = columnStrings[index++];
        DropId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Exp = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HarvestDropId = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HarvestTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HitPoint = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Hp = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HungerSpeed = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Icon = columnStrings[index++];
        _id = int.Parse(columnStrings[index++]);
        LockEnemyRange = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Lv = DataTableParseUtil.ParseInt(columnStrings[index++]);
        MaxHunger = DataTableParseUtil.ParseInt(columnStrings[index++]);
        MissPoint = DataTableParseUtil.ParseInt(columnStrings[index++]);
        MoveSpeed = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Name = columnStrings[index++];
        PushDist = DataTableParseUtil.ParseInt(columnStrings[index++]);
        PushDmg = DataTableParseUtil.ParseInt(columnStrings[index++]);
        RoleAssetID = DataTableParseUtil.ParseInt(columnStrings[index++]);
        SkillCastPool = DataTableParseUtil.ParseArrayList<int>(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                Att = binaryReader.Read7BitEncodedInt32();
                AttSpd = binaryReader.Read7BitEncodedInt32();
                BodyCapacity = binaryReader.Read7BitEncodedInt32();
                CombatDist = binaryReader.Read7BitEncodedInt32();
                CritDmg = binaryReader.Read7BitEncodedInt32();
                CritRate = binaryReader.Read7BitEncodedInt32();
                Def = binaryReader.Read7BitEncodedInt32();
                Desc = binaryReader.ReadString();
                DropId = binaryReader.Read7BitEncodedInt32();
                Exp = binaryReader.Read7BitEncodedInt32();
                HarvestDropId = binaryReader.Read7BitEncodedInt32();
                HarvestTime = binaryReader.Read7BitEncodedInt32();
                HitPoint = binaryReader.Read7BitEncodedInt32();
                Hp = binaryReader.Read7BitEncodedInt32();
                HungerSpeed = binaryReader.Read7BitEncodedInt32();
                Icon = binaryReader.ReadString();
                _id = binaryReader.Read7BitEncodedInt32();
                LockEnemyRange = binaryReader.Read7BitEncodedInt32();
                Lv = binaryReader.Read7BitEncodedInt32();
                MaxHunger = binaryReader.Read7BitEncodedInt32();
                MissPoint = binaryReader.Read7BitEncodedInt32();
                MoveSpeed = binaryReader.Read7BitEncodedInt32();
                Name = binaryReader.ReadString();
                PushDist = binaryReader.Read7BitEncodedInt32();
                PushDmg = binaryReader.Read7BitEncodedInt32();
                RoleAssetID = binaryReader.Read7BitEncodedInt32();
                SkillCastPool = binaryReader.ReadArrayList<Int32>();
            }
        }

        return true;
    }
}

