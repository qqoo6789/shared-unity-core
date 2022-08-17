/* 
 * @Author XQ
 * @Date 2022-08-15 19:31:15
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/EventFunction/WaitToBattleStatusEventFunc.cs
 */

using UnityGameFramework.Runtime;
/// <summary>
/// 等待进入战斗状态事件方法  和 OnInputSkillInBattleStatusEventFunc 对立
/// </summary>
public class WaitToBattleStatusEventFunc : EntityStatusEventFunctionBase
{
    public override void AddEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease += OnInputSkillRelease;
    }

    public override void RemoveEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease -= OnInputSkillRelease;
    }

    protected virtual void OnInputSkillRelease(int skillID, UnityEngine.Vector3 dir, long[] targets)
    {
        if (EntityStatus is not IEntityCanSkill entityCanSkill)
        {
            return;
        }

        if (!entityCanSkill.CheckCanSkill(skillID))
        {
            return;
        }

        OwnerFsm.SetData<VarInt32>(StatusDataDefine.SKILL_ID, skillID);
        OwnerFsm.SetData<VarVector3>(StatusDataDefine.SKILL_DIR, dir);
        OwnerFsm.SetData<VarInt64Array>(StatusDataDefine.SKILL_TARGETS, targets);
        EntityStatus.EventFuncChangeState(OwnerFsm, SkillAccumulateStatusCore.Name);
    }
}