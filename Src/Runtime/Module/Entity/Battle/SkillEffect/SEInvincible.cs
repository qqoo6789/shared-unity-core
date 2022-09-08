/** 
 * @Author XQ
 * @Date 2022-08-11 22:43:11
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SEInvincible.cs
 */

/// <summary>
/// 无敌效果
/// </summary>
public class SEInvincible : SkillEffectBase
{
    private EntityBattleDataCore _battleData;

    public override void OnAdd()
    {
        base.OnAdd();

        if (RefOwner.TryGetComponent(out _battleData))
        {
            _battleData.AddBattleState(BattleDefine.eBattleState.Invincible);
        }
    }

    public override void OnRemove()
    {
        if (_battleData != null)
        {
            _battleData.RemoveBattleState(BattleDefine.eBattleState.Invincible);
            _battleData = null;
        }

        base.OnRemove();
    }
}