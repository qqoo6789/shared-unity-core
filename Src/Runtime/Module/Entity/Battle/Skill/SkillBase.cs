/*
 * @Author: xiang huan
 * @Date: 2023-01-06 10:52:11
 * @Description:  技能基础, 用了引用池，记住继承Clear清除数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/Skill/SkillBase.cs
 * 
 */
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

public class SkillBase : IReference
{
    public int SkillID { get; private set; }
    public DRSkill DRSkill { get; private set; }
    /// <summary>
    ///技能标识
    /// </summary>
    public int SkillFlag { get; private set; }
    /// <summary>
    ///技能释放目标标识
    /// </summary>
    public int TargetFlag { get; private set; }

    /// <summary>
    /// 宿主对象
    /// </summary>
    public EntityBase RefEntity { get; private set; }
    public void SetData(int skillID)
    {
        SkillID = skillID;
        DRSkill = GFEntryCore.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);
        if (DRSkill == null)
        {
            Log.Error("The skill ID was not found in the skill table:{0}", skillID);
            return;

        }
        SkillFlag = 0;
        if (DRSkill.SkillFlag != null)
        {
            for (int i = 0; i < DRSkill.SkillFlag.Length; i++)
            {
                SkillFlag |= 1 << DRSkill.SkillFlag[i];
            }
        }
        TargetFlag = 0;
        if (DRSkill.TargetFlag != null)
        {
            for (int i = 0; i < DRSkill.TargetFlag.Length; i++)
            {
                TargetFlag |= 1 << DRSkill.TargetFlag[i];
            }
        }
    }
    /// <summary>
    /// 技能被添加
    /// </summary>
    public virtual void OnAdd(EntityBase owner)
    {
        RefEntity = owner;
        _ = SkillUtil.EntitySkillEffectExecute(DRSkill, Vector3.zero, null, DRSkill.EffectInit, RefEntity, RefEntity);
    }

    /// <summary>
    /// 技能被移除
    /// </summary>
    public virtual void OnRemove()
    {
        SkillUtil.EntityAbolishSkillEffect(DRSkill, DRSkill.EffectInit, RefEntity, RefEntity);
        RefEntity = null;
    }

    /// <summary>
    /// 技能开启
    /// </summary>
    public virtual void OnToggleOn()
    {

    }

    /// <summary>
    /// 技能关闭
    /// </summary>
    public virtual void OnToggleOff()
    {

    }
    public virtual void Clear()
    {
        SkillID = 0;
        DRSkill = null;
        RefEntity = null;
    }

    /// <summary>
    /// 创建技能
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Create<T>(int skillID) where T : SkillBase, new()
    {
        T skill = ReferencePool.Acquire<T>();
        skill.SetData(skillID);
        return skill;
    }

    /// <summary>
    /// 效果被销毁
    /// </summary>
    public void Dispose()
    {
        ReferencePool.Release(this);
    }
}