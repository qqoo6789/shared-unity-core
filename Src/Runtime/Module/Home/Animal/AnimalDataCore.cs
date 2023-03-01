using System;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 动物数据
/// </summary>
public class AnimalDataCore : MonoBehaviour
{
    /// <summary>
    /// 动物Id 家园系统中的ID和数据管理Id一致
    /// </summary>
    public ulong AnimId => _saveData.AnimId;
    /// <summary>
    /// 配置表
    /// </summary>
    /// <value></value>
    public DRMonster DRMonster { get; private set; }
    [SerializeField]
    private AnimBaseData _baseData;
    /// <summary>
    /// 动物基础数据 对应动物管理列表中的数据
    /// </summary>
    /// <value></value>
    public AnimBaseData BaseData => _baseData;
    [SerializeField]
    private AnimalSaveData _saveData;
    /// <summary>
    /// 动物存档数据
    /// </summary>
    /// <value></value>
    public AnimalSaveData SaveData => _saveData;

    /// <summary>
    /// 是否能收获
    /// </summary>
    public bool IsCanHarvest => _saveData.HarvestProgress >= 100;

    /// <summary>
    /// 好感度改变事件 T0:更改后好感度
    /// </summary>
    public Action<int> MsgFavorabilityChanged;

    public void SetBaseData(AnimBaseData animalBaseData)
    {
        _baseData = animalBaseData;
        DRMonster = GFEntryCore.DataTable.GetDataTable<DRMonster>().GetDataRow(_baseData.Cid);
        if (DRMonster == null)
        {
            throw new Exception($"动物配置表中没有找到cid为{_baseData.Cid}的数据");
        }
    }

    public void SetSaveData(AnimalSaveData animalSaveData)
    {
        if (animalSaveData != null)
        {
            _saveData = animalSaveData;
            if (_baseData.AnimId != _saveData.AnimId)
            {
                Log.Error($"动物数据和存档数据不一致 _baseData.AnimId:{_baseData.AnimId} _saveData.AnimId:{_saveData.AnimId}");
                _saveData.AnimId = _baseData.AnimId;
            }
        }
        else
        {
            _saveData = new AnimalSaveData(_baseData.AnimId)
            {
                HungerProgress = DRMonster.MaxHunger
            };
        }
    }
}