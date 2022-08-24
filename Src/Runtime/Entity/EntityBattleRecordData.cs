/*
 * @Author: xiang huan
 * @Date: 2022-08-22 15:50:05
 * @Description: 实体战斗记录数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Entity/EntityBattleRecordData.cs
 * 
 */

using System.Collections.Generic;
using UnityEngine;
public class EntityBattleRecordData : MonoBehaviour
{
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

    public void AddDamageRecord(long entityId, int damageNum, bool isLive)
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