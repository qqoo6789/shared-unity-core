/*
 * @Author: xiang huan
 * @Date: 2022-07-29 10:08:50
 * @Description: 实体技能搜索目标
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/EntitySkillSearchTarget.cs
 * 
 */


using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class EntitySkillSearchTarget : EntityBaseComponent
{
    public EntityBase TargetEntity { get; protected set; } //目标实体
    protected EntityInputData InputData;
    protected virtual void Start()
    {
        InputData = GetComponent<EntityInputData>();
    }

    public virtual void UpdateTarget(List<EntityBase> targetEntities)
    {
        TargetEntity = null;
        if (targetEntities == null || targetEntities.Count <= 0)
        {
            return;
        }
        float curDist = float.MaxValue;
        foreach (EntityBase entity in targetEntities)
        {
            if (!entity.Inited || entity.BattleDataCore == null || !entity.BattleDataCore.IsLive())
            {
                continue;
            }
            float dist = Vector3.Distance(RefEntity.Position, entity.Position);

            if (dist < curDist)
            {
                curDist = dist;
                TargetEntity = entity;
            }
        }
    }

    public virtual void SearchTarget(int skillID)
    {
        UpdateTarget(null);
        DRSkill drSkill = GFEntryCore.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);
        if (drSkill == null)
        {
            Log.Error($"not find skill id:{skillID}");
            return;
        }

        List<EntityBase> targetEntities = SearchSkillRangeTarget(drSkill);
        if (targetEntities.Count <= 0)
        {
            targetEntities = SearchSkillDistanceTarget(drSkill);
        }
        UpdateTarget(targetEntities);
    }

    protected List<EntityBase> SearchSkillRangeTarget(DRSkill drSkill)
    {
        if (drSkill.SkillRange == null || drSkill.SkillRange.Length == 0)
        {
            return new();
        }

        Vector3 dir = RefEntity.Forward;
        if (InputData != null && InputData.InputMoveDirection != null)//有输入移动方向需要按照输入方向
        {
            dir = InputData.InputMoveDirection.Value;
        }
        List<EntityBase> targetEntities = SkillUtil.SearchTargetEntityList(RefEntity.RoleBaseDataCore.CenterPos, RefEntity, drSkill.SkillRange, dir);
        return targetEntities;
    }

    protected List<EntityBase> SearchSkillDistanceTarget(DRSkill drSkill)
    {
        int[] range = { (int)BattleDefine.eSkillShapeId.SkillShapeSphere, drSkill.SkillDistance };
        List<EntityBase> targetEntities = SkillUtil.SearchTargetEntityList(RefEntity.RoleBaseDataCore.CenterPos, RefEntity, range, RefEntity.Forward);
        return targetEntities;
    }
}
