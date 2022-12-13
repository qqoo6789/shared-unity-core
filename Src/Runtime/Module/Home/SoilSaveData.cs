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
    /// 当前生成的阶段 从0开始 只对生长阶段有效
    /// </summary>
    public int GrowingStage;
    /// <summary>
    /// 当前种子配置ID 只有放了种子的状态才有效
    /// </summary>
    public int SeedCid;
    /// <summary>
    /// 种植了多格种子时 种子归属的根土地ID 0代表不是多格种子 多格种子根在左下角格子里
    /// </summary>
    public ulong MultipleGridsRootSoilID;
    /// <summary>
    /// 最后一次执行动作到达的效果值 只对有动作效果进度的状态有效
    /// </summary>
    public int LastActionEffectValue;
    /// <summary>
    /// 最后一次执行动作的时间 只对有动作效果进度的状态有效
    /// </summary>
    public long LastActionStamp;
}