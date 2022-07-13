/// <summary>
/// 实体工厂
/// </summary>
public class EntityFactory
{
    public T CreateSceneEntity<T>(long id, eEntityType type) where T : EntityBase, new()
    {
        T entity = new();
        entity.InitBaseInfo(id, type);
        return AssemblyEntity(entity);//返回装备后的实体
    }

    /// <summary>
    /// 根据实体类型装配出不同feature的实体
    /// </summary>
    /// <param name="entity"></param>
    protected virtual T AssemblyEntity<T>(T entity) where T : EntityBase
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