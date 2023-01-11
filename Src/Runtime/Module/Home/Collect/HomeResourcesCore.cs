using UnityEngine;
using static HomeDefine;

/// <summary>
/// 家园采集资源core
/// </summary>
public class HomeResourcesCore : EntityBaseComponent, ICollectResourceCore
{
    public ulong Id => (ulong)RefEntity.BaseData.Id;

    public eResourceType ResourceType => eResourceType.HomeResource;

    public GameObject LogicRoot => RefEntity.EntityRoot;

    public Vector3 Position => RefEntity.Position;

    public bool CheckSupportAction(eAction action)
    {
        return (eAction.Harvest & action) != 0;
    }

    public void ExecuteAction(eAction action, int toolCid, bool itemValid)
    {
        GFEntryCore.GetModule<IEntityMgr>().RemoveEntity(RefEntity.BaseData.Id);
    }
}