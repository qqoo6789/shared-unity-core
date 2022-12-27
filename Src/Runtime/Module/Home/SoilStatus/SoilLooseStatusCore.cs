using Newtonsoft.Json;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 土地松土状态
/// </summary>
public class SoilLooseStatusCore : SoilStatusCore
{
    public override eSoilStatus StatusFlag => eSoilStatus.Loose;

    protected override eAction SupportAction => eAction.Sowing;

    protected override float AutoEnterNextStatusTime => HomeDefine.SOIL_FROM_LOOSE_TO_IDLE_TIME;

    protected override void OnAutoEnterNextStatus()
    {
        base.OnAutoEnterNextStatus();

        ChangeState(eSoilStatus.Idle);
    }
    protected override void OnExecuteHomeAction(eAction action, int effectValue, object actionData)
    {
        base.OnExecuteHomeAction(action, effectValue, actionData);

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