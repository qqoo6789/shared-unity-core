/*
 * @Author: xiang huan
 * @Date: 2022-07-26 15:38:17
 * @Description: 共享库GFEntry引用
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/GFEntryCore.cs
 * 
 */
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;

public static class GFEntryCore
{
    private static List<object> s_GFEntryList = new();
    /// <summary>
    /// 获取数据表组件。
    /// </summary>
    private static IDataTableComponent s_DataTableComponent = null;
    public static IDataTableComponent DataTable
    {
        get
        {
            if (s_DataTableComponent == null)
            {
                s_DataTableComponent = GetModule<IDataTableComponent>();
            }

            return s_DataTableComponent;
        }
    }

    private static SkillEffectCoreFactory s_SkillEffectCoreFactory = null;
    public static SkillEffectCoreFactory SkillEffectFactory
    {
        get
        {
            if (s_SkillEffectCoreFactory == null)
            {
                s_SkillEffectCoreFactory = GetModule<SkillEffectCoreFactory>();
            }

            return s_SkillEffectCoreFactory;
        }
    }
    private static HomeResourcesAreaMgrCore s_homeResourcesAreaMgrCore = null;
    public static HomeResourcesAreaMgrCore HomeResourcesAreaMgr
    {
        get
        {
            if (s_homeResourcesAreaMgrCore == null)
            {
                s_homeResourcesAreaMgrCore = GetModule<HomeResourcesAreaMgrCore>();
            }

            return s_homeResourcesAreaMgrCore;
        }
    }
    public static void AddModule(object module)
    {
        if (s_GFEntryList.IndexOf(module) != -1)
        {
            Log.Error($"GFEntry module is already exist, type {module.GetType().Name}.");
            return;
        }

        s_GFEntryList.Add(module);
    }

    public static void RemoveModule(object module)
    {
        bool remove = s_GFEntryList.Remove(module);
        if (!remove)
        {
            Log.Error($"GFEntry module is not exist, type {module.GetType().Name}.");
        }
    }
    /// <summary>
    /// 注意！！！频繁获取会有性能损耗，如果有频繁获取的需求，请参照DataTable的获取方式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T GetModule<T>() where T : class
    {
        foreach (object item in s_GFEntryList)
        {
            if (item is T)
            {
                return item as T;
            }
        }

        Log.Error($"GFEntry module is not exist, type {typeof(T).Name}.");
        return null;
    }
}
