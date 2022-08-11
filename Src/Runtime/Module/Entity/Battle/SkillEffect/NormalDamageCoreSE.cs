/*
 * @Author: xiang huan
 * @Date: 2022-07-19 16:19:58
 * @Description: 普通伤害效果
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/NormalDamageCoreSE.cs
 * 
 */

using MelandGame3;

public class NormalDamageCoreSE : SkillEffectBase
{
    public override bool IsRepeat => true;
    public override void Start()
    {
        if (EffectData == null)
        {
            return;
        }
        DamageEffect effect = EffectData as DamageEffect;
        if (RefOwner.TryGetComponent(out EntityBattleDataCore battleData))
        {
            battleData.SetHP(battleData.HP + effect.DamageValue.DeltaInt);
        }
    }
}