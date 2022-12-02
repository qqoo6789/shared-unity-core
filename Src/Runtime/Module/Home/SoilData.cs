using UnityEngine;

/// <summary>
/// 单块土地上的数据
/// </summary>
public class SoilData : MonoBehaviour
{
    [SerializeField]
    private SoilSaveData _saveData;
    /// <summary>
    /// 能存档的数据
    /// </summary>
    /// <value></value>
    public SoilSaveData SaveData => _saveData;

    private void Awake()
    {
        _saveData = new SoilSaveData();
    }

    internal void SetId(ulong id)
    {
        SaveData.Id = id;
    }

    /// <summary>
    /// 整个保存数据直接设置 往往是初始化的时候
    /// </summary>
    /// <param name="saveData"></param>
    internal void SetSaveData(SoilSaveData saveData)
    {
        _saveData = saveData;
    }
}