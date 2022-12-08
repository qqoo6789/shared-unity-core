/*
 * @Author: xiang huan
 * @Date: 2022-12-08 15:29:03
 * @Description: 家园资源区域模块
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Home/HomeResourcesAreaMgrCore.cs
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
        AreaMap.Remove(id);
    }

    public HomeResourcesArea GetArea(int id)
    {
        if (!AreaMap.ContainsKey(id))
        {
            return null;
        }
        return AreaMap[id];
    }
}
