/// <summary>
/// 表中家园动作的属性分类 比如砍树 采矿
/// </summary>
public class TableHomeActionAttribute
{
    /// <summary>
    /// 收获双倍概率
    /// </summary>
    public eAttributeType ExtraHarvestRate;
    /// <summary>
    /// 可生效等级
    /// </summary>
    public eAttributeType AvailableLv;
}