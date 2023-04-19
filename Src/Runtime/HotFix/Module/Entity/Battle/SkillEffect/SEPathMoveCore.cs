/*
 * @Author: xiang huan
 * @Date: 2022-08-12 14:36:36
 * @Description: 技能路径移动效果
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SEPathMoveCore.cs
 * 
 */


using UnityEngine;
using UnityGameFramework.Runtime;

public class SEPathMoveCore : SkillEffectBase
{
    protected DistanceMove DistanceMove;
    public override void Start()
    {
        base.Start();
        if (EffectData == null)
        {
            return;
        }
        if (!RefEntity.TryGetComponent(out DistanceMove))
        {
            Log.Error($"SEPathMoveCore not find DistanceMove cpt");
            return;
        }

        RefEntity.GetComponent<EntityEvent>().SpecialMoveStartNotMoveStatus?.Invoke();

        Vector3 targetPos = NetUtilCore.LocFromNet(EffectData.BeatBackValue.BackToPos);
        Vector3 offset = targetPos - RefEntity.Position;
        float distance = offset.magnitude;
        if (distance <= 0)
        {
            return;
        }
        float speed = distance / (EffectCfg.Duration * TimeUtil.MS2S);
        DistanceMove.SetMoveSpeed(speed);
        DistanceMove.MoveTo(offset, distance, speed);
    }

    public override void OnRemove()
    {
        if (DistanceMove != null)
        {
            DistanceMove = null;
        }
    }

    public override GameMessageCore.DamageEffect CreateEffectData(EntityBase fromEntity, EntityBase targetEntity, UnityEngine.Vector3 skillDir, long[] targets)
    {
        if (EffectCfg.Parameters == null || EffectCfg.Parameters.Length <= 0)
        {
            Log.Error($"SEPathMove Parameters Error EffectID = {EffectID}");
            return null;
        }
        float distance = EffectCfg.Parameters[0] * MathUtilCore.CM2M;
        Vector3 curPos = targetEntity.Position;
        Vector3 forward = targetEntity.Forward;
        forward.Set(forward.x, 0, forward.z);
        Vector3 targetPos = curPos + (forward.normalized * distance);
        GameMessageCore.DamageEffect effect = new();
        effect.BeatBackValue = new();
        effect.BeatBackValue.CurLoc = NetUtilCore.LocToNet(curPos);
        effect.BeatBackValue.BackToPos = NetUtilCore.LocToNet(targetPos);
        return effect;
    }
}