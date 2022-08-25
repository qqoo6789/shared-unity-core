/*
 * @Author: xiang huan
 * @Date: 2022-08-22 15:50:05
 * @Description: 实体战斗记录数据
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Entity/EntityBattleRecordData.cs
 * 
 */

using System.Collections.Generic;
using UnityEngine;
public class EntityBattleRecordData : MonoBehaviour
{
    /// <summary>
    /// Dictionary<来源实体ID，造成总伤害>
    /// </summary>
    public Dictionary<long, int> DamageRecordMap { get; private set; } //实体伤害记录
    public long EndDamageEntityID { get; private set; } //最后造成伤害的实体
    private void Start()
    {
        DamageRecordMap = new();
    }
    public void ResetDamageRecord()
    {
        DamageRecordMap.Clear();
        EndDamageEntityID = 0;
    }

    /// <summary>
    /// 添加伤害记录
    /// </summary>
    /// <param name="formId">伤害来源ID</param>
    /// <param name="damageNum">伤害数值</param>
    /// <param name="isLive">当前实体是否存活</param>
    public void AddDamageRecord(long formId, int damageNum, bool isLive)
    {
        if (DamageRecordMap.TryGetValue(entityId, out int value))
        {
            DamageRecordMap[entityId] = value + damageNum;
        }
        else
        {
            DamageRecordMap.Add(entityId, damageNum);
        }
        if (!isLive && EndDamageEntityID == 0)
        {
            EndDamageEntityID = entityId;
        }
    }
}