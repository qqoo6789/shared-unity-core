using static HomeDefine;

/// <summary>
/// 土地生长湿润状态
/// </summary>
public class SoilGrowingWetStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.GrowingWet;

    protected override eAction SupportAction => eAction.Eradicate;

    protected override float AutoEnterNextStatusTime => SoilData.SeedEveryGrowStageTime;

    protected override void OnAutoEnterNextStatus()
    {
        base.OnAutoEnterNextStatus();

        int growStage = SoilData.SaveData.GrowingStage;
        if (growStage >= SoilData.SeedGrowStageNum - 1)//成熟了
        {
            ChangeState(SoilData.SaveData.SowingValid ? eSoilStatus.Harvest : eSoilStatus.RotHarvest);
        }
        else
        {
            SoilData.SetGrowStage(growStage + 1);
            ChangeState(eSoilStatus.GrowingThirsty);
        }
    }

    protected override void OnExecuteHomeAction(eAction action, object actionData)
    {
        base.OnExecuteHomeAction(action, actionData);

        if (action == eAction.Eradicate)
        {
            SoilData.ClearAllData();
            ChangeState(eSoilStatus.Loose);
        }
    }
}