/// <summary>
/// 打怪 boss 这类敌人伤害属性集合
/// </summary>
public class TableEnemyDamageAttribute : TableCoreDamageAttribute
{
    /// <summary>
    /// 防御力
    /// </summary>
    public eAttributeType Def;

    /// <summary>
    /// 伤害加深比例
    /// </summary>
    public eAttributeType Vulnerable;
}