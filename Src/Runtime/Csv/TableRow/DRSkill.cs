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
public class DRSkill : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id-int。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取accuBreakable-bool。*/
    /// </summary>
    public bool AccuBreakable
    {
        get;
        private set;
    }

    /// <summary>
  /**获取accuSound-string[]。*/
    /// </summary>
    public string[] AccuSound
    {
        get;
        private set;
    }

    /// <summary>
  /**获取accuTime-int。*/
    /// </summary>
    public int AccuTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取attackCanMove-bool。*/
    /// </summary>
    public bool AttackCanMove
    {
        get;
        private set;
    }

    /// <summary>
  /**获取effectEnemy-int[]。*/
    /// </summary>
    public int[] EffectEnemy
    {
        get;
        private set;
    }

    /// <summary>
  /**获取effectForward-int[]。*/
    /// </summary>
    public int[] EffectForward
    {
        get;
        private set;
    }

    /// <summary>
  /**获取forwardReleaseTime-int。*/
    /// </summary>
    public int ForwardReleaseTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取isAutoUse-bool。*/
    /// </summary>
    public bool IsAutoUse
    {
        get;
        private set;
    }

    /// <summary>
  /**获取isHoldSkill-bool。*/
    /// </summary>
    public bool IsHoldSkill
    {
        get;
        private set;
    }

    /// <summary>
  /**获取isRemote-bool。*/
    /// </summary>
    public bool IsRemote
    {
        get;
        private set;
    }

    /// <summary>
  /**获取rangeTips-bool。*/
    /// </summary>
    public bool RangeTips
    {
        get;
        private set;
    }

    /// <summary>
  /**获取releaseSound-string[]。*/
    /// </summary>
    public string[] ReleaseSound
    {
        get;
        private set;
    }

    /// <summary>
  /**获取releaseTime-int。*/
    /// </summary>
    public int ReleaseTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillCD-int。*/
    /// </summary>
    public int SkillCD
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillDistance-int。*/
    /// </summary>
    public int SkillDistance
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillFlag-int[]。*/
    /// </summary>
    public int[] SkillFlag
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillName-string。*/
    /// </summary>
    public string SkillName
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillRange-int[]。*/
    /// </summary>
    public int[] SkillRange
    {
        get;
        private set;
    }

    /// <summary>
  /**获取targetFlag-int[]。*/
    /// </summary>
    public int[] TargetFlag
    {
        get;
        private set;
    }

    /// <summary>
  /**获取targetLock-bool。*/
    /// </summary>
    public bool TargetLock
    {
        get;
        private set;
    }

    /// <summary>
  /**获取releaseAct-string。*/
    /// </summary>
    public string ReleaseAct
    {
        get;
        private set;
    }

    /// <summary>
  /**获取releaseEff-string。*/
    /// </summary>
    public string ReleaseEff
    {
        get;
        private set;
    }

    /// <summary>
  /**获取animRotate-int。*/
    /// </summary>
    public int AnimRotate
    {
        get;
        private set;
    }

    /// <summary>
  /**获取flyAvatar-string。*/
    /// </summary>
    public string FlyAvatar
    {
        get;
        private set;
    }

    /// <summary>
  /**获取flyDistance-int。*/
    /// </summary>
    public int FlyDistance
    {
        get;
        private set;
    }

    /// <summary>
  /**获取flySpeed-int。*/
    /// </summary>
    public int FlySpeed
    {
        get;
        private set;
    }

    /// <summary>
  /**获取flyTime-int。*/
    /// </summary>
    public int FlyTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillIcon-string。*/
    /// </summary>
    public string SkillIcon
    {
        get;
        private set;
    }

    /// <summary>
  /**获取accuAct-string。*/
    /// </summary>
    public string AccuAct
    {
        get;
        private set;
    }

    /// <summary>
  /**获取accuEff-int。*/
    /// </summary>
    public int AccuEff
    {
        get;
        private set;
    }

    /// <summary>
  /**获取accuTab-string。*/
    /// </summary>
    public string AccuTab
    {
        get;
        private set;
    }

    /// <summary>
  /**获取releaseSpd-int。*/
    /// </summary>
    public int ReleaseSpd
    {
        get;
        private set;
    }

    /// <summary>
  /**获取skillShake-string[]。*/
    /// </summary>
    public string[] SkillShake
    {
        get;
        private set;
    }

    /// <summary>
  /**获取homeAction-int[]。*/
    /// </summary>
    public int[] HomeAction
    {
        get;
        private set;
    }

    /// <summary>
  /**获取effectInit-int[]。*/
    /// </summary>
    public int[] EffectInit
    {
        get;
        private set;
    }

    /// <summary>
  /**获取effectSelf-int[]。*/
    /// </summary>
    public int[] EffectSelf
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        AccuBreakable = DataTableParseUtil.ParseBool(columnStrings[index++]);
        AccuSound = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        AccuTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        AttackCanMove = DataTableParseUtil.ParseBool(columnStrings[index++]);
        EffectEnemy = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        EffectForward = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        ForwardReleaseTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        _id = int.Parse(columnStrings[index++]);
        IsAutoUse = DataTableParseUtil.ParseBool(columnStrings[index++]);
        IsHoldSkill = DataTableParseUtil.ParseBool(columnStrings[index++]);
        IsRemote = DataTableParseUtil.ParseBool(columnStrings[index++]);
        RangeTips = DataTableParseUtil.ParseBool(columnStrings[index++]);
        ReleaseSound = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        ReleaseTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        SkillCD = DataTableParseUtil.ParseInt(columnStrings[index++]);
        SkillDistance = DataTableParseUtil.ParseInt(columnStrings[index++]);
        SkillFlag = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        SkillName = columnStrings[index++];
        SkillRange = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        TargetFlag = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        TargetLock = DataTableParseUtil.ParseBool(columnStrings[index++]);
        ReleaseAct = columnStrings[index++];
        ReleaseEff = columnStrings[index++];
        AnimRotate = DataTableParseUtil.ParseInt(columnStrings[index++]);
        FlyAvatar = columnStrings[index++];
        FlyDistance = DataTableParseUtil.ParseInt(columnStrings[index++]);
        FlySpeed = DataTableParseUtil.ParseInt(columnStrings[index++]);
        FlyTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        SkillIcon = columnStrings[index++];
        AccuAct = columnStrings[index++];
        AccuEff = DataTableParseUtil.ParseInt(columnStrings[index++]);
        AccuTab = columnStrings[index++];
        ReleaseSpd = DataTableParseUtil.ParseInt(columnStrings[index++]);
        SkillShake = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        HomeAction = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        EffectInit = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        EffectSelf = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                AccuBreakable = binaryReader.ReadBoolean();
                AccuSound = binaryReader.ReadArray<String>();
                AccuTime = binaryReader.Read7BitEncodedInt32();
                AttackCanMove = binaryReader.ReadBoolean();
                EffectEnemy = binaryReader.ReadArray<Int32>();
                EffectForward = binaryReader.ReadArray<Int32>();
                ForwardReleaseTime = binaryReader.Read7BitEncodedInt32();
                _id = binaryReader.Read7BitEncodedInt32();
                IsAutoUse = binaryReader.ReadBoolean();
                IsHoldSkill = binaryReader.ReadBoolean();
                IsRemote = binaryReader.ReadBoolean();
                RangeTips = binaryReader.ReadBoolean();
                ReleaseSound = binaryReader.ReadArray<String>();
                ReleaseTime = binaryReader.Read7BitEncodedInt32();
                SkillCD = binaryReader.Read7BitEncodedInt32();
                SkillDistance = binaryReader.Read7BitEncodedInt32();
                SkillFlag = binaryReader.ReadArray<Int32>();
                SkillName = binaryReader.ReadString();
                SkillRange = binaryReader.ReadArray<Int32>();
                TargetFlag = binaryReader.ReadArray<Int32>();
                TargetLock = binaryReader.ReadBoolean();
                ReleaseAct = binaryReader.ReadString();
                ReleaseEff = binaryReader.ReadString();
                AnimRotate = binaryReader.Read7BitEncodedInt32();
                FlyAvatar = binaryReader.ReadString();
                FlyDistance = binaryReader.Read7BitEncodedInt32();
                FlySpeed = binaryReader.Read7BitEncodedInt32();
                FlyTime = binaryReader.Read7BitEncodedInt32();
                SkillIcon = binaryReader.ReadString();
                AccuAct = binaryReader.ReadString();
                AccuEff = binaryReader.Read7BitEncodedInt32();
                AccuTab = binaryReader.ReadString();
                ReleaseSpd = binaryReader.Read7BitEncodedInt32();
                SkillShake = binaryReader.ReadArray<String>();
                HomeAction = binaryReader.ReadArray<Int32>();
                EffectInit = binaryReader.ReadArray<Int32>();
                EffectSelf = binaryReader.ReadArray<Int32>();
            }
        }

        return true;
    }
}

