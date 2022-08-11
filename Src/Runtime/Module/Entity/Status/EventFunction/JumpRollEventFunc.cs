using UnityGameFramework.Runtime;

/** 
* @Author XQ
* @Date 2022-08-11 20:21:20
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/EventFunction/JumpRollEventFunc.cs
*/

/// <summary>
/// 需要监听翻滚技能的功能 
/// !!! 不要挂载到Idle和move这些本来就监听了技能的状态上 主要是给技能状态使用的
/// </summary>
public class JumpRollEventFunc : EntityStatusEventFunctionBase
{
    public override void AddEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease += OnInputSkillRelease;
    }

    public override void RemoveEvent(EntityEvent entityEvent)
    {
        entityEvent.InputSkillRelease -= OnInputSkillRelease;
    }

    private void OnInputSkillRelease(int skillID)
    {
        if (StatusCtrl.TryGetComponent(out PlayerRoleDataCore playerData))
        {
            //是翻滚动作
            if (playerData.DRRole.JumpRollSkill == skillID)
            {
                OwnerFsm.SetData<VarInt32>(StatusDataDefine.SKILL_ID, skillID);
                EntityStatus.EventFuncChangeState(OwnerFsm, SkillAccumulateStatusCore.Name);
                return;
            }
        }
    }
}