/*
 * @Author: mangit
 * @LastEditors: mangit
 * @Description: 表定义
 * @Date: 2022-06-23 20:28:37
 * @FilePath: /Assets/Src/Csv/Table/TableDefine.cs
 */
public static class TableDefine
{
    public static readonly int ITEMID_EXP = 79999902;
    public static readonly int ITEMID_BATTERY = 3010203;
    public static readonly int ITEMID_SEED = 3010204;
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

public static class GameValueID
{
    /// <summary>
    /// 角色最大等级配置
    /// </summary>
    public const int ROLE_MAX_LV = 1000001;
    /// <summary>
    /// 角色插槽最大等级配置
    /// </summary>
    public const int SLOT_MAX_LV = 1000002;
    /// <summary>
    /// 技能最大等级配置
    /// </summary>
    public const int CRAFT_SKILL_MAX_LV = 1000003;
    /// <summary>
    /// 角色等级与插槽等级正负差距
    /// </summary>
    public const int MAX_LV_GAP_BETWEEN_SLOT_AND_ROLE = 1000004;
    /// <summary>
    /// 升级时不小于角色等级5级的插槽数量限制
    /// </summary>
    public const int COUNT_OF_VALID_SLOT_LV_TO_UPGRADE_ROLE = 1000005;
}

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