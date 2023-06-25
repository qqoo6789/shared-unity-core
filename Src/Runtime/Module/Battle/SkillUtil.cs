
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public static partial class SkillUtil
{
    /// <summary>
    /// 点对点命中检测
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <param name="blockLayer">遮挡层</param>
    /// <returns></returns>
    public static bool CheckP2P(Vector3 origin, Vector3 target, int blockLayer)
    {
        if (origin.ApproximatelyEquals(target))
        {
            return true;
        }

        bool result = !Physics.Linecast(origin, target, blockLayer, QueryTriggerInteraction.Ignore);
        return result;
    }

    /// <summary>
    /// 点对点命中检测
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <param name="hitInfo"></param>
    /// <param name="blockLayer">遮挡层</param>
    /// <returns></returns>
    public static bool CheckP2P(Vector3 origin, Vector3 target, out RaycastHit hitInfo, int blockLayer)
    {
        if (origin.ApproximatelyEquals(target))
        {
            hitInfo = default;
            return true;
        }

        bool result = !Physics.Linecast(origin, target, out hitInfo, blockLayer, QueryTriggerInteraction.Ignore);
        return result;
    }

    public static int GetEntityTargetLayer(GameMessageCore.EntityType type)
    {
        int targetLayer;
        switch (type)
        {
            case GameMessageCore.EntityType.MainPlayer:
            case GameMessageCore.EntityType.Player:
                targetLayer = 1 << MLayerMask.MONSTER;
                break;
            case GameMessageCore.EntityType.Monster:
                targetLayer = 1 << MLayerMask.PLAYER;
                break;
            default:
                targetLayer = 0;
                break;
        }
        return targetLayer;
    }

    /// <summary>
    /// 搜索目标列表
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="fromEntity"></param>
    /// <param name="skillRange"></param>
    /// <param name="skillDir"></param>
    public static List<EntityBase> SearchTargetEntityList(Vector3 pos, EntityBase fromEntity, int[] skillRange, Vector3 skillDir)
    {

        List<EntityBase> targetEntityList = new();
        int targetLayer = GetEntityTargetLayer(fromEntity.BaseData.Type);
        List<Collider> colliders = SearchTargetColliders(pos, skillRange, skillDir, targetLayer, MLayerMask.MASK_SCENE_OBSTRUCTION);

        if (colliders == null || colliders.Count <= 0)
        {
            return targetEntityList;
        }

        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].gameObject.TryGetComponent(out EntityReferenceData refData))
            {
                if (refData.Entity != null && refData.Entity.BaseData.Id != fromEntity.BaseData.Id)
                {
                    targetEntityList.Add(refData.Entity);
                }
            }
        }
        return targetEntityList;
    }

    /// <summary>
    /// 搜索目标碰撞体列表
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="skillRange"></param>
    /// <param name="skillDir"></param>
    /// <param name="targetLayer">搜索目标层</param>
    /// <param name="blockLayer">阻挡层</param>
    /// <returns></returns>
    public static List<Collider> SearchTargetColliders(Vector3 pos, int[] skillRange, Vector3 skillDir, int targetLayer, int blockLayer)
    {
        if (skillRange == null || skillRange.Length <= 0)
        {
            return new();
        }

        SkillShapeBase shape = SkillShapeFactory.CreateOneSkillShape(skillRange, pos, skillDir);
        List<Collider> colliders = shape.CheckHited(targetLayer, blockLayer);
        SkillShapeBase.Release(shape);
        return colliders;
    }

    /// <summary>
    /// 计算技能CD
    /// </summary>
    /// <param name="skillID"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static long CalculateSkillCD(int skillID, EntityBase entity)
    {
        DRSkill CurSkillCfg = GFEntryCore.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);
        if (CurSkillCfg == null)
        {
            Log.Error($"CalculateSkillCD  Error skillID = {skillID}");
            return 0;
        }

        long skillCD = CurSkillCfg.SkillCD;
        if (skillCD < 0)
        {
            skillCD = 0;
        }
        return skillCD;
    }

    /// <summary>
    /// 实体技能效果执行
    /// </summary>
    /// <param name="inputData">输入数据</param>
    /// <param name="effectList">效果列表</param>
    /// <param name="fromEntity">释放实体</param>
    /// <param name="targetEntity">目标实体</param>
    /// <returns></returns>
    public static List<GameMessageCore.DamageEffect> EntitySkillEffectExecute(InputSkillReleaseData inputData, int[] effectList, EntityBase fromEntity, EntityBase targetEntity)
    {
        List<GameMessageCore.DamageEffect> effects = new();
        if (effectList == null || effectList.Length <= 0)
        {
            return effects;
        }
        SkillEffectCpt effectCpt = targetEntity.GetComponent<SkillEffectCpt>();
        List<SkillEffectBase> skillEffects = SkillConfigParse.ParseSkillEffect(inputData.SkillID, fromEntity.BaseData.Id, targetEntity.BaseData.Id, effectList);
        for (int i = 0; i < skillEffects.Count; i++)
        {
            try
            {
                SkillEffectBase skillEffect = skillEffects[i];
                if (effectCpt.CheckApplyEffect(fromEntity, targetEntity, skillEffect))
                {
                    GameMessageCore.DamageEffect effectData = skillEffect.CreateEffectData(fromEntity, targetEntity, inputData);
                    if (effectData == null)
                    {
                        continue;
                    }
                    effectData.EffectType = (GameMessageCore.DamageEffectId)skillEffect.EffectCfg.Id;
                    skillEffect.SetEffectData(effectData);
                    skillEffect.SetInputData(inputData);
                    effects.Add(effectData);
                    if (!inputData.IsPreRelease)
                    {
                        fromEntity.EntityEvent.BeforeGiveSkillEffect?.Invoke(targetEntity, effectData);
                        targetEntity.EntityEvent.BeforeApplySkillEffect?.Invoke(effectData);
                        effectCpt.ApplyOneEffect(skillEffect);//注意顺序，Effects如果是瞬间的，应用后会立即被清除
                        fromEntity.EntityEvent.AfterGiveSkillEffect?.Invoke(targetEntity, effectData);
                        targetEntity.EntityEvent.AfterApplySkillEffect?.Invoke(effectData);
                    }
                    else
                    {
                        skillEffect.PlayPreEffect(targetEntity);
                        skillEffect.Dispose();
                    }

                }
                else
                {
                    skillEffect.Dispose();
                }
            }
            catch (System.Exception e)
            {
                Log.Error($"skill cast skill effect apply error skillID = {inputData.SkillID}, effectID = {skillEffects[i].EffectID} error = {e}");
                continue;
            }
        }
        return effects;
    }
    /// <summary>
    /// 实体技能效果执行
    /// </summary>
    /// <param name="inputData">输入数据</param>
    /// <param name="effectList">效果列表</param>
    /// <param name="fromEntity">释放实体</param>
    /// <param name="targetEntity">目标实体</param>
    /// <returns></returns>
    public static List<GameMessageCore.DamageEffect> EntitySkillEffectExecuteMiss(InputSkillReleaseData inputData, EntityBase fromEntity, EntityBase targetEntity)
    {
        List<GameMessageCore.DamageEffect> effects = new();

        try
        {
            GameMessageCore.DamageEffect effectData = SkillDamage.CreateSpecialDamageEffect(GameMessageCore.DamageState.Miss, targetEntity.BattleDataCore.HP, 0);
            effects.Add(effectData);
            if (inputData.IsPreRelease)
            {
                SkillEffectBase skillEffect = GFEntryCore.SkillEffectFactory.CreateOneSkillEffect(inputData.SkillID, TableDefine.DAMAGE_EFFECT_ID, fromEntity.BaseData.Id, targetEntity.BaseData.Id, 0);
                skillEffect.SetEffectData(effectData);
                skillEffect.SetInputData(inputData);
                skillEffect.PlayPreEffect(targetEntity);
                skillEffect.Dispose();
            }
        }
        catch (System.Exception e)
        {
            Log.Error($"EntitySkillEffectExecuteMiss Error  = {inputData.SkillID}, error = {e}");
        }

        return effects;
    }
    /// <summary>
    /// 实体技能效果取消
    /// </summary>
    /// <param name="skillID">技能ID</param>
    /// <param name="effectList">效果列表</param>
    /// <param name="fromEntity">释放实体</param>
    /// <param name="targetEntity">目标实体</param>
    /// <returns></returns>
    public static void EntityAbolishSkillEffect(int skillID, int[] effectList, EntityBase fromEntity, EntityBase targetEntity)
    {
        SkillEffectCpt effectCpt = targetEntity.GetComponent<SkillEffectCpt>();
        if (effectList != null && effectList.Length > 0)
        {
            for (int i = 0; i < effectList.Length; i++)
            {
                effectCpt.AbolishSkillEffect(effectList[i], skillID, fromEntity.BaseData.Id);
            }
        }
    }
    /// <summary>
    /// 计算技能效果列表
    /// </summary>
    /// <param name="modifierList"></param>
    /// <returns></returns>
    public static List<int> CalculateSkillEffectModifierList(List<SkillEffectModifier> modifierList)
    {
        Dictionary<int, int> effectValueMap = new();
        bool isReplace = false;
        for (int i = 0; i < modifierList.Count; i++)
        {
            SkillEffectModifier modifier = modifierList[i];
            if (modifier.Type == eSkillEffectModifierType.Replace)
            {
                effectValueMap.Clear();
                isReplace = true;
            }

            for (int j = 0; j < modifier.EffectIDs.Length; j++)
            {
                int effectID = modifier.EffectIDs[j];
                if (effectValueMap.TryGetValue(effectID, out int count))
                {
                    effectValueMap[effectID] = count + modifier.Value;
                }
                else
                {
                    effectValueMap.Add(effectID, modifier.Value);
                }
            }

            if (isReplace)
            {
                break;
            }
        }

        List<int> effectList = new();
        foreach (KeyValuePair<int, int> item in effectValueMap)
        {
            if (item.Value > 0)
            {
                effectList.Add(item.Key);
            }
        }
        return effectList;
    }

    public static int[] GetSkillEffect(EntityBase entity, DRSkill drSkill, eSkillEffectApplyType type)
    {
        //拥有技能, 效果可能是动态的，所以需要从技能组件中获取
        SkillBase skill = entity.GetComponent<SkillCpt>().GetSkill(drSkill.Id);
        if (skill != null)
        {
            return skill.GetEffect(type);
        }

        //没有技能， 效果是静态的，直接从配置中获取
        return type switch
        {
            eSkillEffectApplyType.Init => drSkill.EffectInit,
            eSkillEffectApplyType.Forward => drSkill.EffectForward,
            eSkillEffectApplyType.CastSelf => drSkill.EffectSelf,
            eSkillEffectApplyType.CastEnemy => drSkill.EffectEnemy,
            _ => null,
        };
    }

    public static bool IsSceneDeath(GameMessageCore.DamageState dmgState)
    {
        return dmgState is GameMessageCore.DamageState.Fall or GameMessageCore.DamageState.WaterDrown;
    }
}