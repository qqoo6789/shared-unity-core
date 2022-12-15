using static HomeDefine;

/// <summary>
/// 土地饥渴状态
/// </summary>
public class SoilThirstyStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Thirsty;
}