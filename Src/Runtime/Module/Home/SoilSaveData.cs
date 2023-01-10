using System;
using static HomeDefine;

/// <summary>
/// 单个土地的存储数据 可能是数据库的  也可能是服务器转给客户端的
/// </summary>
[Serializable]
public class SoilSaveData
{
    /// <summary>
    /// 土地唯一ID
    /// </summary>
    public ulong Id;

    /// <summary>
    /// 土地状态
    /// </summary>
    public eSoilStatus SoilStatus;
    /// <summary>
    /// 开始当前状态的时间
    /// </summary>
    public long StatusStartStamp;
    /// <summary>
    /// 当前生成的阶段 从0开始 只对生长阶段有效 不要在这里直接设置 需要走SoilData.SetGrowStage
    /// </summary>
    public int GrowingStage = -1;
    /// <summary>
    /// 当前种子配置ID 只有放了种子的状态才有效 不要在这里直接设置 需要走SoilData.SetSeedCid
    /// </summary>
    public int SeedCid;
    /// <summary>
    /// 播种是否有效
    /// </summary>
    public bool SowingValid;
    /// <summary>
    /// 种植了多格种子时 种子归属的根土地ID 0代表不是多格种子 多格种子根在左下角格子里
    /// </summary>
    public ulong MultipleGridsRootSoilID;
    /// <summary>
    /// 施的肥料配置ID
    /// </summary>
    public int ManureCid;
    /// <summary>
    /// 施肥是否有效
    /// </summary>
    public bool ManureValid;
}