using static HomeDefine;

/// <summary>
/// 土地正常生长状态
/// </summary>
public class SoilGrowingStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Growing;

    protected override eAction SupportAction => eAction.Hoeing;

    protected override float AutoEnterNextStatusTime => SoilData.SeedEveryGrowStageTime;

    protected override void OnAutoEnterNextStatus()
    {
        base.OnAutoEnterNextStatus();

        int growStage = SoilData.SaveData.GrowingStage;
        if (growStage >= SoilData.SeedGrowStageNum - 1)//成熟了
        {
            ChangeState(eSoilStatus.Harvest);
        }
        else
        {
            SoilData.SetGrowStage(growStage + 1);
            ChangeState(eSoilStatus.GrowingThirsty);
        }
    }

    protected override void OnExecuteHomeAction(eAction action, int effectValue, object actionData)
    {
        base.OnExecuteHomeAction(action, effectValue, actionData);

        SoilData.SetSeedCid(-1);
        ChangeState(eSoilStatus.Loose);
    }
}