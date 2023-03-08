using UnityEngine;
using static HomeDefine;

/// <summary>
/// 家园单块土地实体
/// </summary>
public abstract class HomeSoilCore : MonoBehaviour, ICollectResourceCore
{
    public ulong Id => SoilData.SaveData.Id;

    public eResourceType ResourceType => eResourceType.Soil;

    public Vector3 Position => transform.position;

    public GameObject LogicRoot => gameObject;


    public SoilEvent SoilEvent { get; set; }
    public SoilStatusCtrl StatusCtrl { get; private set; }
    public SoilData SoilData { get; private set; }

    public eAction SupportAction => GetCurStatus().SupportAction;

    public int Lv => throw new System.NotImplementedException();//目前业务没有需要读的 否则报错

    protected virtual void Awake()
    {
        SoilEvent = gameObject.AddComponent<SoilEvent>();
        StatusCtrl = gameObject.AddComponent<SoilStatusCtrl>();
        SoilData = gameObject.AddComponent<SoilData>();
        _ = gameObject.AddComponent<HomeActionProgressData>();

        InitStatus(StatusCtrl);
    }

    /// <summary>
    /// 子类初始化具体的状态 前后端不一样
    /// </summary>
    /// <param name="statusCtrl"></param>
    protected abstract void InitStatus(SoilStatusCtrl statusCtrl);

    /// <summary>
    /// 获取当前状态
    /// </summary>
    /// <returns></returns>
    protected SoilStatusCore GetCurStatus()
    {
        return StatusCtrl.Fsm.CurrentState as SoilStatusCore;
    }

    public bool CheckSupportAction(eAction action)
    {
        return GetCurStatus().CheckSupportAction(action);
    }

    public void ExecuteAction(eAction action, int toolCid, bool itemValid, int extraWateringNum)
    {
        if (action == eAction.Sowing)
        {
            SoilEvent.MsgExecuteAction?.Invoke(eAction.Sowing, (toolCid, itemValid));
        }
        else if (action == eAction.Manure)
        {
            SoilEvent.MsgExecuteAction?.Invoke(eAction.Manure, (toolCid, itemValid));
        }
        else
        {
            SoilEvent.MsgExecuteAction?.Invoke(action, null);
        }

        if (action == eAction.Watering && extraWateringNum > 0)
        {
            SoilData.SaveData.ExtraWateringNum = extraWateringNum;
        }
    }

    public void ExecuteProgress(eAction targetCurAction)
    {

    }
}