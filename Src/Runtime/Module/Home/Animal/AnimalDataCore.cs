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
    public ulong AnimId => SaveData.AnimId;
    /// <summary>
    /// 配置表
    /// </summary>
    /// <value></value>
    public DRMonster DRMonster { get; private set; }
    /// <summary>
    /// 动物基础数据 对应动物管理列表中的数据
    /// </summary>
    /// <value></value>
    public AnimBaseData BaseData { get; private set; }
    /// <summary>
    /// 动物存档数据
    /// </summary>
    /// <value></value>
    public AnimalSaveData SaveData { get; private set; }

    public void SetBaseData(AnimBaseData animalBaseData)
    {
        BaseData = animalBaseData;
        DRMonster = GFEntryCore.DataTable.GetDataTable<DRMonster>().GetDataRow(BaseData.Cid);
        if (DRMonster == null)
        {
            throw new Exception($"动物配置表中没有找到cid为{BaseData.Cid}的数据");
        }
    }

    public void SetSaveData(AnimalSaveData animalSaveData)
    {
        if (animalSaveData != null)
        {
            SaveData = animalSaveData;
            if (BaseData.AnimId != SaveData.AnimId)
            {
                Log.Error($"动物数据和存档数据不一致 BaseData.AnimId:{BaseData.AnimId} SaveData.AnimId:{SaveData.AnimId}");
                SaveData.AnimId = BaseData.AnimId;
            }
        }
        else
        {
            SaveData = new AnimalSaveData(BaseData.AnimId);
        }
    }
}