/// <summary>
/// 眩晕技能效果球
/// </summary>
public class SEStunCore : SkillEffectBase
{
    public override void OnAdd()
    {
        base.OnAdd();

        if (RefEntity.BattleDataCore != null)
        {
            RefEntity.BattleDataCore.AddBattleState(BattleDefine.eBattleState.Stun);
        }

        RefEntity.EntityEvent.EntityReceiveStunEffect?.Invoke();
    }

    public override void OnRemove()
    {
        if (RefEntity.BattleDataCore != null)
        {
            RefEntity.BattleDataCore.RemoveBattleState(BattleDefine.eBattleState.Stun);
        }

        RefEntity.EntityEvent.EntityRemoveStunEffect?.Invoke();
        base.OnRemove();
    }

    public override bool CheckApplyEffect(EntityBase fromEntity, EntityBase targetEntity)
    {

        if (RefEntity.BattleDataCore != null)
        {
            //霸体状态不应该进入击退状态
            if (RefEntity.BattleDataCore.HasBattleState(BattleDefine.eBattleState.Endure))
            {
                return false;
            }
        }

        return base.CheckApplyEffect(fromEntity, targetEntity);
    }
}