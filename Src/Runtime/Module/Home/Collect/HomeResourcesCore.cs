using UnityEngine;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 家园采集资源core
/// </summary>
public abstract class HomeResourcesCore : EntityBaseComponent, ICollectResourceCore
{
    public ulong Id => (ulong)RefEntity.BaseData.Id;

    public eResourceType ResourceType => eResourceType.HomeResource;

    public GameObject LogicRoot => RefEntity.EntityRoot;

    public Vector3 Position => RefEntity.Position;

    public bool IsDead { get; private set; }
    private eAction _supportAction = eAction.None;

    private void Awake()
    {
        IsDead = false;
    }

    private void Start()
    {
        DRHomeResources dr = GetComponent<ResourceDataCore>().DRHomeResources;
        _supportAction = TableUtil.ToHomeAction(dr.HomeAction);
    }

    public bool CheckSupportAction(eAction action)
    {
        if (IsDead)
        {
            return false;
        }

        return (_supportAction & action) != 0;
    }

    public void ExecuteAction(eAction action, int toolCid, bool itemValid)
    {
        if (action != eAction.Harvest)
        {
            Log.Error($"家园采集资源 action {action} not support");
            return;
        }

        IsDead = true;
        OnDeath();
    }

    /// <summary>
    /// 当采集资源死亡时
    /// </summary>
    protected abstract void OnDeath();
}