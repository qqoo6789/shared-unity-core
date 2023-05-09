using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// 整个家园的存储数据 可能是数据库来的 也可能是给客户端协议里解来的
/// </summary>
[Serializable]
public class HomeSaveData
{
    /// <summary>
    /// 所有非空白idle状态的土地列表 空白的不存储
    /// </summary>
    public List<SoilSaveData> SoilSaveDataList;

    public List<HomeResourcesAreaSaveData> HomeAreaSaveDataList;
    public List<AnimalSaveData> AnimalSaveDataList;
    public AnimalSceneSaveData AnimalSceneSaveData;
    /// <summary>
    /// 上次保存的时间戳
    /// </summary>
    public long LastSaveStamp;

    // 是否使用初始家园数据
    public bool UseDefaultData;

    public static string ToJson(object data)
    {
        return JsonConvert.SerializeObject(data, Formatting.None);
    }

    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}