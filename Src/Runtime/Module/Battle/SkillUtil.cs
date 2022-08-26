
using System.Collections.Generic;
using UnityEngine;

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
        SkillShapeBase shape = SkillShapeFactory.CreateOneSkillShape(skillRange, entity.Root, skillDir);
        int targetLayer = GetEntityTargetLayer(entity.BaseData.Type);
        Collider[] colliders = shape.CheckHited(targetLayer);
        SkillShapeBase.Release(shape);

        if (colliders == null || colliders.Length <= 0)
        {
            return targetEntityList;
        }

        for (int i = 0; i < colliders.Length; i++)
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
}