/// <summary>
/// 畜牧工具
/// </summary>
public class AnimalUtilCore
{
    /// <summary>
    /// 计算动物的好感度对应的心的数量
    /// </summary>
    /// <param name="FavorabilityValue"></param>
    /// <returns></returns>
    public int CalculateAnimalFavorabilityHeartNum(int FavorabilityValue)
    {
        return FavorabilityValue / HomeDefine.ANIMAL_FAVORABILITY_ONE_HEART_NUM;
    }
}