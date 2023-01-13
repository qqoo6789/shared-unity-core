/*
 * @Author: xiang huan
 * @Date: 2022-08-09 14:10:48
 * @Description: 实体技能数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/EntitySkillDataCore.cs
 * 
 */
using System.Collections.Generic;
using GameMessageCore;
using UnityGameFramework.Runtime;

public class EntitySkillDataCore : EntityBaseComponent
{
    protected List<int> EquipmentSkillIDList = new(); //装备技能列表
    protected List<int> BaseSkillIDList = new(); //基础技能列表

    protected SkillCpt SkillComponent; //技能组件
    protected PlayerRoleDataCore RoleDataCore;//角色数据
    private void Awake()
    {
        SkillComponent = gameObject.GetComponent<SkillCpt>();
        RoleDataCore = gameObject.GetComponent<PlayerRoleDataCore>();
    }

    private void Start()
    {
        RefEntity.EntityEvent.EntityAvatarUpdated += OnUpdateEquipmentSkillID;
        OnUpdateEquipmentSkillID();
    }
    public void AddBaseSkillList(int[] skillIDs)
    {
        RemoveBaseSkillList();
        BaseSkillIDList.AddRange(skillIDs);
        for (int i = 0; i < BaseSkillIDList.Count; i++)
        {
            SkillComponent.AddSkill(BaseSkillIDList[i]);
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
        HashSet<int> newSkillIDMap = new();
        if (RoleDataCore.WearDic.TryGetValue(GameMessageCore.AvatarPosition.Weapon, out PlayerAvatar avatar))
        {
            DREquipment drEquipment = GFEntryCore.DataTable.GetDataTable<DREquipment>().GetDataRow(avatar.ObjectId);
            if (drEquipment != null)
            {
                for (int i = 0; i < drEquipment.GivenSkillId.Length; i++)
                {
                    newSkillIDMap.Add(drEquipment.GivenSkillId[i]);
                }
            }
            else
            {
                Log.Error($"OnUpdateEquipmentSkillID not find equipment id:{avatar.ObjectId}");
            }
        }
        //
        for (int i = EquipmentSkillIDList.Count - 1; i >= 0; i--)
        {
            //已经有的技能不需要添加
            if (newSkillIDMap.Contains(EquipmentSkillIDList[i]))
            {
                newSkillIDMap.Remove(EquipmentSkillIDList[i]);
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
            SkillComponent.AddSkill(skillID);
        }
    }
    private void OnDestroy()
    {
        RemoveBaseSkillList();
        RefEntity.EntityEvent.EntityAvatarUpdated -= OnUpdateEquipmentSkillID;
    }
}