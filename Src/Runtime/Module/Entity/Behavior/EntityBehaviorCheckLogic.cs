/// <summary>
/// 实体行为检查的查询逻辑  用来判定是否能够移动 能够攻击的判定逻辑
/// </summary>
public class EntityBehaviorCheckLogic
{
    /// <summary>
    /// 检查实体是否能够移动
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool CheckEntityCanMove(EntityBase entity)
    {
        if (entity == null)
        {
            return false;
        }

        if (!entity.TryGetComponent<EntityStatusCtrl>(out EntityStatusCtrl entityStatusCtrl))
        {
            return false;
        }

        if (!(entityStatusCtrl.Fsm.CurrentState is IEntityCanMove))
        {
            return false;
        }

        IEntityCanMove judge = entityStatusCtrl.Fsm.CurrentState as IEntityCanMove;
        return judge.CheckCanMove();
    }

    /// <summary>
    /// 检查实体是否能够放技能
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool CheckEntityCanSkill(EntityBase entity)
    {
        if (entity == null)
        {
            return false;
        }

        if (!entity.TryGetComponent<EntityStatusCtrl>(out EntityStatusCtrl entityStatusCtrl))
        {
            return false;
        }

        if (!(entityStatusCtrl.Fsm.CurrentState is IEntityCanSkill))
        {
            return false;
        }

        IEntityCanSkill judge = entityStatusCtrl.Fsm.CurrentState as IEntityCanSkill;
        return judge.CheckCanSkill();
    }
}