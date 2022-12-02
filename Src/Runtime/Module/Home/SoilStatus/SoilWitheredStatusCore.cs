using static HomeDefine;

/// <summary>
/// 土地植物干枯状态
/// </summary>
public class SoilWitheredStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Withered;
}