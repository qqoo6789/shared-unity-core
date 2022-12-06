using System.Collections.Generic;
/*
 * @Author: xiang huan
 * @Date: 2022-12-06 10:27:50
 * @Description: 资源刷新区域
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/ServerConfig/Cpt/HomeResourcesAreaNodeCpt.cs
 * 
 */

using UnityEngine;
public class HomeResourcesAreaNodeCpt : MonoBehaviour, IServerDataNodeCpt
{
    [Header("区域类型")]
    public eHomeResourcesAreaType AreaType;

    [Header("刷新间隔(ms)")]
    public int UpdateInterval;

    [System.Serializable]
    public struct HomeResourcesArea
    {
        [Header("权重(百分位)")]
        public int Weight;
        [Header("配置ID")]
        public int ConfigId;
        [Header("刷新上限")]
        public int LimitNum;
    }
    [Header("资源列表")]
    public List<HomeResourcesArea> PointList;
    public object GetServerData()
    {
        HomeResourcesAreaData data = new();
        data.X = transform.position.x;
        data.Y = transform.position.y;
        data.Z = transform.position.z;
        data.Scale = new System.Numerics.Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        data.AreaType = AreaType;
        data.UpdateInterval = UpdateInterval;
        data.PointList = new();
        for (int i = 0; i < PointList.Count; i++)
        {
            HomeResourcesAreaPointData point = new();
            point.Weight = PointList[i].Weight;
            point.ConfigId = PointList[i].ConfigId;
            point.LimitNum = PointList[i].LimitNum;
            data.PointList.Add(point);
        }
        return data;
    }
}