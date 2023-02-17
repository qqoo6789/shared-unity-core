using System.Collections.Generic;
using UnityGameFramework.Runtime;

/// <summary>
/// 实体属性表
/// </summary>
public class EntityAttributeTable
{
    //伤害分类
    private Dictionary<HomeDefine.eAction, TableBaseDamageAttribute> _damageAttributeClassifyMap;

    //家园动作属性分类
    private Dictionary<HomeDefine.eAction, TableHomeActionAttribute> _homeAttributeClassifyMap;

    private static EntityAttributeTable s_instance;

    public static EntityAttributeTable Inst
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new EntityAttributeTable();
            }

            return s_instance;
        }
    }

    private EntityAttributeTable()
    {
        InitDamageAttributeClassify();

        InitHomeAttributeClassify();
    }

    /// <summary>
    /// 获取伤害属性分类
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public TableBaseDamageAttribute GetDamageAttributeClassify(HomeDefine.eAction action)
    {
        if (!_damageAttributeClassifyMap.TryGetValue(action, out TableBaseDamageAttribute attribute))
        {
            Log.Error($"EntityAttributeTable GetDamageAttributeClassify Not Find Action = {action}");
            return null;
        }

        return attribute;
    }

    /// <summary>
    /// 获取家园属性分类
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public TableHomeActionAttribute GetHomeAttributeClassify(HomeDefine.eAction action)
    {
        if (!_homeAttributeClassifyMap.TryGetValue(action, out TableHomeActionAttribute attribute))
        {
            Log.Error($"EntityAttributeTable GetHomeActionAttributeClassify Not Find Action = {action}");
            return null;
        }

        return attribute;
    }

    private void InitHomeAttributeClassify()
    {
        _homeAttributeClassifyMap = new();

        //砍树
        TableHomeActionAttribute cutTree = new TableHomeActionAttribute()
        {
            ExtraHarvestRate = eAttributeType.ExtraWoodRate,
            AvailableLv = eAttributeType.TreeAvailableLv,
        };
        _homeAttributeClassifyMap.Add(HomeDefine.eAction.Cut, cutTree);

        //采矿
        TableHomeActionAttribute mining = new TableHomeActionAttribute()
        {
            ExtraHarvestRate = eAttributeType.ExtraOreRate,
            AvailableLv = eAttributeType.OreAvailableLv,
        };
        _homeAttributeClassifyMap.Add(HomeDefine.eAction.Mining, mining);

        //农作物
        TableHomeActionAttribute harvest = new TableHomeActionAttribute()
        {
            ExtraHarvestRate = eAttributeType.ExtraHarvestRate,
            AvailableLv = eAttributeType.CropAvailableLv,
        };
        _homeAttributeClassifyMap.Add(HomeDefine.eAction.Harvest, harvest);
    }

    private void InitDamageAttributeClassify()
    {
        _damageAttributeClassifyMap = new();

        //怪物
        TableBaseDamageAttribute attack = new TableBaseDamageAttribute()
        {
            Att = eAttributeType.CombatAtt,
            Def = eAttributeType.CombatDef,
            DmgBonus = eAttributeType.CombatDmgBonus,
            CritRate = eAttributeType.CombatCritRate,
            CritDmg = eAttributeType.CombatCritDmg,
        };
        _damageAttributeClassifyMap.Add(HomeDefine.eAction.AttackMonster, attack);

        //砍树
        TableBaseDamageAttribute cutTree = new TableBaseDamageAttribute()
        {
            Att = eAttributeType.TreeAtt,
            DmgBonus = eAttributeType.TreeDmgBonus,
            CritRate = eAttributeType.TreeCritRate,
            CritDmg = eAttributeType.TreeCritDmg,
        };
        _damageAttributeClassifyMap.Add(HomeDefine.eAction.Cut, cutTree);

        //采矿
        TableBaseDamageAttribute mining = new TableBaseDamageAttribute()
        {
            Att = eAttributeType.OreAtt,
            DmgBonus = eAttributeType.OreDmgBonus,
            CritRate = eAttributeType.OreCritRate,
            CritDmg = eAttributeType.OreCritDmg,
        };
        _damageAttributeClassifyMap.Add(HomeDefine.eAction.Mining, mining);
    }
}