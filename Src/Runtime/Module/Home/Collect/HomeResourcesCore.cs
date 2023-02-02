using UnityEngine;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 家园采集资源core
/// </summary>
public abstract class HomeResourcesCore : EntityBaseComponent, ICollectResourceCore
{
    public ulong Id { get; private set; }

    public eResourceType ResourceType => eResourceType.HomeResource;

    public GameObject LogicRoot => RefEntity.EntityRoot;

    public Vector3 Position => RefEntity.Position;

    public bool IsDead { get; private set; }

    public eAction SupportAction { get; set; } = eAction.None;

    protected virtual void Awake()
    {
        IsDead = false;
    }

    protected virtual void Start()
    {
        Id = (ulong)RefEntity.BaseData.Id;
        ResourceDataCore resourceData = GetComponent<ResourceDataCore>();
        DRHomeResources dr = resourceData.DRHomeResources;
        SupportAction = TableUtil.ToHomeAction(dr.HomeAction);

        if (HomeModuleCore.IsInited)//在家园里
        {
            HomeModuleCore.SoilResourceRelation.AddResourceOnSoil((long)Id, resourceData.SaveData.Id);
        }
    }

    protected virtual void OnDestroy()
    {
        if (HomeModuleCore.IsInited)//在家园里
        {
            HomeModuleCore.SoilResourceRelation.RemoveResourceOnSoil((long)Id);
        }
    }

    public bool CheckSupportAction(eAction action)
    {
        if (IsDead)
        {
            return false;
        }

        return (SupportAction & action) != 0;
    }

    public void ExecuteAction(eAction action, int toolCid, bool itemValid)
    {
        if (!CheckSupportAction(action))
        {
            Log.Error($"家园采集资源 action {action} not support,isDead:{IsDead}");
            return;
        }

        IsDead = true;
        OnDeath();
    }

    /// <summary>
    /// 当采集资源死亡时
    /// </summary>
    protected virtual void OnDeath()
    {
    }
}