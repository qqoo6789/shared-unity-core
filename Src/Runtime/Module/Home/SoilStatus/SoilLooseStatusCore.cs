using static HomeDefine;

/// <summary>
/// 土地松土状态
/// </summary>
public class SoilLooseStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Loose;

    protected override eAction SupportAction => eAction.Sowing;

    protected override float AutoEnterNextStatusTime => 3 * 24 * 60 * 60;

    protected override void OnAutoEnterNextStatus()
    {
        base.OnAutoEnterNextStatus();

        ChangeState(eSoilStatus.Idle);
    }
    protected override void OnExecuteHomeAction(eAction action, int effectValue, object actionData)
    {
        base.OnExecuteHomeAction(action, effectValue, actionData);

        int seedCid = (int)actionData;
        SoilData.SetSeedCid(seedCid);
        ChangeState(eSoilStatus.SeedThirsty);
    }
}