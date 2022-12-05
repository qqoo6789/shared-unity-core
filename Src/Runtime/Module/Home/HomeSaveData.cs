using System;
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
    public string SoilDataList;

    public string HomeAreaDataList;

    public static string ToJson(object data)
    {
        return JsonConvert.SerializeObject(data, Formatting.Indented);
    }

    public static T FromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}