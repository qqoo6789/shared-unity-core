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
    internal void SetSeedCid(int seedCid)
    {
        if (SaveData.SeedCid == seedCid)
        {
            return;
        }

        SaveData.SeedCid = seedCid;

        if (seedCid <= 0)
        {
            SetGrowStage(-1);
            DRSeed = null;
        }
        else
        {
            DRSeed = GFEntryCore.DataTable.GetDataTable<DRSeed>().GetDataRow(seedCid);
            if (DRSeed == null)
            {
                Log.Error($"种子配置表里没有找到cid为 {seedCid} 的种子");
            }
            SetGrowStage(0);
        }
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
}