/*
 * @Author: xiang huan
 * @Date: 2022-12-06 10:27:50
 * @Description: 资源刷新区域
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Home/HomeResourcesArea.cs
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
    public bool isDraw;

    [Header("区域类型")]
    public HomeDefine.eHomeResourcesAreaType AreaType;

    [Header("刷新间隔(ms)")]
    public int UpdateInterval;

    [System.Serializable]
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
    private List<ulong> _soilIdList;
    private void Awake()
    {
        HomeModuleCore.HomeResourcesAreaMgr.AddArea(this);
        AreaBounds = new Bounds(transform.position, transform.localScale);

        if (AreaType == HomeDefine.eHomeResourcesAreaType.farmland)
        {
            InitFarmland();
        }
    }

    /// <summary>
    /// 初始化土地
    /// </summary>
    private void InitFarmland()
    {
        _soilIdList = new();
        Vector3 minPos = AreaBounds.min;
        Vector3 maxPos = AreaBounds.max;
        int countX = (int)MathF.Floor((maxPos.x - minPos.x) / HomeDefine.SOIL_SIZE.x);
        int countZ = (int)MathF.Floor((maxPos.z - minPos.z) / HomeDefine.SOIL_SIZE.z);

        for (int i = 0; i < countX; i++)
        {
            for (int j = 0; j < countZ; j++)
            {
                Vector3 pos = new(minPos.x + i * HomeDefine.SOIL_SIZE.x, minPos.y, minPos.z + j * HomeDefine.SOIL_SIZE.z);
                ulong id = MathUtilCore.AreaToSoil(Id, i, j);
                HomeModuleCore.SoilMgr.AddSoil(id, pos);
                _soilIdList.Add(id);
            }
        }
    }

    private void OnDestroy()
    {
        HomeModuleCore.HomeResourcesAreaMgr.RemoveArea(Id);
        if (_soilIdList != null)
        {
            for (int i = 0; i < _soilIdList.Count; i++)
            {
                HomeModuleCore.SoilMgr.RemoveSoil(_soilIdList[i]);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!isDraw)
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