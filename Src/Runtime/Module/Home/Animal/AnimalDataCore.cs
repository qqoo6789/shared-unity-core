using UnityEngine;

/// <summary>
/// 动物数据
/// </summary>
public class AnimalDataCore : MonoBehaviour
{
    /// <summary>
    /// 动物Id 家园系统中的ID和数据管理Id一致
    /// </summary>
    public ulong AnimId => SaveData.AnimId;
    /// <summary>
    /// 配置表
    /// </summary>
    /// <value></value>
    public DRMonster DRMonster { get; private set; }
    /// <summary>
    /// 动物存档数据
    /// </summary>
    /// <value></value>
    public AnimalSaveData SaveData { get; private set; }
}