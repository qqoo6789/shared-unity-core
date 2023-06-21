using System;
using GameMessageCore;
using UnityGameFramework.Runtime;

public class SkillDamage
{
    // private const int DAMAGE_WAVE_RANGE = 2;  // 策划约定，伤害值需要附加 -2% ～ 2% 的浮动变化 
    private const float MIN_DAMAGE = 1f;//最低伤害
    public static DamageData MakeDamageData(DamageState state, int defenderCurHp, int damage)
    {
        return new DamageData()
        {
            DmgState = state,
            CurrentInt = defenderCurHp,
            DeltaInt = damage,
        };
    }
    /// <summary>
    /// 检测命中
    /// </summary>
    /// <param name="attackerBattleData"></param>
    /// <param name="defenderBattleData"></param>
    /// <returns>DamageData</returns>
    public static bool CheckHit(EntityBattleDataCore attackerBattleData, EntityBattleDataCore defenderBattleData, Random inputRandom = null)
    {
        //无敌状态无法命中
        if (defenderBattleData.HasBattleState(BattleDefine.eBattleState.Invincible))
        {
            return false;
        }
        float realHitRate = (float)(1.0 + (attackerBattleData.HitRate - defenderBattleData.MissRate) / 100.0f);
        int realHitRatePercent = (int)(realHitRate * 100.0f);
        int randValue;
        if (inputRandom == null)
        {
            randValue = UnityEngine.Random.Range(0, 100);
        }
        else
        {
            randValue = inputRandom.Next(0, 100);
        }
        return randValue <= realHitRatePercent;
    }

    /// <summary>
    /// 计算技能伤害，damageData.DeltaInt 为真实伤害数数值，负数为减血，正数为加血
    /// </summary>
    /// <param name="coefficient">伤害系数</param>
    /// <returns>DamageData</returns>
    public static DamageData DamageCalculation(EntityAttributeData fromAttribute, EntityAttributeData toAttribute, float coefficient = 1, Random inputRandom = null)
    {
        /*
            命中概率算法：  命中率=1+（（HIT-EVA）/100） %
            普通伤害算法：  普通伤害baseDmg=攻击方att - 防守方def
            暴击触发算法：  暴击率 = CRT/1000 %
            暴击伤害算法：  dmg = 四舍五入int((1.0+（float64(CritDmg)/1000.0)/100.0) * baseDmg)
        */
        float defHp = toAttribute.GetRealValue(eAttributeType.HP);

        if (fromAttribute == null || toAttribute == null)
        {
            Log.Error($"DamageCalculation fromAttribute or toAttribute is null,form:{fromAttribute}");
            return MakeDamageData(DamageState.Normal, (int)defHp, 0);
        }

        (float damage, bool crit) = CalculateEnemyDamage(fromAttribute, toAttribute, inputRandom);

        float realDamage = damage * coefficient;
        // // 策划约定，伤害值需要附加 -2% ～ 2% 的浮动变化 
        // int dmgRandomRate = UnityEngine.Random.Range(100 - DAMAGE_WAVE_RANGE, 100 + DAMAGE_WAVE_RANGE);
        // damage *= dmgRandomRate / 100.0f;
        // int realDamage = damage < 0 ? 0 : (int)(MathF.Round(damage));
        return MakeDamageData(crit ? DamageState.Crit : DamageState.Normal, (int)defHp, -UnityEngine.Mathf.RoundToInt(realDamage));
    }

