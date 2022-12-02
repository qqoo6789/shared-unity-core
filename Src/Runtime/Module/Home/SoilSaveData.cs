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
    public long CurStatusStartStamp;
    /// <summary>
    /// 当前生成的阶段 从0开始 只对生长阶段有效
    /// </summary>
    public int CurGrowingStage;
    /// <summary>
    /// 当前种子配置ID 只有放了种子的状态才有效
    /// </summary>
    public int CurSeedCid;
}