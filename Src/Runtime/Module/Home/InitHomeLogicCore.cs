using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 初始化家园逻辑core 相当于启动逻辑共享部分
/// </summary>
public class InitHomeLogicCore : MonoBehaviour
{

    /// <summary>
    /// 从家园保存数据中恢复土地状态
    /// </summary>
    /// <param name="saveData"></param>
    public void RestoreSoilStatus(HomeSaveData saveData)
    {
        SoilSaveData[] soilSaveDataList = HomeSaveData.FromJson<SoilSaveData[]>(saveData.SoilDataList);
        foreach (SoilSaveData data in soilSaveDataList)
        {
            ulong id = data.Id;
            HomeSoilCore soil = HomeModuleCore.SoilMgr.GetSoil(id);
            if (soil == null)
            {
                Log.Error($"not found soil form db, id:{id}");
                continue;
            }

            soil.SoilEvent.MsgInitStatus?.Invoke(data);
        }
    }
}