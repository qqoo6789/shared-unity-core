/*
 * @Author: xiang huan
 * @Date: 2022-08-09 14:10:48
 * @Description: 实体技能数据
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/EntitySkillDataCore.cs
 * 
 */
using System.Collections.Generic;
using GameMessageCore;
using UnityGameFramework.Runtime;
public class EntitySkillDataCore : EntityBaseComponent
{
    protected int JumpSkillID = BattleDefine.JUMP_SKILL_ID_NULL; //翻滚技能
    protected List<int> EquipmentSkillIDList = new(); //装备技能列表
    protected List<int> BaseSkillIDList = new(); //基础技能列表

    private SkillCpt _skillCpt;
    protected SkillCpt SkillComponent
    {
        get
        {
            if (_skillCpt == null)
            {
                _skillCpt = gameObject.GetComponent<SkillCpt>();
            }
            return _skillCpt;
        }
    }

    private PlayerRoleDataCore _playerRoleDataCore;
    protected PlayerRoleDataCore RoleDataCore
    {
        get
        {
            if (_playerRoleDataCore == null)
            {
                _playerRoleDataCore = gameObject.GetComponent<PlayerRoleDataCore>();
            }
            return _playerRoleDataCore;
        }
    }

    protected virtual void Start()
    {
        RefEntity.EntityEvent.EntityAvatarUpdated += OnUpdateEquipmentSkillID;
        OnUpdateEquipmentSkillID();
    }

    protected virtual void OnDestroy()
    {
        SetJumpSkillID(BattleDefine.JUMP_SKILL_ID_NULL);
        RemoveBaseSkillList();
        RemoveEquipmentSkill();
        RefEntity.EntityEvent.EntityAvatarUpdated -= OnUpdateEquipmentSkillID;
    }

    public void AddBaseSkillList(int[] skillIDs)
    {
        RemoveBaseSkillList();
        BaseSkillIDList.AddRange(skillIDs);
        for (int i = 0; i < BaseSkillIDList.Count; i++)
        {
            _ = SkillComponent.AddSkill(BaseSkillIDList[i]);
        }
    }

    public void RemoveBaseSkillList()
    {
        for (int i = 0; i < BaseSkillIDList.Count; i++)
        {
            SkillComponent.RemoveSkill(BaseSkillIDList[i]);
        }
        BaseSkillIDList.Clear();
    }
    protected void OnUpdateEquipmentSkillID()
    {
        if (RoleDataCore == null)
        {
            return;
        }
        if (RoleDataCore.WearDic.TryGetValue(AvatarPosition.Weapon, out PlayerAvatar avatar))
        {
            DREquipment drEquipment = GFEntryCore.DataTable.GetDataTable<DREquipment>().GetDataRow(avatar.ObjectId);
            if (drEquipment != null)
            {
                SetEquipmentSkill(drEquipment.GivenSkillId);
            }
            else
            {
                Log.Error($"OnUpdateEquipmentSkillID not find equipment id:{avatar.ObjectId}");
            }
        }
    }

    public void SetEquipmentSkill(IEnumerable<int> skillIDs)
    {
        HashSet<int> newSkillIDMap = new();
        if (skillIDs != null)
        {
            newSkillIDMap = new(skillIDs);
        }

        //
        for (int i = EquipmentSkillIDList.Count - 1; i >= 0; i--)
        {
            //已经有的技能不需要添加
            if (newSkillIDMap.Contains(EquipmentSkillIDList[i]))
            {
                _ = newSkillIDMap.Remove(EquipmentSkillIDList[i]);
            }
            else
            {
                //删除多余的技能
                SkillComponent.RemoveSkill(EquipmentSkillIDList[i]);
                EquipmentSkillIDList.RemoveAt(i);
            }
        }

        foreach (int skillID in newSkillIDMap)
        {
            EquipmentSkillIDList.Add(skillID);
            _ = SkillComponent.AddSkill(skillID);
        }
    }

    public void RemoveEquipmentSkill()
    {
        for (int i = 0; i < EquipmentSkillIDList.Count; i++)
        {
            SkillComponent.RemoveSkill(EquipmentSkillIDList[i]);
        }
        EquipmentSkillIDList.Clear();
    }

    public void SetJumpSkillID(int skillID)
    {
        if (JumpSkillID == skillID)
        {
            return;
        }
        if (JumpSkillID != BattleDefine.JUMP_SKILL_ID_NULL)
        {
            SkillComponent.RemoveSkill(JumpSkillID);
        }
        JumpSkillID = skillID;
        _ = SkillComponent.AddSkill(skillID);
    }
}