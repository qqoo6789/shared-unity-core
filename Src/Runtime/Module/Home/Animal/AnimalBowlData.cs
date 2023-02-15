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
}