using static HomeDefine;

/// <summary>
/// 土地种子发苗后的生长干涸状态
/// </summary>
public class SoilGrowingThirstyStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.GrowingThirsty;

    protected override eAction SupportAction => eAction.Hoeing | eAction.Watering;

    protected override float AutoEnterNextStatusTime => SoilData.DRSeed.WitherTime;

    protected override void OnAutoEnterNextStatus()
    {
        base.OnAutoEnterNextStatus();

        ChangeState(eSoilStatus.Withered);
    }

    protected override void OnExecuteHomeAction(eAction action, object actionData)
    {
        base.OnExecuteHomeAction(action, actionData);

        if (action == eAction.Hoeing)
        {
            SoilData.ClearAllData();
            ChangeState(eSoilStatus.Loose);
        }
        else if (action == eAction.Watering)
        {
            ChangeState(eSoilStatus.GrowingWet);
        }
    }
}