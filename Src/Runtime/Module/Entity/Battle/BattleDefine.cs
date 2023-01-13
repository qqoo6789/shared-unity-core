/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:51:41
 * @Description: 战斗公共定义
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/BattleDefine.cs
 * 
 */

using System.Collections.Generic;
using GameMessageCore;

public static class BattleDefine
{
    public enum eSkillEffectType : int
    {
        EffectIdUnknown = 0,
        SENormalDamage = 1001,
        SEPathMove = 1002,
        SEBeHitPathMove = 1003,
        SEInvincible = 1004,
        SEEndure = 1005,
        SELockEnemyPathMoveCore = 1006,
        SEAttributeModifierCore = 1007,
    }

    public enum eSkillShapeId : int
    {
        IdUnknown = 0,
        SkillShapeBox = 1,
        SkillShapeSphere = 2,
        SkillShapeCapsule = 3,
        SkillShapeFan = 4,
    }
    public enum eBattleState
    {
        Invincible,  //无敌效果（攻击全miss）
        Endure,     //霸体效果（释放技能不可打断）  
    }

    public static readonly Dictionary<EntityProfileField, string> ProfileFieldDict = new()
        {
            {EntityProfileField.Lv, "Lv"},
            {EntityProfileField.Exp, "Exp"},
            {EntityProfileField.Att, "Att"},
            {EntityProfileField.AttSpeed, "AttSpeed"},
            {EntityProfileField.Def, "Def"},
            {EntityProfileField.HpLimit, "HpLimit"},
            {EntityProfileField.CritRate, "CritRate"},
            {EntityProfileField.CritDamage, "CritDmg"},
            {EntityProfileField.HitRate, "HitRate"},
            {EntityProfileField.MissRate, "MissRate"},
            {EntityProfileField.MoveSpeed, "MoveSpeed"},
            {EntityProfileField.PushDmg, "PushDmg"},
            {EntityProfileField.PushDist, "PushDist"},
            {EntityProfileField.HpCurrent, "HpCurrent"},
            {EntityProfileField.HpRecovery, "HpRecovery"},
        };

    public const int SKILL_USE_TAG = (int)eSkillType.General | (int)eSkillType.Channel | (int)eSkillType.Toggle;
}
public enum eEntityCDType : int
{
    Skill,  //技能
    Extend, //扩展
}
public enum eEntityExtendCDType : int
{
    Revive = 0,  //复活
}

public enum eSkillType : int
{
    Passive = 1 << 1,  //被动
    General = 1 << 2,  //主动
    Channel = 1 << 3, //主动持续释放，预留
    Toggle = 1 << 4,  //开关，预留
}
public enum eSkillTargetType : int
{
    NotTarget = 1 << 1,  //无需目标
    Target = 1 << 2,  //需要目标
    Pos = 1 << 3, //需要位置
}