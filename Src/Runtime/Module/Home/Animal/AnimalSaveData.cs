/// <summary>
/// 动物存档的细节数据 不包含main服的动物管理数据
/// </summary>
public class AnimalSaveData
{
    /// <summary>
    /// 动物在家园系统中的全局ID和数据服那边的ID一致 和实体的ID没有直接关系
    /// </summary>
    public ulong AnimId;
    /// <summary>
    /// 饥饿进度 0代表完全饿了
    /// </summary>
    public float HungerProgress;
    /// <summary>
    /// 收获进度 成长进度 进度满了后就可以收获了
    /// </summary>
    public float HarvestProgress;
    /// <summary>
    /// 本收获阶段是否已经安抚过
    /// </summary>
    public bool isComforted;
    /// <summary>
    /// 动物已经死亡
    /// </summary>
    public bool isDead;
    /// <summary>
    /// 如果有自动生产的产品在场景中 这里就有数据
    /// </summary>
    public AnimalProductSaveData ProductSaveData;
}