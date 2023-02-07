using static HomeDefine;
using GameFramework.Fsm;

/// <summary>
/// 土地种子发苗后的生长干涸状态
/// </summary>
public class SoilGrowingThirstyStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.GrowingThirsty;

    public override eAction SupportAction => eAction.Eradicate | eAction.Watering;

    protected override float AutoEnterNextStatusTime => SoilData.DRSeed.WitherTime;

    protected override void OnEnter(IFsm<SoilStatusCtrl> fsm)
    {
        base.OnEnter(fsm);

        StatusCtrl.GetComponent<HomeActionProgressData>().StartProgressAction(eAction.Watering, SOIL_PROGRESS_ACTION_MAX_VALUE);
    }

    protected override void OnLeave(IFsm<SoilStatusCtrl> fsm, bool isShutdown)
    {
        StatusCtrl.GetComponent<HomeActionProgressData>().EndProgressAction();

        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnAutoEnterNextStatus()
    {
        base.OnAutoEnterNextStatus();

        ChangeState(eSoilStatus.Withered);
    }

    protected override void OnExecuteHomeAction(eAction action, object actionData)
    {
        base.OnExecuteHomeAction(action, actionData);

        if (action == eAction.Eradicate)
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