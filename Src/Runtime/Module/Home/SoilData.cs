using System;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 单块土地上的数据
/// </summary>
public class SoilData : MonoBehaviour
{
    [SerializeField]
    private SoilSaveData _saveData;
    /// <summary>
    /// 能存档的数据
    /// </summary>
    /// <value></value>
    public SoilSaveData SaveData => _saveData;
    /// <summary>
    /// 当前土地的种子配置 可能为空 只有真正有效才有值
    /// </summary>
    /// <value></value>
    public DRSeed DRSeed { get; private set; }

    /// <summary>
    /// 种子生长阶段数量 无效时为0
    /// </summary>
    public int SeedGrowStageNum => DRSeed == null ? 0 : DRSeed.GrowRes.Length;
    /// <summary>
    /// 种子每个生长阶段的时间 无效时为0
    /// </summary>
    public float SeedEveryGrowStageTime => SeedGrowStageNum == 0 ? 0 : DRSeed.GrowTotalTime / SeedGrowStageNum;

    private void Awake()
    {
        _saveData = new SoilSaveData();
    }

    internal void SetId(ulong id)
    {
        SaveData.Id = id;
    }

    /// <summary>
    /// 整个保存数据直接设置 往往是初始化的时候
    /// </summary>
    /// <param name="saveData"></param>
    internal void SetSaveData(SoilSaveData saveData)
    {
        _saveData = saveData;

        if (saveData.SeedCid > 0)
        {
            DRSeed = GFEntryCore.DataTable.GetDataTable<DRSeed>().GetDataRow(saveData.SeedCid);
            if (DRSeed == null)
            {
                Log.Error($"初始化土地数据时种子配置表里没有找到cid为 {saveData.SeedCid} 的种子");
            }
        }
    }

    /// <summary>
    /// 设置当前种子配置id 如果是要清除种子 则传入0
    /// </summary>
    /// <param name="seedCid">0代表清除</param>
    /// <param name="sowingValid">是否播种有效</param>
    internal void SetSeedCid(int seedCid, bool sowingValid)
    {
        if (SaveData.SeedCid == seedCid)
        {
            return;
        }

        SaveData.SeedCid = seedCid;

        DRSeed = GFEntryCore.DataTable.GetDataTable<DRSeed>().GetDataRow(seedCid);
        if (DRSeed == null)
        {
            Log.Error($"种子配置表里没有找到cid为 {seedCid} 的种子");
        }
        SetGrowStage(0);
        SaveData.SowingValid = sowingValid;
    }

    /// <summary>
    /// 清理所有数据到默认值
    /// </summary>
    internal void ClearAllData()
    {
        DRSeed = null;
        _saveData = new SoilSaveData();//防止数据没清干净
    }

    /// <summary>
    /// 设置当前种子的成长阶段 从0开始 最大不能超过配置的数量索引
    /// </summary>
    /// <param name="growStage"></param>
    internal void SetGrowStage(int growStage)
    {
        if (SaveData.GrowingStage == growStage)
        {
            return;
        }

        if (growStage >= SeedGrowStageNum)
        {
            Log.Error($"土地的成长阶段设置错误 :{growStage} cfgNum:{SeedGrowStageNum}");
            return;
        }

        SaveData.GrowingStage = growStage;
    }

    /// <summary>
    /// 设置施肥
    /// </summary>
    /// <param name="manureCid">肥料cid</param>
    /// <param name="isValid">是否施肥有效</param>
    internal void SetManure(int manureCid, bool isValid)
    {
        if (SaveData.ManureCid > 0)
        {
            Log.Error($"土地已经施肥了 不能再施肥了");
            return;
        }

        SaveData.ManureCid = manureCid;
        SaveData.ManureValid = isValid;
    }
}