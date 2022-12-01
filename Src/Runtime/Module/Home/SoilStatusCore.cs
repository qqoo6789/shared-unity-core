/// <summary>
/// 土地上的各种状态的基类 实体状态类直接可以在内部任意时间切换状态 不要求再update 切换时取OwnerFsm
/// </summary>
public class SoilStatusCore : ComponentStatusCore<SoilStatusCtrl>
{
}