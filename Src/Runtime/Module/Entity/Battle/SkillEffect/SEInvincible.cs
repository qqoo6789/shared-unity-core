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
    public override void OnAdd()
    {
        base.OnAdd();

        if (RefOwner.TryGetComponent(out EntityBattleDataCore battleData))
        {
            battleData.ChangeInvincible(true);
        }
    }

    public override void OnRemove()
    {
        if (RefOwner.TryGetComponent(out EntityBattleDataCore battleData))
        {
            battleData.ChangeInvincible(false);
        }

        base.OnRemove();
    }
}