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
    protected Vector3 MovePos;

    public override void OnAdd()
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
        Vector3 targetPos = NetUtilCore.LocFromNet(EffectData.BeatBackValue.BackToPos);
        float distance = Vector3.Distance(RefOwner.transform.position, targetPos);
        float time = distance / (EffectCfg.Duration * TimeUtil.MS2S);
        PathMove.enabled = true;
        PathMove.SetMoveSpeed(time);
        PathMove.MovePoint(MovePos);
    }

    public override void OnRemove()
    {
        if (PathMove != null)
        {
            PathMove.enabled = false;
            PathMove = null;
        }
    }
}
