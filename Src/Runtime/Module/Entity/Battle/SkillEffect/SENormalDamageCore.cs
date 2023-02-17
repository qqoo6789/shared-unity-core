/*
* @Author: xiang huan
* @Date: 2022-07-19 16:19:58
* @Description: 普通伤害效果
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SENormalDamageCore.cs
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

        if (CheckAndApplyFallDeath(EffectData.DamageValue))
        {
            return;
        }

        if (RefEntity.BattleDataCore != null)
        {
            RefEntity.BattleDataCore.SetHP(EffectData.DamageValue.CurrentInt);
            if (EffectData.DamageValue.DeltaInt < 0)
            {
                if (RefEntity.TryGetComponent(out EntityBattleRecordDataCore battleRecordData))
                {
                    battleRecordData.AddDamageRecord(FromID, Mathf.Abs(EffectData.DamageValue.DeltaInt), RefEntity.BattleDataCore.IsLive());
                }

                RefEntity.EntityEvent.GetHurt?.Invoke(-EffectData.DamageValue.DeltaInt);
            }
        }
    }

    /// <summary>
    /// 检查并应用掉落死亡
    /// </summary>
    /// <param name="damageData"></param>
    /// <returns>如果是掉落死亡返回true</returns>
    protected bool CheckAndApplyFallDeath(DamageData damageData)
    {
        if (damageData.DmgState != DamageState.Fall)
        {
            return false;
        }

        RefEntity.BattleDataCore.SetHP(0);
        RefEntity.BattleDataCore.SetDeathReason(DamageState.Fall);
        return true;
    }
}