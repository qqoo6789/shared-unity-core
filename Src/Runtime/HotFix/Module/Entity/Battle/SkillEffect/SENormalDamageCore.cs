/*
* @Author: xiang huan
* @Date: 2022-07-19 16:19:58
* @Description: 普通伤害效果
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SENormalDamageCore.cs
* 
*/

using GameMessageCore;
using UnityEngine;
public class SENormalDamageCore : SkillEffectBase
{

    /// <summary>
    /// 检测能否应用效果
    /// </summary>
    /// <param name="fromEntity">发送方</param>
    /// <param name="targetEntity">接受方</param>
    public override bool CheckApplyEffect(EntityBase fromEntity, EntityBase targetEntity)
    {
        //目标方已经死亡
        if (targetEntity.BattleDataCore != null)
        {
            if (!targetEntity.BattleDataCore.IsLive())
            {
                return false;
            }
        }
        return true;
    }

    public override void Start()
    {
        base.Start();
        if (EffectData == null)
        {
            return;
        }

        if (CheckAndApplySceneDeath(EffectData.DamageValue))
        {
            return;
        }

        if (RefEntity.BattleDataCore != null)
        {
            bool isLive = RefEntity.BattleDataCore.IsLive();
            RefEntity.BattleDataCore.SetHP(EffectData.DamageValue.CurrentInt);
            if (EffectData.DamageValue.DeltaInt < 0 && isLive)
            {
                RefEntity.EntityEvent.EntityBattleAddDamage?.Invoke(FromID, -EffectData.DamageValue.DeltaInt);
            }
        }
    }

    /// <summary>
    /// 检查并应用环境死亡
    /// </summary>
    /// <param name="damageData"></param>
    /// <returns>如果是掉落死亡返回true</returns>
    protected bool CheckAndApplySceneDeath(DamageData damageData)
    {
        if (damageData.DmgState is not DamageState.Fall and not DamageState.WaterDrown)
        {
            return false;
        }

        RefEntity.BattleDataCore.SetHP(0);
        RefEntity.BattleDataCore.SetDeathReason(damageData.DmgState);
        return true;
    }
}