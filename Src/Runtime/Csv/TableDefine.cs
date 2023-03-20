using System.Collections.Generic;
using GameMessageCore;
/*
* @Author: mangit
* @LastEditors: Please set LastEditors
* @Description: 表定义
* @Date: 2022-06-23 20:28:37
* @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Csv/TableDefine.cs
*/
public static class TableDefine
{
    public static readonly int ITEMID_EXP_FARMING = 188;
    public static readonly int ITEMID_EXP_COMBAT = 189;
    public static readonly int ITEMID_EXP_GATHER = 190;
    public static readonly int ITEMID_BATTERY = 21;
    public static readonly int ITEMID_SEED = 22;
    public static readonly int DATA_TABLE_START_ROW = 1;
    public static readonly string DATA_TABLE_ENCODING = "UTF-8";
    public const int DAMAGE_EFFECT_ID = 15;  // 基础伤害效果ID

    public static Dictionary<TalentType, int> TalentType2ItemIdDic = new(){
        {TalentType.Farming, ITEMID_EXP_FARMING},
        {TalentType.Battle, ITEMID_EXP_COMBAT},
        {TalentType.Gather, ITEMID_EXP_GATHER},
    };

    public enum eRoleAttrName//表属性属性字段名
    {
        Hp,
        HpRecovery,
        Att,
        AttSpd,
        Def,
        CritRate,
        CritDmg,
        HitPoint,
        MissPoint,
        MoveSpeed
    }
}

public enum eRoleID
{
    male = 1,
}

public enum eGameValueID
{
    animalMaxNum = 8,
}

// public static class GameValueID
// {
//     /// <summary>
//     /// 角色最大等级配置
//     /// </summary>
//     public const int ROLE_MAX_LV = 1;
//     /// <summary>
//     /// 角色插槽最大等级配置
//     /// </summary>
//     public const int SLOT_MAX_LV = 2;
//     /// <summary>
//     /// 技能最大等级配置
//     /// </summary>
//     public const int CRAFT_SKILL_MAX_LV = 3;
//     /// <summary>
//     /// 角色等级与插槽等级正负差距
//     /// </summary>
//     public const int MAX_LV_GAP_BETWEEN_SLOT_AND_ROLE = 4;
//     /// <summary>
//     /// 升级时不小于角色等级5级的插槽数量限制
//     /// </summary>
//     public const int COUNT_OF_VALID_SLOT_LV_TO_UPGRADE_ROLE = 5;
// }

/// <summary>
/// 语言表id
/// </summary>
public static class LanguageTableID
{
    public const int ACQUIRE_EXP_TIPS = 10060001;
    public const int ACQUIRE_LNCO_TIPS = 10060002;
    public const int ACQUIRE_SKILL_PROFICIENCY_TIPS = 10060003;
}

/// <summary>
/// 对应item表中的type字段
/// </summary>
public enum ePropItemType
{
    None = 0,
    Equipment = 1,
    Consumable = 3,
    Material = 4,
    Building = 5,
}

/// <summary>
/// 对应equipment表中的gearType
/// </summary>
public enum eEquipmentType
{
    HeadArmor = 1,
    ChestArmor,
    LegsArmor,
    FeetArmor,
    HandsArmor,
    Axe,
    PickAxe,
    Sword,
    Bow,
    Decorations,
    SingleGun,
    DoubleGun,
    Dagger,
    Spear,
}

//消耗品使用交互类型
public enum eFoodItemInteractType
{
    addHp,//增加血量
    occupyLand,//占地
    equipItem,//装备道具
}
public enum eSkillEffectType : int
{
    EffectIdUnknown = 0,
    SENormalDamage = 1,
    SEPathMove = 2,
    SEBeHitPathMove = 3,
    SEInvincible = 4,
    SEEndure = 5,
    SELockEnemyPathMoveCore = 6,
    SEAttributeModifierCore = 7,
    SEStun = 8,
    SEDotDamage = 9,
    SECollisionTrigger = 10,
    SETriggerQuickCastSkill = 11,
    SESkillRangeTrigger = 12,
    SEBloodRage = 13,
    SEUnharmedAddAttr = 14,
    SESkillEffectModifier = 15,
    SECaptureRopeHit = 16,
    SECaptureDamage = 17,
}
/// <summary>
/// 技能效果作用类型
/// </summary>
public enum eSkillEffectApplyType : int
{
    Init = 0, //初始阶段
    Forward = 1, //前置阶段
    CastSelf = 2, //对自己释放
    CastEnemy = 3, //对敌人释放
}
/// <summary>
/// 技能效果修改类型
/// </summary>
public enum eSkillEffectModifierType : int
{
    Replace = 0, //替换
    Add = 1, //增加
    Remove = 2, //移除
}