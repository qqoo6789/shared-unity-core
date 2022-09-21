/*
 * @Author: xiang huan
 * @Date: 2022-08-09 14:10:48
 * @Description: 实体CD数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/EntityCDDataCore.cs
 * 
 */
using System.Collections.Generic;
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

    /// <summary>
    /// 初始化实体CD
    /// </summary>
    public void InitSvrEntityCD(MelandGame3.EntityCD entityCD)
    {
        long curTimeStamp = TimeUtil.GetTimeStamp();
        SkillCDMap.Clear();
        if (entityCD.SkillCdList != null && entityCD.SkillCdList.Count > 0)
        {
            for (int i = 0; i < entityCD.SkillCdList.Count; i++)
            {
                if (entityCD.SkillCdList[i].Time > curTimeStamp)
                {
                    SkillCDMap[entityCD.SkillCdList[i].SkillId] = entityCD.SkillCdList[i].Time;
                }
            }
        }

        ExtendCDMap.Clear();
        if (entityCD.ExtendCdList != null && entityCD.ExtendCdList.Count > 0)
        {
            for (int i = 0; i < entityCD.ExtendCdList.Count; i++)
            {
                if (entityCD.ExtendCdList[i].Time > curTimeStamp)
                {
                    ExtendCDMap[(BattleDefine.eEntityExtCDType)entityCD.ExtendCdList[i].Type] = entityCD.ExtendCdList[i].Time;
                }
            }
        }
    }
    /// <summary>
    /// 转换成协议EntityCD格式
    /// </summary>
    public MelandGame3.EntityCD ToSvrEntityCD()
    {
        MelandGame3.EntityCD entityCD = new();
        foreach (KeyValuePair<int, long> item in SkillCDMap)
        {
            MelandGame3.EntitySkillCD skillCD = new()
            {
                SkillId = item.Key,
                Time = item.Value
            };
            entityCD.SkillCdList.Add(skillCD);
        }

        foreach (KeyValuePair<BattleDefine.eEntityExtCDType, long> item in ExtendCDMap)
        {
            MelandGame3.EntityExtendCD extendCD = new()
            {
                Type = (int)item.Key,
                Time = item.Value
            };
            entityCD.ExtendCdList.Add(extendCD);
        }
        return entityCD;
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