using Newtonsoft.Json;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 土地播种已湿润状态
/// </summary>
public class SoilSeedWetStatusCore : SoilGrowingWetStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.SeedWet;

    protected override eAction SupportAction
    {
        get
        {
            eAction res = base.SupportAction;
            if (SoilData.SaveData.ManureCid <= 0)//如果没有施过肥可以施肥
            {
                res |= eAction.Manure;
            }
            return res;
        }
    }

    protected override void OnExecuteHomeAction(eAction action, int effectValue, object actionData)
    {
        base.OnExecuteHomeAction(action, effectValue, actionData);

        if (action == eAction.Manure)
        {
            try
            {
                (int manureCid, bool manureValid) = ((int, bool))actionData;
                SoilData.SetManure(manureCid, manureValid);
            }
            catch (System.Exception e)
            {
                Log.Error($"播种湿润时施肥失败 actionData:{JsonConvert.SerializeObject(actionData)} error:{e}");
            }
        }
    }
}