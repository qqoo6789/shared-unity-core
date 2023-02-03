/*
 * @Author: xiang huan
 * @Date: 2022-07-19 13:38:00
 * @Description: 技能组件
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/Cpt/SkillCpt.cs
 * 
 */
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class SkillCpt : EntityBaseComponent
{
    public Dictionary<int, SkillBase> SkillMap { get; private set; } = new();

    /// <summary>
    /// 添加技能
    /// </summary>
    public SkillBase AddSkill(int skillID)
    {
        if (SkillMap.ContainsKey(skillID))
        {
            Log.Warning($"Add Skill Repeat! skillId ={skillID}");
            return SkillMap[skillID];
        }
        SkillBase skill = SkillBase.Create<SkillBase>(skillID);
        SkillMap.Add(skillID, skill);
        skill.OnAdd(RefEntity);
        return skill;
    }

    /// <summary>
    /// 删除技能
    /// </summary>
    public void RemoveSkill(int skillID)
    {
        if (!SkillMap.TryGetValue(skillID, out SkillBase skill))
        {
            Log.Warning($"Remove Skill Is Null! skillId ={skillID}");
            return;
        }
        skill.OnRemove();
        skill.Dispose();
        _ = SkillMap.Remove(skillID);
    }
    public void RemoveAllSkill()
    {
        foreach (KeyValuePair<int, SkillBase> item in SkillMap)
        {
            item.Value.OnRemove();
            item.Value.Dispose();
        }
        SkillMap.Clear();
    }
    public bool CanUseSkill(int skillID, Vector3 dir, List<long> targetList)
    {
        //检测是否拥有技能
        if (!SkillMap.TryGetValue(skillID, out SkillBase skill))
        {
            Log.Warning($"CanUseSkill Skill Is Null! skillId ={skillID}");
            return false;
        }
        //检测技能能否使用
        if ((skill.SkillFlag & BattleDefine.SKILL_USE_TAG) == 0)
        {
            return false;
        }

        return CheckSkillTarget(skill, targetList);
    }
    public bool CheckSkillTarget(SkillBase skill, List<long> targetList)
    {
        //不需要目标
        if ((skill.TargetFlag & (int)eSkillTargetType.NotTarget) != 0)
        {
            return true;
        }
        //拥有目标
        if ((skill.TargetFlag & (int)eSkillTargetType.Target) != 0 && targetList.Count > 0)
        {
            return true;

        }
        return false;
    }
    private void OnDestroy()
    {
        RemoveAllSkill();
    }
}