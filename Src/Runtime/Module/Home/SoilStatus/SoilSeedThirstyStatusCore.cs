using static HomeDefine;

/// <summary>
/// 土地已播种干涸状态
/// </summary>
public class SoilSeedThirstyStatusCore : SoilActionProgressStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.SeedThirsty;

    protected override eAction SupportAction => eAction.Watering | eAction.Hoeing;

    protected override float AutoEnterNextStatusTime => 0;

    protected override eAction NeedEffectValueActionType => eAction.Watering;

    protected override int NeedActionEffectValue => SOIL_NEED_WATERING_EFFECT_VALUE;

    protected override int LostActionEffectValueSpeed => SOIL_WATERING_EFFECT_VALUE_LOST_SPEED;

    protected override void OnActionComplete(eAction action, object actionData)
    {
        if (action == eAction.Watering)
        {
            ChangeState(eSoilStatus.Growing);
        }
        else if (action == eAction.Hoeing)
        {
            SoilData.SetSeedCid(0);
            ChangeState(eSoilStatus.Loose);
        }
    }
}