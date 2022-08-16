/* 
 * @Author XQ
 * @Date 2022-08-05 12:54:15
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Behavior/EntityBehaviorCheckLogic.cs
 */

using GameFramework.Fsm;

/// <summary>
/// 实体行为检查的查询逻辑  用来判定是否能够移动 能够攻击的判定逻辑
/// </summary>
public static class EntityBehaviorCheckLogic
{
    /// <summary>
    /// 获取实体的当前状态 找不到为null
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static FsmState<EntityStatusCtrl> GetCurrentStatus(EntityBase entity)
    {
        if (entity == null)
        {
            return null;
        }

        if (!entity.TryGetComponent(out EntityStatusCtrl entityStatusCtrl))
        {
            return null;
        }

        return entityStatusCtrl.Fsm.CurrentState;
    }

    /// <summary>
    /// 实体是否在idle闲置状态
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool IsIdleStatus(EntityBase entity)
    {
        FsmState<EntityStatusCtrl> curStatus = GetCurrentStatus(entity);
        if (curStatus == null)
        {
            return false;
        }

        return curStatus is IdleStatusCore;
    }

    /// <summary>
    /// 实体是否在移动状态
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool IsMovingStatus(EntityBase entity)
    {
        FsmState<EntityStatusCtrl> curStatus = GetCurrentStatus(entity);
        if (curStatus == null)
        {
            return false;
        }

        return curStatus is DirectionMoveStatusCore or PathMoveStatusCore;
    }

    /// <summary>
    /// 实体是否在战斗状态
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool IsBattleStatus(EntityBase entity)
    {
        FsmState<EntityStatusCtrl> curStatus = GetCurrentStatus(entity);
        if (curStatus == null)
        {
            return false;
        }

        return curStatus is SkillAccumulateStatusCore or SkillForwardStatusCore or SkillCastStatusCore;
    }

    /// <summary>
    /// 检查实体是否能够移动
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool CheckEntityCanMove(EntityBase entity)
    {
        FsmState<EntityStatusCtrl> curStatus = GetCurrentStatus(entity);
        if (curStatus == null)
        {
            return false;
        }

        if (curStatus is not IEntityCanMove judge)
        {
            return false;
        }

        return judge.CheckCanMove();
    }

    /// <summary>
    /// 检查实体是否能够放技能
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool CheckEntityCanSkill(EntityBase entity)
    {
        FsmState<EntityStatusCtrl> curStatus = GetCurrentStatus(entity);
        if (curStatus == null)
        {
            return false;
        }

        if (curStatus is not IEntityCanSkill judge)
        {
            return false;
        }

        return judge.CheckCanSkill();
    }
}