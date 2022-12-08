/*
 * @Author: xiang huan
 * @Date: 2022-12-08 15:29:03
 * @Description: 家园资源区域模块
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Home/HomeResourcesAreaMgrCore.cs
 * 
 */

using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class HomeResourcesAreaMgrCore : MonoBehaviour
{
    protected Dictionary<int, HomeResourcesArea> AreaMap = new();

    public virtual void AddArea(HomeResourcesArea area)
    {
        if (AreaMap.ContainsKey(area.Id))
        {
            Log.Error($"Resources Area ID Repeat !!! ID = {area.Id}");
            return;
        }
        AreaMap.Add(area.Id, area);
    }
    public virtual void RemoveArea(int id)
    {
        if (!AreaMap.ContainsKey(id))
        {
            return;
        }
        _ = AreaMap.Remove(id);
    }

    public HomeResourcesArea GetArea(int id)
    {
        if (!AreaMap.ContainsKey(id))
        {
            return null;
        }
        return AreaMap[id];
    }

    public virtual void InitHomeAreaSaveData(HomeResourcesAreaSaveData[] areaSaveDataList)
    {

        Dictionary<int, HomeResourcesAreaSaveData> saveDataMap = new();
        for (int i = 0; i < areaSaveDataList.Length; i++)
        {
            saveDataMap.Add(areaSaveDataList[i].Id, areaSaveDataList[i]);
        }
        foreach (KeyValuePair<int, HomeResourcesArea> item in AreaMap)
        {
            HomeResourcesArea area = item.Value;
            if (saveDataMap.ContainsKey(area.Id))
            {
                area.InitSaveData(saveDataMap[area.Id]);
            }
            else
            {
                area.InitSaveData(null);
            }
        }
    }

    public List<HomeResourcesAreaSaveData> GetHomeAreaSaveData()
    {
        List<HomeResourcesAreaSaveData> list = new();
        foreach (KeyValuePair<int, HomeResourcesArea> item in AreaMap)
        {
            list.Add(item.Value.GetSaveData());
        }
        return list;
    }
}
