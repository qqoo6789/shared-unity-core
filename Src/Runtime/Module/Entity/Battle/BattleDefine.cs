/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:51:41
 * @Description: 战斗公共定义
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/BattleDefine.cs
 * 
 */

using System.Collections.Generic;
using MelandGame3;

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
            {EntityProfileField.EntityProfileFieldLv, "Lv"},
            {EntityProfileField.EntityProfileFieldExp, "Exp"},
            {EntityProfileField.EntityProfileFieldAtt, "Att"},
            {EntityProfileField.EntityProfileFieldAttSpeed, "AttSpeed"},
            {EntityProfileField.EntityProfileFieldDef, "Def"},
            {EntityProfileField.EntityProfileFieldHpLimit, "HpLimit"},
            {EntityProfileField.EntityProfileFieldCritRate, "CritRate"},
            {EntityProfileField.EntityProfileFieldCritDamage, "CritDmg"},
            {EntityProfileField.EntityProfileFieldMissRate, "MissRate"},
            {EntityProfileField.EntityProfileFieldMoveSpeed, "MoveSpeed"},
            {EntityProfileField.EntityProfileFieldPushDmg, "PushDmg"},
            {EntityProfileField.EntityProfileFieldPushDist, "PushDist"},
            {EntityProfileField.EntityProfileFieldHpCurrent, "HpCurrent"},
            {EntityProfileField.EntityProfileFieldHpRecovery, "HpRecovery"},
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