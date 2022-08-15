using System.Collections.Generic;
using UnityGameFramework.Runtime;

/** 
 * @Author XQ
 * @Date 2022-08-11 11:54:34
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Battle/SkillConfigParse.cs
 */

/// <summary>
/// 技能配置解析
/// </summary>
public static class SkillConfigParse
{
    /// <summary>
    /// 返回技能前摇效果 可能为空
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public static List<SkillEffectBase> ParseSkillForwardEffect(int skillID, long formID, long targetID)
    {
        if (skillID <= 0)
        {
            Log.Error($"ParseSkillEffect Error skillID is error, skillID = {skillID}");
            return null;
        }

        DRSkill drSkill = GFEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);
        if (drSkill == null)
        {
            Log.Error($"ParseSkillEffect Error skillID is not exist, skillID = {skillID}");
            return null;
        }
        List<SkillEffectBase> skillEffects = ParseSkillEffect(drSkill, formID, targetID, drSkill.EffectForward);
        return skillEffects;
    }

    public static List<SkillEffectBase> ParseSkillEffect(DRSkill curSkillCfg, long formID, long targetID, int[] effectList)
    {
        List<SkillEffectBase> skillEffects = new();
        if (effectList == null || effectList.Length <= 0)
        {
            return skillEffects;
        }

        for (int i = 0; i < effectList.Length; i++)
        {
            int effectID = effectList[i];
            DRSkillEffect skillEffectCfg = GFEntry.DataTable.GetDataTable<DRSkillEffect>().GetDataRow(effectID);
            if (skillEffectCfg == null)
            {
                Log.Error($"not find skill effect skillID = {curSkillCfg.Id} effectID = {effectID}");
                continue;
            }
            try
            {
                SkillEffectBase skillBase = GFEntry.SkillEffectFactory.CreateOneSkillEffect(curSkillCfg.Id, effectID, formID, targetID, skillEffectCfg.Duration);
                skillEffects.Add(skillBase);
            }
            catch (System.Exception)
            {
                Log.Error($"ParseSkillEffect error = {curSkillCfg.Id} effectID = {effectID}");
            }
        }
        return skillEffects;
    }
}