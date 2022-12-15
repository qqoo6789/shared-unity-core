using static HomeDefine;

/// <summary>
/// 土地正常生长状态
/// </summary>
public class SoilGrowingStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Growing;
}