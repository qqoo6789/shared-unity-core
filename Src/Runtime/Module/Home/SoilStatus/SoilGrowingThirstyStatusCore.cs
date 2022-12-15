using static HomeDefine;

/// <summary>
/// 土地种子发苗后的生长干涸状态
/// </summary>
public class SoilGrowingThirstyStatusCore : SoilActionProgressStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.GrowingThirsty;

    protected override eAction SupportAction => eAction.Hoeing | eAction.Watering;

    protected override float AutoEnterNextStatusTime => SoilData.DRSeed.WitherTime;

    protected override eAction NeedEffectValueActionType => eAction.Watering;

    protected override int NeedActionEffectValue => SOIL_NEED_WATERING_EFFECT_VALUE;

    protected override int LostActionEffectValueSpeed => SOIL_WATERING_EFFECT_VALUE_LOST_SPEED;

    protected override void OnAutoEnterNextStatus()
    {
        base.OnAutoEnterNextStatus();

        ChangeState(eSoilStatus.Withered);
    }

    protected override void OnActionComplete(eAction action, object actionData)
    {
        if (action == eAction.Hoeing)
        {
            SoilData.SetSeedCid(0);
            ChangeState(eSoilStatus.Idle);
        }
        else if (action == eAction.Watering)
        {
            ChangeState(eSoilStatus.Growing);
        }
    }
}