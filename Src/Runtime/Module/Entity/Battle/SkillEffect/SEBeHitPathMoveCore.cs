/*
 * @Author: xiang huan
 * @Date: 2022-08-12 14:36:36
 * @Description: 受击路径移动效果
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SEBeHitPathMoveCore.cs
 * 
 */


public class SEBeHitPathMoveCore : SEPathMoveCore
{
    public override void OnAdd()
    {
        base.OnAdd();
        EntityEvent.EntityBeHitMove?.Invoke(EffectCfg.Duration);
    }
}
