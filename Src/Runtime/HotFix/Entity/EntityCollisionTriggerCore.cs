/*
 * @Author: xiang huan
 * @Date: 2022-08-26 14:25:46
 * @Description: 实体层碰撞触发组件
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Entity/EntityCollisionTriggerCore.cs
 * 
 */
using CMF;
using UnityEngine;
using System.Collections.Generic;

public class EntityCollisionTrigger : MonoBehaviour
{
    public EntityBase RefEntity { get; private set; }
    public void Init(EntityBase entity)
    {
        RefEntity = entity;
    }
    public HashSet<long> EntityTriggerSet = new(); //触碰实体Map
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & (1 << MLayerMask.ENTITY_TRIGGER)) == 0)
        {
            return;
        }
        EntityCollisionTrigger trigger = other.gameObject.GetComponent<EntityCollisionTrigger>();
        if (trigger != null && trigger.RefEntity != null && trigger.RefEntity.Inited)
        {
            if (!EntityTriggerSet.Contains(trigger.RefEntity.BaseData.Id))
            {
                _ = EntityTriggerSet.Add(trigger.RefEntity.BaseData.Id);
            }
            RefEntity.EntityEvent.EntityTriggerEnter?.Invoke(trigger.RefEntity);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & (1 << MLayerMask.ENTITY_TRIGGER)) <= 0)
        {
            return;
        }
        //TODO: 可能在实体销毁时这里无法清除
        EntityCollisionTrigger otherTrigger = other.gameObject.GetComponent<EntityCollisionTrigger>();
        if (otherTrigger != null && otherTrigger.RefEntity != null && otherTrigger.RefEntity.Inited)
        {
            _ = EntityTriggerSet.Remove(otherTrigger.RefEntity.BaseData.Id);
            RefEntity.EntityEvent.EntityTriggerExit?.Invoke(otherTrigger.RefEntity);
        }
    }
}