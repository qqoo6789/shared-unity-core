
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

        bool result = !Physics.Linecast(origin, target, blockLayer);
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

        bool result = !Physics.Linecast(origin, target, out hitInfo, blockLayer);
        return result;
    }

    public static int GetEntityTargetLayer(MelandGame3.EntityType type)
    {
        int targetLayer;
        switch (type)
        {
            case MelandGame3.EntityType.MainPlayer:
            case MelandGame3.EntityType.EntityTypePlayer:
                targetLayer = 1 << MLayerMask.MONSTER;
                break;
            case MelandGame3.EntityType.EntityTypeMonster:
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
    /// <param name="entity">实体ID</param>
    /// <param name="skillRange">技能范围配置</param>
    /// <param name="skillDir">技能方向</param>
    public static List<EntityBase> SearchTargetEntityList(EntityBase entity, int[] skillRange, Vector3 skillDir)
    {

        List<EntityBase> targetEntityList = new();
        Vector3 startPos;
        if (entity.TryGetComponent(out EntityCollisionCore entityCollision) && entityCollision.BodyCollision != null)
        {
            startPos = entityCollision.BodyCollision.bounds.center;
        }
        else
        {
            startPos = entity.GetTransform().position;
        }
        SkillShapeBase shape = SkillShapeFactory.CreateOneSkillShape(skillRange, startPos, skillDir);
        int targetLayer = GetEntityTargetLayer(entity.BaseData.Type);
        List<Collider> colliders = shape.CheckHited(targetLayer, MLayerMask.MASK_SCENE_OBSTRUCTION);
        SkillShapeBase.Release(shape);

        if (colliders == null || colliders.Count <= 0)
        {
            return targetEntityList;
        }

        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].gameObject.TryGetComponent(out EntityReferenceData refData))
            {
                if (refData.Entity != null && refData.Entity.BaseData.Id != entity.BaseData.Id)
                {
                    targetEntityList.Add(refData.Entity);
                }
            }
        }
        return targetEntityList;
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
        if (!entity.TryGetComponent(out EntityBattleDataCore entityBattleData))
        {
            Log.Error($"CalculateSkillCD  Not Find EntityBattleDataCore skillID = {skillID}");
            return 0;
        }
        //AttSpeed暂时做冷却缩减使用
        double cdScale = (10000 - entityBattleData.AttSpeed) / 10000;
        long skillCD = (long)(CurSkillCfg.SkillCD * cdScale);
        if (skillCD < 0)
        {
            skillCD = 0;
        }
        return skillCD;
    }
}