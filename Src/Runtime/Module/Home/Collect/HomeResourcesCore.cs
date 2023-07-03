using System;
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

    public int Lv
    {
        get
        {
            ResourceDataCore resourceData = GetComponent<ResourceDataCore>();
            return resourceData.DRHomeResources.Lv;
        }
    }

    protected virtual void Awake()
    {
        IsDead = false;
    }

    /// <summary>
    /// 初始化基础的ResourceData之后调用  用来第一次初始化基础的内容
    /// </summary>
    public void OnInitedResourceData()
    {
        Id = (ulong)RefEntity.BaseData.Id;
        ResourceDataCore resourceData = GetComponent<ResourceDataCore>();
        DRHomeResources dr = resourceData.DRHomeResources;
        SupportAction = TableUtil.ToHomeAction(dr.HomeAction);

        if ((PROGRESS_ACTION_MASK & SupportAction) != 0)
        {
            GetComponent<HomeActionProgressData>().StartProgressAction(SupportAction, dr.MaxActionValue);
        }
    }

    protected virtual void Start()
    {
        if (HomeModuleCore.IsInited)//在家园里
        {
            ResourceDataCore resourceData = GetComponent<ResourceDataCore>();
            HomeModuleCore.SoilResourceRelation.AddResourceOnSoil((long)Id, resourceData.SaveData.Id);
        }
    }

    protected virtual void OnDestroy()
    {
        if (HomeModuleCore.IsInited)//在家园里
        {
            HomeModuleCore.SoilResourceRelation.RemoveResourceOnSoil((long)Id);
        }

        GetComponent<HomeActionProgressData>().EndProgressAction();
    }

    public bool CheckSupportAction(eAction action)
    {
        if (IsDead)
        {
            return false;
        }

        return (SupportAction & action) != 0;
    }

    public void ExecuteAction(eAction action, int toolCid, int skillId, object actionData)
    {
        if (!CheckSupportAction(action))
        {
            Log.Error($"家园采集资源 action {action} not support,isDead:{IsDead}");
            return;
        }

        OnExecuteAction(action, skillId);

        IsDead = true;
        OnDeath();
    }

    protected virtual void OnExecuteAction(eAction action, int skillId)
    {
    }

    public virtual void ExecuteProgress(eAction targetCurAction, int skillId, int deltaProgress, bool isCrit)
    {

    }

    /// <summary>
    /// 当采集资源死亡时
    /// </summary>
    protected virtual void OnDeath()
    {
    }
}