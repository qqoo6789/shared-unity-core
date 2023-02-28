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
public class DRSlotLv : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id-int。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取slot-int。*/
    /// </summary>
    public int Slot
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
  /**获取exp-int。*/
    /// </summary>
    public int Exp
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
  /**获取hp-int。*/
    /// </summary>
    public int Hp
    {
        get;
        private set;
    }

    /// <summary>
  /**获取hpRecovery-int。*/
    /// </summary>
    public int HpRecovery
    {
        get;
        private set;
    }

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
  /**获取def-int。*/
    /// </summary>
    public int Def
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
  /**获取critDmg-int。*/
    /// </summary>
    public int CritDmg
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

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        Slot = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Lv = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Exp = DataTableParseUtil.ParseInt(columnStrings[index++]);
        UseMELD = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Hp = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HpRecovery = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Att = DataTableParseUtil.ParseInt(columnStrings[index++]);
        AttSpd = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Def = DataTableParseUtil.ParseInt(columnStrings[index++]);
        CritRate = DataTableParseUtil.ParseInt(columnStrings[index++]);
        CritDmg = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HitPoint = DataTableParseUtil.ParseInt(columnStrings[index++]);
        MissPoint = DataTableParseUtil.ParseInt(columnStrings[index++]);
        MoveSpeed = DataTableParseUtil.ParseInt(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                Slot = binaryReader.Read7BitEncodedInt32();
                Lv = binaryReader.Read7BitEncodedInt32();
                Exp = binaryReader.Read7BitEncodedInt32();
                UseMELD = binaryReader.Read7BitEncodedInt32();
                Hp = binaryReader.Read7BitEncodedInt32();
                HpRecovery = binaryReader.Read7BitEncodedInt32();
                Att = binaryReader.Read7BitEncodedInt32();
                AttSpd = binaryReader.Read7BitEncodedInt32();
                Def = binaryReader.Read7BitEncodedInt32();
                CritRate = binaryReader.Read7BitEncodedInt32();
                CritDmg = binaryReader.Read7BitEncodedInt32();
                HitPoint = binaryReader.Read7BitEncodedInt32();
                MissPoint = binaryReader.Read7BitEncodedInt32();
                MoveSpeed = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

