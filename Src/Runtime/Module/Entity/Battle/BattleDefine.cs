/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:51:41
 * @Description: 战斗公共定义
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/BattleDefine.cs
 * 
 */

public static class BattleDefine
{
    public enum eSkillEffectType : int
    {
        EffectIdUnknown = 0,
        NormalDamageSE = 1001,
    }

    public enum eSkillShapeId : int
    {
        IdUnknown = 0,
        SkillShapeBox = 1,
        SkillShapeSphere = 2,
        SkillShapeCapsule = 3,
    }
}