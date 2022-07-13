using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class EntityMgr : MonoBehaviour
{
    /// <summary>
    /// 场景所有实体 包括了主角
    /// </summary>
    /// <returns></returns>
    protected readonly Dictionary<long, EntityBase> EntityDic = new();
    /// <summary>
    /// 实体工厂，用于创建实体
    /// </summary>
    private EntityFactory _factory;
    protected EntityFactory Factory => _factory ??= GetFactory();

    /// <summary>
    /// 获取存在的场景实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns>如果没有会返回null</returns>
    public EntityBase GetEntity(long id)
    {
        //EntityBase是个复杂对象的时候，可以用这个方法来获取效率会更高，有待观察 https://blog.csdn.net/sigmeta/article/details/121534293
        // if (EntityDic.ContainsKey(id))
        // {
        //     return EntityDic[id];
        // }

        if (EntityDic.TryGetValue(id, out EntityBase entity))
        {
            return entity;
        }

        Log.Warning($"Can not find entity with id {id}");
        return null;
    }

    /// <summary>
    /// 添加一个场景实体 主角使用另外一个方法
    /// </summary>
    /// <param name="entityID"></param>
    /// <param name="entityType"></param>
    /// <returns></returns>
    public virtual EntityBase AddEntity(long entityID, eEntityType entityType)
    {
        if (EntityDic.ContainsKey(entityID))
        {
            Log.Error($"Entity {entityID} already exist,type={entityType}");
            RemoveEntity(entityID);
        }

        EntityBase entity = CreateEntity(entityID, entityType);
        try
        {
            entity.Init();
        }
        catch (Exception e)
        {
            Log.Error($"Entity {entityID} init failed,type={entityType},error={e}");
        }
        EntityDic.Add(entityID, entity);

        return entity;
    }

    /// <summary>
    /// 移除一个场景实体 主角不使用这个方法
    /// </summary>
    /// <param name="entityID"></param>
    public void RemoveEntity(long entityID)
    {
        if (!EntityDic.TryGetValue(entityID, out EntityBase entity))
        {
            Log.Error($"Entity {entityID} not exist");
            return;
        }

        _ = EntityDic.Remove(entityID);
        try
        {
            entity.Dispose();
        }
        catch (Exception e)
        {
            Log.Error($"Entity {entityID} dispose failed,error={e}");
        }
    }

    protected virtual EntityBase CreateEntity(long entityID, eEntityType entityType)
    {
        return Factory.CreateSceneEntity<EntityBase>(entityID, entityType);
    }

    /// <summary>
    /// 获取实体工厂，子类可以按需重写，返回继承自EntityFactory的实体工厂类
    /// </summary>
    /// <returns></returns>
    protected virtual EntityFactory GetFactory()
    {
        return new();
    }
}