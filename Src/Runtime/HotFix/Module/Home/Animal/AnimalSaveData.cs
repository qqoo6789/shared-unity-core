using GameMessageCore;
using UnityEngine;

/// <summary>
/// 动物存档的细节数据 不包含main服的动物管理数据
/// </summary>
[System.Serializable]
public class AnimalSaveData
{
    /// <summary>
    /// 动物在家园系统中的全局ID和数据服那边的ID一致 和实体的ID没有直接关系
    /// </summary>
    public ulong AnimalId;
    /// <summary>
    /// 饥饿进度 0代表完全饿了
    /// </summary>
    public float HungerProgress = 0;
    /// <summary>
    /// 上次完全饥饿的时间戳 >0说明正在饥饿中
    /// </summary>
    public long LastCompleteHungerStamp;
    /// <summary>
    /// 收获进度 0~100 代表进度百分比值 成长进度 进度满了后就可以收获了
    /// </summary>
    public float HarvestProgress = 0;
    /// <summary>
    /// 本收获阶段是否已经安抚过
    /// </summary>
    public bool IsComforted;
    /// <summary>
    /// 动物已经死亡
    /// </summary>
    public bool IsDead;
    /// <summary>
    /// 如果有自动生产的产品在场景中 这里就有数据
    /// </summary>
    public AnimalProductSaveData ProductSaveData;

    public AnimalSaveData()
    {

    }

    public AnimalSaveData(ulong animalId)
    {
        AnimalId = animalId;
    }

    public AnimalSaveData(ProxyAnimalData data)
    {
        AnimalId = data.AnimalId;
        HungerProgress = data.HungerProgress;
        HarvestProgress = data.HarvestProgress;
        IsComforted = data.IsComforted;
        IsDead = data.IsDead;

        ProductSaveData = data.ProductData == null ? null : new AnimalProductSaveData(data.ProductData);
    }

    public ProxyAnimalData ToProxyAnimalData()
    {
        ProxyAnimalData data = new()
        {
            AnimalId = AnimalId,
            HungerProgress = Mathf.CeilToInt(HungerProgress),
            HarvestProgress = Mathf.CeilToInt(HarvestProgress),
            IsComforted = IsComforted,
            IsDead = IsDead,

            ProductData = ProductSaveData?.ToProxyAnimalData()
        };

        return data;
    }

    /// <summary>
    /// 统一设置饥饿进度 0~100
    /// </summary>
    public void SetHarvestProgress(float progress)
    {
        HarvestProgress = Mathf.Clamp(progress, 0, HomeDefine.ANIMAL_CAN_HARVEST_PROCESS);
    }
}