    /// <summary>
    /// 计算对敌人的伤害 怪物 boss等
    /// </summary>
    /// <param name="fromAttribute"></param>
    /// <param name="toAttribute"></param>
    /// <returns></returns>
    public static (float damage, bool crit) CalculateEnemyDamage(EntityAttributeData fromAttribute, EntityAttributeData toAttribute, Random inputRandom = null)
    {
        if (fromAttribute == null || toAttribute == null)
        {
            Log.Error($"CalculateEnemyDamage fromAttribute or toAttribute is null,form:{fromAttribute}");
            return (0, false);
        }

        TableEnemyDamageAttribute attributeClassify = EntityAttributeTable.Inst.GetDamageAttributeClassify<TableEnemyDamageAttribute>(HomeDefine.eAction.AttackEnemy);

        float baseDamage = fromAttribute.GetRealValue(attributeClassify.Att) - toAttribute.GetRealValue(attributeClassify.Def);
        baseDamage = Math.Max(0, baseDamage);

        (float coreDamage, bool crit) = CalculateCoreDamage(baseDamage, attributeClassify, fromAttribute, inputRandom);

        float res = coreDamage * (1 + toAttribute.GetRealValue(attributeClassify.Vulnerable));
        res = Math.Max(MIN_DAMAGE, res);

        return (res, crit);
    }

    /// <summary>
    /// 计算家园动作的伤害 砍树 挖矿等
    /// </summary>
    /// <param name="action"></param>
    /// <param name="fromAttribute"></param>
    /// <param name="toLevel">目标等级</param>
    /// <param name="homeAttRate">家园攻击力技能倍率</param>
    /// <returns></returns>
    public static (float damage, bool crit) CalculateHomeDamage(HomeDefine.eAction action, EntityAttributeData fromAttribute, int toLevel, float homeAttRate)
    {
        if ((action & HomeDefine.NEED_CALCULATE_DAMAGE_ACTION_MASK) == 0)
        {
            Log.Error($"CalculateHomeDamage action:{action} is not support calculate damage");
            return (0, false);
        }

        TableHomeDamageAttribute attributeClassify = EntityAttributeTable.Inst.GetDamageAttributeClassify<TableHomeDamageAttribute>(action);

        float baseDamage = fromAttribute.GetRealValue(attributeClassify.Att) * homeAttRate;
        baseDamage = Math.Max(0, baseDamage);

        (float coreDamage, bool crit) = CalculateCoreDamage(baseDamage, attributeClassify, fromAttribute);

        float res = coreDamage * MathF.Min(1, fromAttribute.GetRealValue(attributeClassify.AvailableLv) / toLevel);
        res = Math.Max(MIN_DAMAGE, res);

        return (res, crit);
    }

    /// <summary>
    /// 计算核心伤害值 并返回是否暴击
    /// </summary>
    /// <param name="baseDamage"></param>
    /// <param name="coreDamageClassify"></param>
    /// <param name="fromAttribute"></param>
    /// <returns></returns>
    private static (float damage, bool crit) CalculateCoreDamage(float baseDamage, TableCoreDamageAttribute coreDamageClassify, EntityAttributeData fromAttribute, Random inputRandom = null)
    {
        int critRate = (int)(fromAttribute.GetRealValue(coreDamageClassify.CritRate) * 1000);
        int realCritExtra;
        if (inputRandom == null)
        {
            realCritExtra = UnityEngine.Random.Range(0, 1000) < critRate ? 1 : 0;
        }
        else
        {
            realCritExtra = inputRandom.Next(0, 1000) < critRate ? 1 : 0;
        }
        float res = baseDamage * (1 + (realCritExtra * fromAttribute.GetRealValue(coreDamageClassify.CritDmg)));
        res *= 1 + fromAttribute.GetRealValue(coreDamageClassify.DmgBonus);
        return (res, realCritExtra == 1);
    }

    /// <summary>
    /// 创建特殊伤害效果
    /// </summary>
    /// <returns></returns>
    public static DamageEffect CreateSpecialDamageEffect(DamageState type, int finalHp, int deltaHp)
    {
        DamageEffect effect = new()
        {
            DamageValue = MakeDamageData(type, finalHp, deltaHp),
            EffectType = (DamageEffectId)TableDefine.DAMAGE_EFFECT_ID
        };
        return effect;
    }
}