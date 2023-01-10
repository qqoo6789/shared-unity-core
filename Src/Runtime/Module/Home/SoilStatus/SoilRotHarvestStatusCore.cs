using static HomeDefine;

/// <summary>
/// 土地腐败成熟收获状态
/// </summary>
public class SoilRotHarvestStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.RotHarvest;

    protected override eAction SupportAction => eAction.Harvest;

    protected override float AutoEnterNextStatusTime => 0;

    protected override void OnExecuteHomeAction(eAction action, object actionData)
    {
        base.OnExecuteHomeAction(action, actionData);

        SoilData.ClearAllData();
        ChangeState(eSoilStatus.Loose);
    }
}