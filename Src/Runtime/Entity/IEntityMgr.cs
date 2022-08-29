public interface IEntityMgr
{
    T GetEntityWithRoot<T>(UnityEngine.GameObject go) where T : EntityBase;
}