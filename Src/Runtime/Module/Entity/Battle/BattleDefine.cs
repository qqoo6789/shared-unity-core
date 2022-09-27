/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:51:41
 * @Description: 战斗公共定义
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/BattleDefine.cs
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
            {EntityProfileField.MissRate, "MissRate"},
            {EntityProfileField.MoveSpeed, "MoveSpeed"},
            {EntityProfileField.PushDmg, "PushDmg"},
            {EntityProfileField.PushDist, "PushDist"},
            {EntityProfileField.HpCurrent, "HpCurrent"},
            {EntityProfileField.HpRecovery, "HpRecovery"},
        };
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