using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class EntityMgr<TEntity, TFactory> : MonoBehaviour where TEntity : EntityBase, new() where TFactory : EntityFactory<TEntity>, new()
{
    /// <summary>
    /// 场景所有实体 包括了主角
    /// </summary>
    /// <returns></returns>
    protected readonly Dictionary<long, TEntity> EntityDic = new();
    /// <summary>
    /// 场景所有实体 包括了主角,通过root节点id作为key
    /// </summary>
    /// <returns></returns>
    protected readonly Dictionary<int, TEntity> EntityRootDic = new();
    /// <summary>
    /// 实体工厂，用于创建实体
    /// </summary>
    protected TFactory Factory = new();

    /// <summary>
    /// 获取存在的场景实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns>如果没有会返回null</returns>
    public TEntity GetEntity(long id)
    {
        //EntityBase是个复杂对象的时候，可以用这个方法来获取效率会更高，有待观察 https://blog.csdn.net/sigmeta/article/details/121534293
        // if (EntityDic.ContainsKey(id))
        // {
        //     return EntityDic[id];
        // }

        if (EntityDic.TryGetValue(id, out TEntity entity))
        {
            return entity;
        }

        Log.Warning($"Can not find entity with id {id}");
        return null;
    }

    /// <summary>
    /// 通过实体显示节点反查逻辑实体
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public TEntity GetEntityWithRoot(GameObject go)
    {
        int goID = go.GetInstanceID();
        if (EntityRootDic.TryGetValue(goID, out TEntity entity))
        {
            return entity;
        }

        Log.Warning($"Can not find entity with root, name: {go.name}, id: {goID}");
        return null;
    }

    /// <summary>
    /// 添加一个场景实体 主角使用另外一个方法
    /// </summary>
    /// <param name="entityID"></param>
    /// <param name="entityType"></param>
    /// <returns></returns>
    public virtual TEntity AddEntity(long entityID, eEntityType entityType)
    {
        if (EntityDic.ContainsKey(entityID))
        {
            Log.Error($"Entity {entityID} already exist,type={entityType}");
            RemoveEntity(entityID);
        }

        TEntity entity = CreateEntity(entityID, entityType);
        try
        {
            entity.Init();
        }
        catch (Exception e)
        {
            Log.Error($"Entity {entityID} init failed,type={entityType},error={e}");
        }
        EntityDic.Add(entityID, entity);
        EntityRootDic.Add(entity.RootID, entity);
        return entity;
    }

    /// <summary>
    /// 移除一个场景实体 主角不使用这个方法
    /// </summary>
    /// <param name="entityID"></param>
    public void RemoveEntity(long entityID)
    {
        if (!EntityDic.TryGetValue(entityID, out TEntity entity))
        {
            Log.Error($"Entity {entityID} not exist");
            return;
        }

        _ = EntityDic.Remove(entityID);
        _ = EntityRootDic.Remove(entity.RootID);
        try
        {
            entity.Dispose();
        }
        catch (Exception e)
        {
            Log.Error($"Entity {entityID} dispose failed,error={e}");
        }
    }

    protected virtual TEntity CreateEntity(long entityID, eEntityType entityType)
    {
        return Factory.CreateSceneEntity(entityID, entityType);
    }
}