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
    public ulong AnimalId => _saveData.AnimalId;
    /// <summary>
    /// 配置表
    /// </summary>
    /// <value></value>
    public DRMonster DRMonster { get; private set; }
    [SerializeField]
    private AnimalBaseData _baseData;
    /// <summary>
    /// 动物基础数据 对应动物管理列表中的数据
    /// </summary>
    /// <value></value>
    public AnimalBaseData BaseData => _baseData;
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

    public void SetBaseData(AnimalBaseData animalBaseData)
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
            if (animalSaveData.ProductSaveData != null && animalSaveData.ProductSaveData.ProductId == 0)
            {
                Log.Error($"动物存档数据中的产品数据有误 id==0 Product:{animalSaveData.ProductSaveData}");
                animalSaveData.ProductSaveData = null;
            }

            _saveData = animalSaveData;
            if (_baseData.AnimalId != _saveData.AnimalId)
            {
                Log.Error($"动物数据和存档数据不一致 _baseData.AnimalId:{_baseData.AnimalId} _saveData.AnimalId:{_saveData.AnimalId}");
                _saveData.AnimalId = _baseData.AnimalId;
            }
        }
        else
        {
            _saveData = new AnimalSaveData(_baseData.AnimalId)
            {
                HungerProgress = DRMonster.MaxHunger
            };
        }
    }
}