/* 
 * @Author XQ
 * @Date 2022-08-15 11:15:06
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/EventFunction/OnInputSkillInBattleStatusEventFunc.cs
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

    private void OnInputSkillRelease(InputSkillReleaseData inputData)
    {
        StatusCtrl.transform.forward = inputData.Dir;

        OwnerFsm.SetData<VarInputSkill>(StatusDataDefine.SKILL_INPUT, inputData);
        EntityStatus.EventFuncChangeState(OwnerFsm, SkillAccumulateStatusCore.Name);
    }
}