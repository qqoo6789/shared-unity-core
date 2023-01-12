using static HomeDefine;

/// <summary>
/// 土地植物干枯状态
/// </summary>
public class SoilWitheredStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Withered;

    protected override eAction SupportAction => eAction.Hoeing;

    protected override float AutoEnterNextStatusTime => 0;

    protected override void OnExecuteHomeAction(eAction action, int effectValue, object actionData)
    {
        base.OnExecuteHomeAction(action, effectValue, actionData);

        SoilData.SetSeedCid(0);
        ChangeState(eSoilStatus.Idle);
    }
}