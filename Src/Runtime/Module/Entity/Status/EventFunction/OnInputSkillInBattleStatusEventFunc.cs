/* 
 * @Author XQ
 * @Date 2022-08-15 11:15:06
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/EventFunction/OnInputSkillInBattleStatusEventFunc.cs
 */

using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 在战斗状态中监听到输入技能  和 WaitToBattleStatusEventFunc 对立
/// </summary>
public class OnInputSkillInBattleStatusEventFunc : EntityStatusEventFunctionBase
{
    public override void AddEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease += OnInputSkillRelease;
    }

    public override void RemoveEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease -= OnInputSkillRelease;
    }

    private void OnInputSkillRelease(int skillID, Vector3 dir, long[] targets)
    {
        if (StatusCtrl.TryGetComponent(out PlayerRoleDataCore playerData))
        {
            //是翻滚动作
            if (playerData.DRRole.JumpRollSkill == skillID)
            {
                EntityStatus.EntityEvent.StartJumpRoll?.Invoke();

                OwnerFsm.SetData<VarInt32>(StatusDataDefine.SKILL_ID, skillID);
                OwnerFsm.SetData<VarVector3>(StatusDataDefine.SKILL_DIR, dir);
                OwnerFsm.SetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS, targets);
                EntityStatus.EventFuncChangeState(OwnerFsm, SkillAccumulateStatusCore.Name);
                return;
            }
        }
    }
}