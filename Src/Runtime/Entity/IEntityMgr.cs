public interface IEntityMgr
{
    T GetEntityWithRoot<T>(UnityEngine.GameObject go) where T : EntityBase;
    T GetEntity<T>(long id) where T : EntityBase;
}