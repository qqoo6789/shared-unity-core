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
    public ResourceDataCore Data { get; private set; }

    public int Lv => Data.DRHomeResources.Lv;

    protected virtual void Awake()
    {
        IsDead = false;
    }

    protected virtual void Start()
    {
        if (HomeModuleCore.IsInited)//在家园里
        {
            if (Data != null)
            {
                HomeModuleCore.SoilResourceRelation.AddResourceOnSoil((long)Id, Data.SaveData.Id);
            }
            else
            {
                Log.Error($"家园采集资源 Data 组件 is null");
            }
        }
    }

    /// <summary>
    /// 初始化基础的ResourceData之后调用  用来第一次初始化基础的内容
    /// </summary>
    public void OnInitedResourceData()
    {
        Id = (ulong)RefEntity.BaseData.Id;
        Data = GetComponent<ResourceDataCore>();
        DRHomeResources dr = Data.DRHomeResources;
        SupportAction = TableUtil.ToHomeAction(dr.HomeAction);

        if ((PROGRESS_ACTION_MASK & SupportAction) != 0)
        {
            GetComponent<HomeActionProgressData>().StartProgressAction(SupportAction, dr.MaxActionValue);
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

    public virtual void ExecuteProgress(eAction targetCurAction, int skillId, int deltaProgress, bool isCrit, bool isPreEffect)
    {

    }

    /// <summary>
    /// 当采集资源死亡时
    /// </summary>
    protected virtual void OnDeath()
    {
    }
}