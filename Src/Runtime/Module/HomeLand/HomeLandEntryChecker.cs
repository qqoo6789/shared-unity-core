using UnityEngine;
/// <summary>
/// 进出家园检测
/// </summary>
public class HomeLandEntryChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EntityBase entity = GFEntryCore.GetModule<IEntityMgr>().GetEntityWithRoot<EntityBase>(other.gameObject);
        if (entity != null && entity.Inited)
        {
            OnPlayerEnterHome(entity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EntityBase entity = GFEntryCore.GetModule<IEntityMgr>().GetEntityWithRoot<EntityBase>(other.gameObject);
        if (entity != null && entity.Inited)
        {
            OnPlayerExitHome(entity);
        }
    }

    private void OnPlayerEnterHome(EntityBase entity)
    {
        HomeLandModule.OnPlayerEnterHomeLand?.Invoke(entity);
    }

    private void OnPlayerExitHome(EntityBase entity)
    {
        HomeLandModule.OnPlayerExitHomeLand?.Invoke(entity);
    }
}