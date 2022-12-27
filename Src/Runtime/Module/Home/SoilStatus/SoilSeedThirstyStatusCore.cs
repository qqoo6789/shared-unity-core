using Newtonsoft.Json;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 土地已播种干涸状态
/// </summary>
public class SoilSeedThirstyStatusCore : SoilActionProgressStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.SeedThirsty;

    protected override eAction SupportAction
    {
        get
        {
            eAction res = eAction.Watering | eAction.Hoeing;
            if (SoilData.SaveData.ManureCid <= 0)//如果没有施过肥可以施肥
            {
                res |= eAction.Manure;
            }
            return res;
        }
    }

    protected override float AutoEnterNextStatusTime => 0;

    protected override eAction NeedEffectValueActionType => eAction.Watering;

    protected override int NeedActionEffectValue => SOIL_NEED_WATERING_EFFECT_VALUE;

    protected override int LostActionEffectValueSpeed => SOIL_WATERING_EFFECT_VALUE_LOST_SPEED;

    protected override void OnActionComplete(eAction action, object actionData)
    {
        if (action == eAction.Watering)
        {
            ChangeState(eSoilStatus.SeedWet);
        }
        else if (action == eAction.Hoeing)
        {
            SoilData.ClearAllData();
            ChangeState(eSoilStatus.Loose);
        }
        else if (action == eAction.Manure)
        {
            try
            {
                (int manureCid, bool manureValid) = ((int, bool))actionData;
                SoilData.SetManure(manureCid, manureValid);
            }
            catch (System.Exception e)
            {
                Log.Error($"播种干涸时施肥失败 actionData:{JsonConvert.SerializeObject(actionData)} error:{e}");
            }
        }
    }
}