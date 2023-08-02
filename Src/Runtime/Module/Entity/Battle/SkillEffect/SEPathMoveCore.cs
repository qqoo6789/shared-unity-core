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
        TimerMgr.AddTimer(GetHashCode(), EffectData.BeatBackValue.DelayTime, Move);
    }

    //移动
    private void Move()
    {
        RefEntity.GetComponent<EntityEvent>().SpecialMoveStartNotMoveStatus?.Invoke();
        Vector3 targetPos = NetUtilCore.LocFromNet(EffectData.BeatBackValue.BackToPos);
        Vector3 offset = targetPos - RefEntity.Position;
        float distance = offset.magnitude;
        if (distance <= 0)
        {
            return;
        }
        float speed = distance / ((EffectCfg.Duration - EffectData.BeatBackValue.DelayTime) * TimeUtil.MS2S);
        DistanceMove.SetMoveSpeed(speed);
        DistanceMove.MoveTo(offset, distance, speed);
    }
    public override void OnRemove()
    {
        if (DistanceMove != null)
        {
            DistanceMove = null;
        }
        _ = TimerMgr.RemoveTimer(GetHashCode());
    }

    public override GameMessageCore.DamageEffect CreateEffectData(EntityBase fromEntity, EntityBase targetEntity, InputSkillReleaseData inputData)
    {
        if (EffectCfg.Parameters == null || EffectCfg.Parameters.Length <= 0)
        {
            Log.Error($"SEPathMove Parameters Error EffectID = {EffectID}");
            return null;
        }
        float distance = EffectCfg.Parameters[0] * MathUtilCore.CM2M;
        int delayTime = 0;
        if (EffectCfg.Parameters.Length > 1)
        {
            delayTime = EffectCfg.Parameters[1];
        }
        int dir = 1;
        if (EffectCfg.Parameters.Length > 2)
        {
            dir = EffectCfg.Parameters[2];
        }
        Vector3 curPos = targetEntity.Position;
        Vector3 forward = targetEntity.Forward;
        forward.Set(forward.x * dir, 0, forward.z * dir);
        Vector3 targetPos = curPos + (forward.normalized * distance);
        GameMessageCore.DamageEffect effect = new();
        effect.BeatBackValue = new();
        effect.BeatBackValue.CurLoc = NetUtilCore.LocToNet(curPos);
        effect.BeatBackValue.BackToPos = NetUtilCore.LocToNet(targetPos);
        effect.BeatBackValue.DelayTime = delayTime;
        return effect;
    }
}
