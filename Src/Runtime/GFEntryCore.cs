/*
 * @Author: xiang huan
 * @Date: 2022-07-26 15:38:17
 * @Description: 共享库GFEntry引用
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/GFEntryCore.cs
 * 
 */
using System;
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;

public static class GFEntryCore
{
    private static Dictionary<Type, object> s_GFEntryDict = new();
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

    public static void AddModule(object module)
    {
        Type type = module.GetType();
        if (s_GFEntryDict.ContainsKey(type))
        {
            Log.Warning($"GFEntry module is already exist, type {type.Name}.");
        }

        s_GFEntryDict.Add(type, module);
    }

    public static void RemoveModule(object module)
    {
        Type type = module.GetType();
        if (!s_GFEntryDict.ContainsKey(type))
        {
            Log.Warning($"GFEntry module is not exist, type {type.Name}.");
        }

        s_GFEntryDict.Remove(type);
    }

    public static T GetModule<T>() where T : class
    {
        if (s_GFEntryDict.TryGetValue(typeof(T), out object module))
        {
            return module as T;
        }

        Log.Error($"GFEntry module is not exist, type {typeof(T).Name}.");
        return null;
    }
}
