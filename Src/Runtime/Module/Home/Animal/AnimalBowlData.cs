using UnityEngine;

/// <summary>
/// 畜牧食盆数据
/// </summary>
public class AnimalBowlData : MonoBehaviour
{
    /// <summary>
    /// 当前存放的食物配置 可能为null
    /// </summary>
    /// <value></value>
    public DRAnimalFood CurDRFood { get; private set; }
    /// <summary>
    /// 当前食盆保存的食物数据 如果没有食物则为null
    /// </summary>
    /// <value></value>
    public AnimalBowlSaveData SaveData { get; private set; }

    /// <summary>
    /// 设置食物的存档数据
    /// </summary>
    /// <param name="saveData"></param>
    internal void SetSaveData(AnimalBowlSaveData saveData)
    {
        SaveData = saveData ??= new AnimalBowlSaveData();
    }
}