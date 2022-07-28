/// <summary>
/// 判定实体能放技能接口
/// </summary>
public interface IEntityCanSkill
{
    /// <summary>
    /// 能放技能判定逻辑
    /// </summary>
    /// <returns></returns>
    bool CheckCanSkill();
}