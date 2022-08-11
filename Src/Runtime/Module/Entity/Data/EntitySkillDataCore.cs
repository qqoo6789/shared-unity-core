/*
 * @Author: xiang huan
 * @Date: 2022-08-09 14:10:48
 * @Description: 实体技能数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Entity/EntitySkillDataCore.cs
 * 
 */
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Collections.Generic;

public class EntitySkillDataCore : MonoBehaviour
{
    /// <summary>
    /// 技能CD
    /// </summary>
    public Dictionary<int, long> CDMap { get; private set; }
    private void Awake()
    {
        CDMap = new();
    }

    /// <summary>
    /// 获得技能CD
    /// </summary>
    public long GetSkillCD(int skillID)
    {
        if (CDMap.TryGetValue(skillID, out long value))
        {
            long curTimeStamp = TimeUtil.GetTimeStamp();
            return value - curTimeStamp;
        }
        return 0;
    }

    /// <summary>
    /// 重置技能CD
    /// </summary>
    public void ResetSkillCD(int skillID)
    {
        DRSkill CurSkillCfg = GFEntry.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);
        if (CurSkillCfg == null)
        {
            Log.Error($"EntitySkillDataCore GetSkillCD Error skillID = {skillID}");
            return;
        }
        long curTimeStamp = TimeUtil.GetTimeStamp();
        long cdTime = curTimeStamp + CurSkillCfg.SkillCD;
        CDMap[skillID] = cdTime;
    }

    /// <summary>
    /// 重置所有技能CD
    /// </summary>
    public void ResetAllSkillCD()
    {
        CDMap.Clear();
    }
}