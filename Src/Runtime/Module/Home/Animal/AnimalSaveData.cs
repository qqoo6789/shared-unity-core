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
    public ulong AnimId;
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
    public bool isComforted;
    /// <summary>
    /// 动物已经死亡
    /// </summary>
    public bool isDead;
    /// <summary>
    /// 如果有自动生产的产品在场景中 这里就有数据
    /// </summary>
    public AnimalProductSaveData ProductSaveData;

    public AnimalSaveData()
    {

    }

    public AnimalSaveData(ulong animId)
    {
        AnimId = animId;
    }

    public AnimalSaveData(ProxyAnimalData data)
    {
        AnimId = data.AnimId;
        HungerProgress = data.HungerProgress;
        HarvestProgress = data.HarvestProgress;
        isComforted = data.IsComforted;
        isDead = data.IsDead;

        if (data.ProductData == null)
        {
            ProductSaveData = null;
        }
        else
        {
            ProductSaveData = new AnimalProductSaveData()
            {
                ProductId = data.ProductData.ProductId,
                ItemCid = data.ProductData.ItemCid,
                Pos = NetUtilCore.Vector3FromNet(data.ProductData.Position),
            };
        }
    }

    public ProxyAnimalData ToProxyAnimalData()
    {
        ProxyAnimalData data = new ProxyAnimalData()
        {
            AnimId = AnimId,
            HungerProgress = Mathf.CeilToInt(HungerProgress),
            HarvestProgress = Mathf.CeilToInt(HarvestProgress),
            IsComforted = isComforted,
            IsDead = isDead,
        };

        if (ProductSaveData == null)
        {
            data.ProductData = null;
        }
        else
        {
            data.ProductData = new()
            {
                ProductId = ProductSaveData.ProductId,
                ItemCid = ProductSaveData.ItemCid,
                Position = NetUtilCore.Vector3ToNet(ProductSaveData.Pos),
            };
        }

        return data;
    }
}