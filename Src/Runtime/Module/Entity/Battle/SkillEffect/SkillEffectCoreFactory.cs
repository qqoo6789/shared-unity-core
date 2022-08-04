/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:49:14
 * @Description: 技能效果球工厂
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SkillEffectCoreFactory.cs
 * 
 */
using System;
using System.Collections.Generic;
using UnityGameFramework.Runtime;


public class SkillEffectCoreFactory
{
    protected Dictionary<BattleDefine.eSkillEffectId, Type> SkillEffectMap;

    /// <summary>
    /// 初始化效果工厂Map
    /// </summary>
    public virtual void InitSkillEffectMap()
    {
        SkillEffectMap = new();
    }
    /// <summary>
    /// 创建技能效果
    /// </summary>
    /// <param name="skillID">技能ID</param>
    /// <param name="effectID">效果ID</param>
    /// <param name="fromID">技能释放者ID</param>
    /// <param name="targetID">技能接收ID</param>
    /// <param name="duration">技能持续时间 小于0代表一致持续  0代表立即执行销毁  大于0即到时自动销毁</param>
    /// <returns></returns>
    public SkillEffectBase CreateOneSkillEffect(int skillID, int effectID, long fromID, long targetID, int duration = 0)
    {
        if (SkillEffectMap == null)
        {
            Log.Error($"createOneSkillEffect Error not init skill effect map");
            return null;
        }
        if (!SkillEffectMap.ContainsKey((BattleDefine.eSkillEffectId)effectID))
        {
            Log.Error($"createOneSkillEffect Error effectID is Unknown  skillID = {skillID} effectID = {effectID}");
            return null;
        }

        _ = SkillEffectMap.TryGetValue((BattleDefine.eSkillEffectId)effectID, out Type skillEffectClass);
        SkillEffectBase effect = SkillEffectBase.Create(skillEffectClass);
        effect.SetData(skillID, effectID, fromID, targetID, duration);
        return effect;
    }
}