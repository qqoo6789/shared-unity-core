using static HomeDefine;

/// <summary>
/// 土地成熟等待收获状态
/// </summary>
public class SoilHarvestStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Harvest;

    protected override eAction SupportAction => eAction.Harvest;

    protected override float AutoEnterNextStatusTime => 0;

    protected override void OnExecuteHomeAction(eAction action, int effectValue, object actionData)
    {
        base.OnExecuteHomeAction(action, effectValue, actionData);

        SoilData.SetSeedCid(0);
        ChangeState(eSoilStatus.Loose);
    }
}