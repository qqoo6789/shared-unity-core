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
    /// /**获取技能ID。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取技能名称1。*/
    /// </summary>
    public string SkillName
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能CD
单位:毫秒。*/
    /// </summary>
    public int SkillCD
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能ICON。*/
    /// </summary>
    public string SkillIcon
    {
        get;
        private set;
    }

    /// <summary>
  /**获取家园动作。*/
    /// </summary>
    public int HomeAction
    {
        get;
        private set;
    }

    /// <summary>
  /**获取家园动作速度。*/
    /// </summary>
    public int HomeActionSpeed
    {
        get;
        private set;
    }

    /// <summary>
  /**获取作用于敌方技能效果。*/
    /// </summary>
    public int[] EffectEnemy
    {
        get;
        private set;
    }

    /// <summary>
  /**获取作用于己方技能效果。*/
    /// </summary>
    public int[] EffectSelf
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能前摇效果。*/
    /// </summary>
    public int[] EffectForward
    {
        get;
        private set;
    }

    /// <summary>
  /**获取蓄力时间。*/
    /// </summary>
    public int AccuTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取蓄力动作。*/
    /// </summary>
    public string AccuAct
    {
        get;
        private set;
    }

    /// <summary>
  /**获取蓄力特效。*/
    /// </summary>
    public int AccuEff
    {
        get;
        private set;
    }

    /// <summary>
  /**获取蓄力是否可以被打断。*/
    /// </summary>
    public bool AccuBreakable
    {
        get;
        private set;
    }

    /// <summary>
  /**获取蓄力条名字。*/
    /// </summary>
    public string AccuTab
    {
        get;
        private set;
    }

    /// <summary>
  /**获取蓄力音效。*/
    /// </summary>
    public string[] AccuSound
    {
        get;
        private set;
    }

    /// <summary>
  /**获取是否持续技能。*/
    /// </summary>
    public bool IsHoldSkill
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能释放动作。*/
    /// </summary>
    public string ReleaseAct
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能释放特效。*/
    /// </summary>
    public string ReleaseEff
    {
        get;
        private set;
    }

    /// <summary>
  /**获取释放音效。*/
    /// </summary>
    public string[] ReleaseSound
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能释放时间。*/
    /// </summary>
    public int ReleaseTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能前摇时间。*/
    /// </summary>
    public int ForwardReleaseTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能范围是否预警。*/
    /// </summary>
    public bool RangeTips
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能生效范围。*/
    /// </summary>
    public int[] SkillRange
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能释放距离cm。*/
    /// </summary>
    public int SkillDistance
    {
        get;
        private set;
    }

    /// <summary>
  /**获取目标锁定。*/
    /// </summary>
    public bool TargetLock
    {
        get;
        private set;
    }

    /// <summary>
  /**获取技能震屏。*/
    /// </summary>
    public string[] SkillShake
    {
        get;
        private set;
    }

    /// <summary>
  /**获取攻击时是否可以移动。*/
    /// </summary>
    public bool AttackCanMove
    {
        get;
        private set;
    }

    /// <summary>
  /**获取动作旋转。*/
    /// </summary>
    public int AnimRotate
    {
        get;
        private set;
    }

    /// <summary>
  /**获取是否自动释放。*/
    /// </summary>
    public bool IsAutoUse
    {
        get;
        private set;
    }

    /// <summary>
  /**获取是否远程技能。*/
    /// </summary>
    public bool IsRemote
    {
        get;
        private set;
    }

    /// <summary>
  /**获取飞行物资源。*/
    /// </summary>
    public string FlyAvatar
    {
        get;
        private set;
    }

    /// <summary>
  /**获取固定飞行速度cm/s。*/
    /// </summary>
    public int FlySpeed
    {
        get;
        private set;
    }

    /// <summary>
  /**获取固定飞行时间ms。*/
    /// </summary>
    public int FlyTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取飞行距离cm。*/
    /// </summary>
    public int FlyDistance
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        index++;
        SkillName = columnStrings[index++];
        SkillCD = DataTableParseUtil.ParseInt(columnStrings[index++]);
        SkillIcon = columnStrings[index++];
        HomeAction = DataTableParseUtil.ParseInt(columnStrings[index++]);
        HomeActionSpeed = DataTableParseUtil.ParseInt(columnStrings[index++]);
        EffectEnemy = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        EffectSelf = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        EffectForward = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        AccuTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        AccuAct = columnStrings[index++];
        AccuEff = DataTableParseUtil.ParseInt(columnStrings[index++]);
        AccuBreakable = DataTableParseUtil.ParseBool(columnStrings[index++]);
        AccuTab = columnStrings[index++];
        AccuSound = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        IsHoldSkill = DataTableParseUtil.ParseBool(columnStrings[index++]);
        ReleaseAct = columnStrings[index++];
        ReleaseEff = columnStrings[index++];
        ReleaseSound = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        ReleaseTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        ForwardReleaseTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        RangeTips = DataTableParseUtil.ParseBool(columnStrings[index++]);
        SkillRange = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        SkillDistance = DataTableParseUtil.ParseInt(columnStrings[index++]);
        TargetLock = DataTableParseUtil.ParseBool(columnStrings[index++]);
        SkillShake = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        AttackCanMove = DataTableParseUtil.ParseBool(columnStrings[index++]);
        AnimRotate = DataTableParseUtil.ParseInt(columnStrings[index++]);
        IsAutoUse = DataTableParseUtil.ParseBool(columnStrings[index++]);
        IsRemote = DataTableParseUtil.ParseBool(columnStrings[index++]);
        FlyAvatar = columnStrings[index++];
        FlySpeed = DataTableParseUtil.ParseInt(columnStrings[index++]);
        FlyTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        FlyDistance = DataTableParseUtil.ParseInt(columnStrings[index++]);

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                SkillName = binaryReader.ReadString();
                SkillCD = binaryReader.Read7BitEncodedInt32();
                SkillIcon = binaryReader.ReadString();
                HomeAction = binaryReader.Read7BitEncodedInt32();
                HomeActionSpeed = binaryReader.Read7BitEncodedInt32();
                EffectEnemy = binaryReader.ReadArray<Int32>();
                EffectSelf = binaryReader.ReadArray<Int32>();
                EffectForward = binaryReader.ReadArray<Int32>();
                AccuTime = binaryReader.Read7BitEncodedInt32();
                AccuAct = binaryReader.ReadString();
                AccuEff = binaryReader.Read7BitEncodedInt32();
                AccuBreakable = binaryReader.ReadBoolean();
                AccuTab = binaryReader.ReadString();
                AccuSound = binaryReader.ReadArray<String>();
                IsHoldSkill = binaryReader.ReadBoolean();
                ReleaseAct = binaryReader.ReadString();
                ReleaseEff = binaryReader.ReadString();
                ReleaseSound = binaryReader.ReadArray<String>();
                ReleaseTime = binaryReader.Read7BitEncodedInt32();
                ForwardReleaseTime = binaryReader.Read7BitEncodedInt32();
                RangeTips = binaryReader.ReadBoolean();
                SkillRange = binaryReader.ReadArray<Int32>();
                SkillDistance = binaryReader.Read7BitEncodedInt32();
                TargetLock = binaryReader.ReadBoolean();
                SkillShake = binaryReader.ReadArray<String>();
                AttackCanMove = binaryReader.ReadBoolean();
                AnimRotate = binaryReader.Read7BitEncodedInt32();
                IsAutoUse = binaryReader.ReadBoolean();
                IsRemote = binaryReader.ReadBoolean();
                FlyAvatar = binaryReader.ReadString();
                FlySpeed = binaryReader.Read7BitEncodedInt32();
                FlyTime = binaryReader.Read7BitEncodedInt32();
                FlyDistance = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

