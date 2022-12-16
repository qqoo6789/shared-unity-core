using UnityEngine;

/// <summary>
/// 家园单块土地实体
/// </summary>
public abstract class HomeSoilCore : MonoBehaviour, ICollectResource
{
    public SoilEvent SoilEvent { get; set; }
    public SoilStatusCtrl StatusCtrl { get; private set; }
    public SoilData SoilData { get; private set; }

    public HomeDefine.eResourceType ResourceType => HomeDefine.eResourceType.Soil;

    public Vector3 Position => transform.position;

    public GameObject LogicRoot => gameObject;

    public bool CanCollect()
    {
        return SoilData.SaveData.SoilStatus == HomeDefine.eSoilStatus.Harvest;
    }

    public Vector3 GetDisplaySize()
    {
        return HomeDefine.SOIL_SIZE;
    }

    protected virtual void Awake()
    {
        SoilEvent = gameObject.AddComponent<SoilEvent>();
        StatusCtrl = gameObject.AddComponent<SoilStatusCtrl>();
        SoilData = gameObject.AddComponent<SoilData>();

        InitStatus(StatusCtrl);
    }

    /// <summary>
    /// 子类初始化具体的状态 前后端不一样
    /// </summary>
    /// <param name="statusCtrl"></param>
    protected abstract void InitStatus(SoilStatusCtrl statusCtrl);
}