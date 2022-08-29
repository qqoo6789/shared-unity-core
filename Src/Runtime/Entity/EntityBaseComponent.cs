public class EntityBaseComponent : EntityComponentCore<EntityBase>
{
    protected override EntityBase RefEntity
    {
        get
        {
            if (RefEntityCache == null)
            {
                IEntityMgr mgr = GFEntryCore.GetModule<IEntityMgr>();
                RefEntityCache = mgr.GetEntityWithRoot<EntityBase>(gameObject);
            }
            return RefEntityCache;
        }
    }
}