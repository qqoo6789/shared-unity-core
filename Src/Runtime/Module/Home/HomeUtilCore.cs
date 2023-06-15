using System.Collections.Generic;
/// <summary>
/// 家园工具core
/// </summary>
public static class HomeUtilCore
{
    /// <summary>
    /// 计算土地的所有已使用肥沃值
    /// </summary>
    /// <param name="soils"></param>
    /// <returns></returns>
    public static int CalculateTotalFertilityUsed(IEnumerable<HomeSoilCore> soils)
    {
        int totalFertilityUsed = 0;
        foreach (HomeSoilCore soil in soils)
        {
            if (soil.SoilData.SaveData.Fertile > 0)
            {
                totalFertilityUsed += soil.SoilData.SaveData.Fertile;
            }
        }
        return totalFertilityUsed;
    }
}