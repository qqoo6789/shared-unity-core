using System.Collections;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
/// <summary>
/// 实体技能采集器，负责采集各个模块提供的技能并添加到实体上
/// </summary>
public class EntitySkillCollector : EntityBaseComponent
{
    private SkillCpt _skillCpt;
    private void Start()
    {
        _skillCpt = RefEntity.GetComponent<SkillCpt>();
        if (_skillCpt == null)
        {
            Log.Error("SkillCollectCpt must add after SkillCpt");
        }

        RefEntity.EntityEvent.TalentSkillUpdated += OnTalentSkillUpdated;
        RefEntity.EntityEvent.TalentSkillInited += OnTalentSkillInited;
    }

    private void OnDestroy()
    {
        _skillCpt = null;
        RefEntity.EntityEvent.TalentSkillUpdated -= OnTalentSkillUpdated;
        RefEntity.EntityEvent.TalentSkillInited -= OnTalentSkillInited;
    }

    private void OnTalentSkillUpdated(IEnumerable<int> addList, IEnumerable<int> removeList)
    {
        if (addList != null)
        {
            foreach (int skillID in addList)
            {
                AddSkill(skillID);
            }
        }

        if (removeList != null)
        {

            foreach (int skillID in removeList)
            {
                RemoveSkill(skillID);
            }
        }
    }

    private void OnTalentSkillInited(IEnumerable<int> skills)
    {
        List<int> addSkills = new List<int>(skills);
        for (int i = 0; i < addSkills.Count; i++)
        {
            AddSkill(addSkills[i]);
        }
    }

    public void AddSkill(int skillID)
    {
        if (_skillCpt == null)
        {
            return;
        }

        _skillCpt.AddSkill(skillID);
    }

    public void RemoveSkill(int skillID)
    {
        if (_skillCpt == null)
        {
            return;
        }

        _skillCpt.RemoveSkill(skillID);
    }
}