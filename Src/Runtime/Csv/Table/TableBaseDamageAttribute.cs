/// <summary>
/// 配表中某个类型的基础伤害伤害属性分类 没有配置的属性会直接为Unknown 比如砍树 采矿 打怪
/// </summary>
public class TableBaseDamageAttribute
{
    /// <summary>
    /// 攻击力
    /// </summary>
    public eAttributeType Att;
    /// <summary>
    /// 防御力
    /// </summary>
    public eAttributeType Def;
    /// <summary>
    /// 伤害加成
    /// </summary>
    public eAttributeType DmgBonus;
    /// <summary>
    /// 暴击率
    /// </summary>
    public eAttributeType CritRate;
    /// <summary>
    /// 暴击伤害
    /// </summary>
    public eAttributeType CritDmg;
}