/*
 * @Author: xiang huan
 * @Date: 2022-08-09 14:10:48
 * @Description: 实体技能数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/HotFix/Module/Entity/Data/EntitySkillDataCore.cs
 * 
 */
using System;
using System.Collections.Generic;
using GameMessageCore;
using UnityGameFramework.Runtime;
public class EntitySkillDataCore : EntityBaseComponent
{
    protected int JumpSkillID = BattleDefine.JUMP_SKILL_ID_NULL; //翻滚技能
    public List<int> EquipmentSkillIDList = new(); //装备技能列表
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

    /// <summary>
    /// 当前连击技能id组
    /// </summary>
    /// <value></value>
    public int[] ComboSkillIdArray
    {
        get
        {
            if (EquipmentSkillIDList.Count > 0)
            {
                return EquipmentSkillIDList.ToArray();
            }

            return BaseSkillIDList.ToArray();
        }
    }

    /// <summary>
    /// 某个技能是否属于连击技能组的 往往也是代表了普通攻击
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public bool IsComboGroupSkill(int skillID)
    {
        return Array.IndexOf(ComboSkillIdArray, skillID) >= 0;
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
        if (RefEntity != null)
        {
            RefEntity.EntityEvent.EntityAvatarUpdated -= OnUpdateEquipmentSkillID;
        }
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
        if (RoleDataCore.WearDic.TryGetValue(AvatarPosition.Weapon, out AvatarAttribute avatar))
        {
            DREquipment drEquipment = EquipmentTable.Inst.GetRowByItemID(avatar.ObjectId);
            if (drEquipment != null)
            {
                SetExtraGroupSkill(drEquipment.GivenSkillId, EquipmentSkillIDList);
            }
            else
            {
                Log.Error($"OnUpdateEquipmentSkillID not find equipment id:{avatar.ObjectId}");
            }
        }
        else
        {
            RemoveEquipmentSkill();
        }
    }

    /// <summary>
    /// 外部debug测试强制设置装备技能
    /// </summary>
    /// <param name="equipmentItemID"></param>
    public void DebugTestForceSetEquipmentSkill(int equipmentItemID)
    {
        DREquipment drEquipment = EquipmentTable.Inst.GetRowByItemID(equipmentItemID);
        if (drEquipment != null)
        {
            SetExtraGroupSkill(drEquipment.GivenSkillId, EquipmentSkillIDList);
        }
    }

    /// <summary>
    /// 设置装备和道具上这种额外的一组技能
    /// </summary>
    /// <param name="newSkillIds"></param>
    /// <param name="oldSkillIds">老技能id组 会直接改掉这里的源数据 不能空</param>
    public virtual bool SetExtraGroupSkill(IEnumerable<int> newSkillIds, List<int> oldSkillIds)
    {
        bool isChange = false;
        if (oldSkillIds == null)
        {
            Log.Error("SetExtraGroupSkill oldSkillIds is null");
            return isChange;
        }

        HashSet<int> newSkillIDMap = new();
        if (newSkillIds != null)
        {
            newSkillIDMap = new(newSkillIds);
        }

        //
        for (int i = oldSkillIds.Count - 1; i >= 0; i--)
        {
            //已经有的技能不需要添加
            if (newSkillIDMap.Contains(oldSkillIds[i]))
            {
                _ = newSkillIDMap.Remove(oldSkillIds[i]);
            }
            else
            {
                //删除多余的技能
                SkillComponent.RemoveSkill(oldSkillIds[i]);
                oldSkillIds.RemoveAt(i);
                isChange = true;
            }
        }

        foreach (int skillID in newSkillIDMap)
        {
            oldSkillIds.Add(skillID);
            _ = SkillComponent.AddSkill(skillID);
            isChange = true;
        }
        return isChange;
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
        int oldJumpSkillID = JumpSkillID;
        if (oldJumpSkillID != BattleDefine.JUMP_SKILL_ID_NULL)
        {
            SkillComponent.RemoveSkill(oldJumpSkillID);
        }
        JumpSkillID = skillID;
        if (JumpSkillID != BattleDefine.JUMP_SKILL_ID_NULL)
        {
            _ = SkillComponent.AddSkill(JumpSkillID);
        }

    }
}