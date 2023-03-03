using GameFramework.Fsm;
/// <summary>
/// 实体上的各种状态的基类 实体状态类直接可以在内部任意时间切换状态 不要求再update 切换时取OwnerFsm
/// </summary>
public class EntityStatusCore : ComponentStatusCore<EntityStatusCtrl>
{
    private HomeAnimalCore _homeAnimalCore;//有这个组件说明是畜牧动物

    protected override void OnInit(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnInit(fsm);

        _ = StatusCtrl.TryGetComponent(out _homeAnimalCore);
    }

    protected virtual bool RefEntityIsDead()
    {
        if (_homeAnimalCore != null)
        {
            return _homeAnimalCore.Data.SaveData.IsDead;
        }

        return StatusCtrl.RefEntity.BattleDataCore != null && !StatusCtrl.RefEntity.BattleDataCore.IsLive();
    }

    protected virtual string GetDeathStatusName()
    {
        if (_homeAnimalCore != null)
        {
            return AnimalDeathStatusCore.Name;
        }

        return DeathStatusCore.Name;
    }
}