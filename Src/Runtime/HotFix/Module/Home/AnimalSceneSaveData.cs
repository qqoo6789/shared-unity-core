/// <summary>
/// 畜牧场景存档数据
/// </summary>
[System.Serializable]
public class AnimalSceneSaveData
{
    public AnimalBowlSaveData[] BowlSaveDataList;
    /// <summary>
    /// 上次保存的时间戳
    /// </summary>
    public long LastSaveStamp;
}