using System;
using GameFramework.Fsm;

/// <summary>
/// 玩家技能cast状态core库
/// </summary>
public class PlayerSkillCastStatusCore : SkillCastStatusCore
{
    protected EntitySkillDataCore SkillDataCore { get; private set; }

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        SkillDataCore = StatusCtrl.GetComponent<EntitySkillDataCore>();
    }
    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        SkillDataCore = null;

        base.OnLeave(fsm, isShutdown);
    }

    /// <summary>
    /// 检查是否能够设置连击
    /// </summary>
    /// <param name="skillId"></param>
    /// <returns></returns>
    protected virtual bool CheckCanSetCombo(int skillId)
    {
        if (Array.IndexOf(SkillDataCore.ComboSkillIdArray, SkillID) < 0)
        {
            return false;
        }

        return true;
    }

    public override bool CheckCanSkill(int skillID)
    {
        if (CheckCanSetCombo(skillID))
        {
            return true;
        }

        return base.CheckCanSkill(skillID);
    }
}