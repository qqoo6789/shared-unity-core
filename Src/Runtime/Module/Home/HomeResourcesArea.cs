/*
 * @Author: xiang huan
 * @Date: 2022-12-06 10:27:50
 * @Description: 资源刷新区域
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Home/HomeResourcesArea.cs
 * 
 */
using System.Collections.Generic;
using UnityEngine;
using System;
public class HomeResourcesArea : SharedCoreComponent
{
    [Header("唯一区域ID(不可重复)")]
    public int Id;

    [Header("是否绘制形状")]
    public bool IsDraw;

    [Header("区域类型")]
    public HomeDefine.eHomeResourcesAreaType AreaType;

    [Header("刷新间隔(ms)")]
    public int UpdateInterval;

    [Serializable]
    public struct HomeResourcesAreaPoint
    {
        [Header("权重(百分位)")]
        public int Weight;
        [Header("配置ID")]
        public int ConfigId;
        [Header("刷新上限")]
        public int LimitNum;
    }
    [Header("资源列表")]
    public List<HomeResourcesAreaPoint> PointList;

    public Bounds AreaBounds { get; private set; }
    public HomeResourcesAreaSaveData SaveData { get; private set; }  //保存数据
    private void Awake()
    {
        GFEntryCore.HomeResourcesAreaMgr.AddArea(this);
        AreaBounds = new Bounds(transform.position, transform.localScale);
        SaveData = CreateSaveData();
    }


    private void OnDestroy()
    {
        GFEntryCore.HomeResourcesAreaMgr.RemoveArea(Id);
    }

    /// <summary>
    /// 创建存储数据
    /// </summary>
    protected HomeResourcesAreaSaveData CreateSaveData()
    {
        HomeResourcesAreaSaveData data = new()
        {
            Id = Id,
            PointList = new(),
            UpdateTime = TimeUtil.GetTimeStamp() + UpdateInterval
        };
        return data;
    }

    /// <summary>
    /// 初始化存储数据
    /// </summary>
    public void SetSaveData(HomeResourcesAreaSaveData saveData)
    {
        if (saveData == null)
        {
            return;
        }
        SaveData = saveData;
    }

    /// <summary>
    /// 获得存储数据
    /// </summary>
    public HomeResourcesAreaSaveData GetSaveData()
    {
        return SaveData;
    }

    /// <summary>
    /// 设置存储数据刷新时间
    /// </summary>
    public void SetSaveDataUpdateTime(long time)
    {
        if (SaveData == null)
        {
            return;
        }
        SaveData.UpdateTime = time;
    }

    /// <summary>
    /// 添加存储数据资源点
    /// </summary>
    public void AddSaveDataPoint(HomeResourcesPointSaveData pointData)
    {
        if (SaveData == null)
        {
            return;
        }
        SaveData.PointList.Add(pointData);
    }

    /// <summary>
    /// 删除存储数据资源点
    /// </summary>
    public void RemoveSaveDataPoint(ulong id)
    {
        if (SaveData == null)
        {
            return;
        }
        for (int i = 0; i < SaveData.PointList.Count; i++)
        {
            if (SaveData.PointList[i].Id == id)
            {
                SaveData.PointList.RemoveAt(i);
                break;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!IsDraw)
        {
            return;
        }
        Gizmos.color = Color.blue;
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = oldMatrix;
    }
}