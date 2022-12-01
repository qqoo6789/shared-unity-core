using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 初始化家园逻辑core 相当于启动逻辑共享部分
/// </summary>
public class InitHomeLogicCore : MonoBehaviour
{
    private void Start()
    {
        InitSoil();
    }

    //初始化所有土地
    private void InitSoil()
    {
        GenerateSoil();
    }

    //生成具体土地
    private void GenerateSoil()
    {
        Vector3 startPos = Vector3.zero;
        for (int i = 0; i < HomeDefine.SOIL_X_NUM; i++)
        {
            for (int j = 0; j < HomeDefine.SOIL_Z_NUM; j++)
            {
                Vector3 pos = startPos + new Vector3(i * HomeDefine.SOIL_SIZE.x, 0, j * HomeDefine.SOIL_SIZE.z);
                ulong id = MathUtilCore.TwoIntToUlong(i, j);
                HomeModuleCore.SoilMgr.AddSoil(id, pos);
            }
        }
    }

    /// <summary>
    /// 从家园保存数据中恢复土地状态
    /// </summary>
    /// <param name="saveData"></param>
    public void RestoreSoilStatus(HomeSaveData saveData)
    {

        foreach (SoilSaveData data in saveData.SoilDataList)
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