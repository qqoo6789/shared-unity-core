/*
 * @Author: xiang huan
 * @Date: 2022-08-09 14:10:48
 * @Description: 实体CD数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/EntityCDDataCore.cs
 * 
 */
using System.Collections.Generic;
using Google.Protobuf.Collections;
public class EntityCDDataCore : EntityBaseComponent
{
    /// <summary>
    /// 技能CD int 技能ID  long 到期时间戳ms
    /// </summary>
    public Dictionary<int, long> SkillCDMap { get; private set; }
    /// <summary>
    /// 扩展CD BattleDefine.eEntityExtCDType CD类型  long 到期时间戳ms
    /// </summary>
    public Dictionary<BattleDefine.eEntityExtCDType, long> ExtendCDMap { get; private set; }
    protected virtual void Awake()
    {
        SkillCDMap = new();
        ExtendCDMap = new();
    }

    public void InitSkillCD(Dictionary<int, long> skillCDMap)
    {
        if (skillCDMap == null || skillCDMap.Count <= 0)
        {
            return;
        }
        long curTimeStamp = TimeUtil.GetTimeStamp();
        foreach (KeyValuePair<int, long> item in skillCDMap)
        {
            if (item.Value > curTimeStamp)
            {
                SkillCDMap[item.Key] = item.Value;
            }
        }
    }

    public void InitSkillCD(RepeatedField<MelandGame3.EntitySkillCD> skillCDList)
    {
        if (skillCDList == null || skillCDList.Count <= 0)
        {
            return;
        }
        long curTimeStamp = TimeUtil.GetTimeStamp();
        for (int i = 0; i < skillCDList.Count; i++)
        {
            if (skillCDList[i].Time > curTimeStamp)
            {
                SkillCDMap[skillCDList[i].SkillId] = skillCDList[i].Time;
            }
        }
    }
    /// <summary>
    /// 是否技能CD
    /// </summary>
    public bool IsSkillCD(int skillID)
    {
        return GetSkillCD(skillID) > 0;
    }

    /// <summary>
    /// 获得技能CD
    /// </summary>
    public long GetSkillCD(int skillID)
    {
        if (SkillCDMap.TryGetValue(skillID, out long value))
        {
            long curTimeStamp = TimeUtil.GetTimeStamp();
            return value > curTimeStamp ? value - curTimeStamp : 0;
        }
        return 0;
    }

    /// <summary>
    /// 重置技能CD
    /// </summary>
    public void ResetSkillCD(int skillID)
    {
        long curTimeStamp = TimeUtil.GetTimeStamp();
        long skillCD = SkillUtil.CalculateSkillCD(skillID, RefEntity);
        long cdTime = curTimeStamp + skillCD;
        SkillCDMap[skillID] = cdTime;
    }

    public void InitExtendCD(Dictionary<BattleDefine.eEntityExtCDType, long> cdMap)
    {
        if (cdMap == null || cdMap.Count <= 0)
        {
            return;
        }
        long curTimeStamp = TimeUtil.GetTimeStamp();
        foreach (KeyValuePair<BattleDefine.eEntityExtCDType, long> item in cdMap)
        {
            if (item.Value > curTimeStamp)
            {
                ExtendCDMap[item.Key] = item.Value;
            }
        }
    }
    public void InitExtendCD(RepeatedField<MelandGame3.EntityExtendCD> extendCDList)
    {
        if (extendCDList == null || extendCDList.Count <= 0)
        {
            return;
        }
        long curTimeStamp = TimeUtil.GetTimeStamp();
        for (int i = 0; i < extendCDList.Count; i++)
        {
            if (extendCDList[i].Time > curTimeStamp)
            {
                ExtendCDMap[(BattleDefine.eEntityExtCDType)extendCDList[i].Type] = extendCDList[i].Time;
            }
        }
    }
    /// <summary>
    /// 设置扩展CD
    /// </summary>
    public void SetExtendCD(BattleDefine.eEntityExtCDType type, long time)
    {
        ExtendCDMap[type] = time;
    }
    /// <summary>
    /// 是否扩展CD中
    /// </summary>
    public bool IsExtendCD(BattleDefine.eEntityExtCDType type)
    {
        if (ExtendCDMap.TryGetValue(type, out long outTime))
        {
            long curTimeStamp = TimeUtil.GetTimeStamp();
            return outTime > curTimeStamp;
        }
        return false;
    }
    /// <summary>
    /// 获得扩展CD
    /// </summary>
    public long GetExtendCD(BattleDefine.eEntityExtCDType type)
    {
        if (ExtendCDMap.TryGetValue(type, out long value))
        {
            return value;
        }
        return 0;
    }
    /// <summary>
    /// 重置所有技能CD
    /// </summary>
    public void ResetAllSkillCD()
    {
        SkillCDMap.Clear();
    }
    /// <summary>
    /// 重置所有CD
    /// </summary>
    public void ResetAllCD()
    {
        SkillCDMap.Clear();
        ExtendCDMap.Clear();
    }

}