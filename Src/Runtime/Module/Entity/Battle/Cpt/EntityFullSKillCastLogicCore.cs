using UnityEngine;

/// <summary>
/// 实体完成的技能释放过程逻辑状态 也能看做一个完成技能释放状态
/// 因为现在前摇和后摇状态之间割裂 这个逻辑会将前摇和后摇的逻辑合并 来处理特效什么的很方便
/// 如果前摇非正常打断了  这里也会提前离开
/// </summary>
public class EntityFullSKillCastLogicCore : EntityBaseComponent
{
    protected DRSkill DRSkill { get; private set; }
    protected long[] Targets { get; private set; }
    protected Vector3 SkillDir { get; private set; }

    public virtual void OnEnter(DRSkill dRSkill, long[] targets, Vector3 skillDir)
    {
        DRSkill = dRSkill;
        Targets = targets;
        SkillDir = skillDir;
    }

    public virtual void OnLeave()
    {
        DRSkill = null;
        Targets = null;
    }
}