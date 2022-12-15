using static HomeDefine;

/// <summary>
/// 土地成熟等待收获状态
/// </summary>
public class SoilHarvestStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Harvest;
}