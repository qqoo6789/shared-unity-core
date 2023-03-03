using GameFramework.Fsm;

/// <summary>
/// 畜牧动物死亡状态
/// </summary>
public class AnimalDeathStatusCore : EntityStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name = "animalDeath";

    public override string StatusName => Name;

    protected HomeAnimalCore HomeAnimal;

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        HomeAnimal = StatusCtrl.RefEntity.GetComponent<HomeAnimalCore>();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        HomeAnimal = null;

        base.OnLeave(fsm, isShutdown);
    }

    public bool CheckCanMove()
    {
        return false;
    }
    public bool CheckCanSkill(int skillID)
    {
        return false;
    }
}