/*
 * @Author: xiang huan
 * @Date: 2023-04-23 19:40:38
 * @Description: 自定义引用池, 回收时，需要自己清理引用
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/HotFix/Module/CustomReferencePool/CustomReferencePool.cs
 * 
 */

using System;
using System.Collections.Generic;
using GameFramework;
using UnityGameFramework.Runtime;

public static class CustomReferencePool
{
    public static readonly Dictionary<Type, CustomReferenceCollection> ReferenceCollections = new();

    /// <summary>
    /// 清除所有引用池。
    /// </summary>
    public static void ClearAll()
    {
        lock (ReferenceCollections)
        {
            foreach (KeyValuePair<Type, CustomReferenceCollection> referenceCollection in ReferenceCollections)
            {
                referenceCollection.Value.RemoveAll();
            }
            ReferenceCollections.Clear();
        }
    }

    /// <summary>
    /// 从引用池获取引用。
    /// </summary>
    /// <typeparam name="T">引用类型。</typeparam>
    /// <returns>引用。</returns>
    public static T Acquire<T>() where T : class, new()
    {

        return GetReferenceCollection(typeof(T)).Acquire<T>();
    }

    /// <summary>
    /// 从引用池获取引用。
    /// </summary>
    /// <param name="referenceType">引用类型。</param>
    /// <returns>引用。</returns>
    public static object Acquire(Type referenceType)
    {
        return GetReferenceCollection(referenceType);
    }

    /// <summary>
    /// 将引用归还引用池。
    /// </summary>
    /// <param name="reference">引用。</param>
    public static void Release(object reference)
    {
        if (reference == null)
        {
            Log.Error("Custom Reference is invalid.");
            return;
        }

        Type referenceType = reference.GetType();
        GetReferenceCollection(referenceType).Release(reference);
    }

    /// <summary>
    /// 从引用池中移除所有的引用。
    /// </summary>
    /// <typeparam name="T">引用类型。</typeparam>
    public static void RemoveAll<T>() where T : class, IReference
    {
        GetReferenceCollection(typeof(T)).RemoveAll();
    }

    /// <summary>
    /// 从引用池中移除所有的引用。
    /// </summary>
    /// <param name="referenceType">引用类型。</param>
    public static void RemoveAll(Type referenceType)
    {
        GetReferenceCollection(referenceType).RemoveAll();
    }

    private static CustomReferenceCollection GetReferenceCollection(Type referenceType)
    {
        if (referenceType == null)
        {
            Log.Error("Custom ReferenceType is invalid.");
            return null;
        }

        CustomReferenceCollection referenceCollection = null;
        lock (ReferenceCollections)
        {
            if (!ReferenceCollections.TryGetValue(referenceType, out referenceCollection))
            {
                referenceCollection = new CustomReferenceCollection(referenceType);
                ReferenceCollections.Add(referenceType, referenceCollection);
            }
        }
        return referenceCollection;
    }
}

