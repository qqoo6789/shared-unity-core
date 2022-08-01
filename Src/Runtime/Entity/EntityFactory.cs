/// <summary>
/// 实体工厂
/// </summary>
public class EntityFactory<TEntity> where TEntity : EntityBase, new()
{
    public TEntity CreateSceneEntity(long id, MelandGame3.EntityType type)
    {
        TEntity entity = new();
        entity.InitBaseInfo(id, type);
        return AssemblyEntity(entity);//返回装备后的实体
    }

    /// <summary>
    /// 根据实体类型装配出不同feature的实体
    /// </summary>
    /// <param name="entity"></param>
    protected virtual TEntity AssemblyEntity(TEntity entity)
    {
        //Example:
        // if (entity.BaseData.Type == eEntityType.monster)
        // {
        //     entity.AddComponent<MonsterLogic>();
        //     entity.AddComponent<OtherLogic>();
        // }
        return entity;
    }
}