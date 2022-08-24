/*
 * @Author: xiang huan
 * @Date: 2022-08-12 14:36:36
 * @Description: 受击路径移动效果
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SEBeHitPathMoveCore.cs
 * 
 */


using MelandGame3;
using UnityGameFramework.Runtime;

public class SEBeHitPathMoveCore : SEPathMoveCore
{
    public override void OnAdd()
    {
        base.OnAdd();
        EntityEvent.EntityBeHitMove?.Invoke(EffectCfg.Duration);
    }

    public override DamageEffect CreateEffectData(EntityBase fromEntity, EntityBase targetEntity)
    {
        if (EffectCfg.Parameters == null || EffectCfg.Parameters.Length <= 0)
        {
            Log.Error($"SEPathMove Parameters Error EffectID = {EffectID}");
            return null;
        }
        float distance = EffectCfg.Parameters[0] * MathUtilCore.CM2M;
        UnityEngine.Vector3 curPos = targetEntity.Position;
        UnityEngine.Vector3 moveDir = targetEntity.Position - fromEntity.Position;
        moveDir.Set(moveDir.x, 0, moveDir.z);
        UnityEngine.Vector3 targetPos = curPos + (moveDir.normalized * distance);
        DamageEffect effect = new();
        effect.EffectType = (DamageEffectId)EffectCfg.EffectType;
        effect.BeatBackValue = new();
        effect.BeatBackValue.CurLoc = NetUtilCore.LocToNet(curPos);
        effect.BeatBackValue.BackToPos = NetUtilCore.LocToNet(targetPos);
        return effect;
    }
}
