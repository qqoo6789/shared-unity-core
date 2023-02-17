using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityGameFramework.Runtime;
/// <summary>
/// 实体技能采集器，负责采集各个模块提供的技能并添加到实体上
/// </summary>
public class EntitySkillCollector : EntityBaseComponent
{
    private SkillCpt _skillCpt;
    private bool _isInitTalentSkill = false;
    private void Start()
    {
        _skillCpt = RefEntity.GetComponent<SkillCpt>();
        if (_skillCpt == null)
        {
            Log.Error("SkillCollectCpt must add after SkillCpt");
        }

        RefEntity.EntityEvent.TalentSkillUpdated += OnTalentSkillUpdated;
        RefEntity.EntityEvent.TalentSkillInited += OnTalentSkillInited;

        CheckInitTalentSkill();
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
        InitTalentSkill(skills);
    }

    public void AddSkill(int skillID)
    {
        if (_skillCpt == null)
        {
            return;
        }

        _ = _skillCpt.AddSkill(skillID);
    }

    public void RemoveSkill(int skillID)
    {
        if (_skillCpt == null)
        {
            return;
        }

        _skillCpt.RemoveSkill(skillID);
    }

    private void CheckInitTalentSkill()
    {
        PlayerTalentTreeDataCore talentData = RefEntity.GetComponent<PlayerTalentTreeDataCore>();
        if (talentData != null)
        {
            InitTalentSkill(talentData.GetTalentSkills());
        }
    }

    private void InitTalentSkill(IEnumerable<int> skills)
    {
        if (_isInitTalentSkill || skills.Count() == 0)
        {
            return;
        }
        _isInitTalentSkill = true;

        foreach (int skillID in skills)
        {
            AddSkill(skillID);
        }
    }
}