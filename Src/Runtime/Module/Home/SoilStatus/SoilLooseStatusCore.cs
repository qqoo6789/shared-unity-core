using Newtonsoft.Json;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 土地松土状态
/// </summary>
public class SoilLooseStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Loose;

    public override eAction SupportAction => eAction.Sowing;

    protected override float AutoEnterNextStatusTime => TableUtil.GetGameValue(eGameValueID.soilFromLooseToIdleTime).Value;

    protected override void OnAutoEnterNextStatus()
    {
        base.OnAutoEnterNextStatus();

        ChangeState(eSoilStatus.Idle);
    }
    protected override void OnExecuteHomeAction(eAction action, object actionData)
    {
        base.OnExecuteHomeAction(action, actionData);

        try
        {
            (int seedCid, bool sowingValid) = ((int, bool))actionData;
            SoilData.SetSeedCid(seedCid, sowingValid);
            ChangeState(eSoilStatus.SeedThirsty);
        }
        catch (System.Exception e)
        {
            Log.Error($"播种失败 actionData:{JsonConvert.SerializeObject(actionData)} error:{e}");
        }
    }
}