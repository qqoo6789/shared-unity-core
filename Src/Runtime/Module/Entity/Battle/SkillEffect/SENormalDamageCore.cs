/*
 * @Author: xiang huan
 * @Date: 2022-07-19 16:19:58
 * @Description: 普通伤害效果
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SENormalDamageCore.cs
 * 
 */


public class SENormalDamageCore : SkillEffectBase
{
    public override bool IsRepeat => true;

    /// <summary>
    /// 检测能否应用效果
    /// </summary>
    /// <param name="fromEntity">发送方</param>
    /// <param name="targetEntity">接受方</param>
    public override bool CheckApplyEffect(EntityBase fromEntity, EntityBase targetEntity)
    {
        //目标方已经死亡
        if (targetEntity.TryGetComponent(out EntityBattleDataCore targetBattleData))
        {
            if (!targetBattleData.IsLive())
            {
                return false;
            }
        }
        return true;
    }

    public override void Start()
    {
        if (EffectData == null)
        {
            return;
        }
        if (RefOwner.TryGetComponent(out EntityBattleDataCore battleData))
        {
            battleData.SetHP(EffectData.DamageValue.CurrentInt);
            if (RefOwner.TryGetComponent(out EntityBattleRecordData battleRecordData))
            {
                battleRecordData.AddDamageRecord(FromID, EffectData.DamageValue.DeltaInt, battleData.IsLive());
            }
        }
    }
}