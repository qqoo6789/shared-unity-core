using System;
/*
 * @Author: xiang huan
 * @Date: 2022-08-12 14:36:36
 * @Description: 向目标单位移动一段距离
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SELockEnemyPathMoveCore.cs
 * 
 */


using GameMessageCore;
using UnityGameFramework.Runtime;

public class SELockEnemyPathMoveCore : SEPathMoveCore
{
    public override DamageEffect CreateEffectData(EntityBase fromEntity, EntityBase targetEntity, UnityEngine.Vector3 skillDir)
    {
        if (EffectCfg.Parameters == null || EffectCfg.Parameters.Length <= 0)
        {
            Log.Error($"SELockEnemyPathMoveCore Parameters Error EffectID = {EffectID}");
            return null;
        }
        float minDist = EffectCfg.Parameters[0] * MathUtilCore.CM2M;
        float maxDist = EffectCfg.Parameters[1] * MathUtilCore.CM2M;
        float moveDist = minDist;
        if (targetEntity != null)
        {
            float dist = UnityEngine.Vector3.Distance(fromEntity.Position, targetEntity.Position);
            if (dist > minDist)
            {
                moveDist = MathF.Min(dist - minDist, maxDist);
            }
            else
            {
                moveDist = 0;
            }
        }
        UnityEngine.Vector3 curPos = fromEntity.Position;
        UnityEngine.Vector3 moveDir = skillDir;
        moveDir.Set(moveDir.x, 0, moveDir.z);
        UnityEngine.Vector3 targetPos = curPos + (moveDir.normalized * moveDist);
        DamageEffect effect = new();
        effect.BeatBackValue = new();
        effect.BeatBackValue.CurLoc = NetUtilCore.LocToNet(curPos);
        effect.BeatBackValue.BackToPos = NetUtilCore.LocToNet(targetPos);
        return effect;
    }
}
