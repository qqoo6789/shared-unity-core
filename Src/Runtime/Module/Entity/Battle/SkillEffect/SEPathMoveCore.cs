/*
 * @Author: xiang huan
 * @Date: 2022-08-12 14:36:36
 * @Description: 技能路径移动效果
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SEPathMoveCore.cs
 * 
 */


using UnityEngine;
using UnityGameFramework.Runtime;

public class SEPathMoveCore : SkillEffectBase
{
    protected PathMove PathMove;
    public override void Start()
    {
        if (EffectData == null)
        {
            return;
        }
        if (!RefOwner.TryGetComponent(out PathMove))
        {
            Log.Error($"SEPathMoveCore not find PathMove cpt");
            return;
        }

        RefOwner.GetComponent<EntityEvent>().SpecialMoveStartNotMoveStatus?.Invoke();

        Vector3 targetPos = NetUtilCore.LocFromNet(EffectData.BeatBackValue.BackToPos);
        float distance = Vector3.Distance(RefOwner.transform.position, targetPos);
        float speed = distance / (EffectCfg.Duration * TimeUtil.MS2S);
        PathMove.SetMoveSpeed(speed);
        _ = PathMove.TryMoveToPoint(targetPos);
    }

    public override void OnRemove()
    {
        if (PathMove != null)
        {
            PathMove = null;
        }
    }

    public override MelandGame3.DamageEffect CreateEffectData(EntityBase fromEntity, EntityBase targetEntity, UnityEngine.Vector3 skillDir)
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
        MelandGame3.DamageEffect effect = new();
        effect.BeatBackValue = new();
        effect.BeatBackValue.CurLoc = NetUtilCore.LocToNet(curPos);
        effect.BeatBackValue.BackToPos = NetUtilCore.LocToNet(targetPos);
        return effect;
    }
}
