using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 自定义的一些数学工具
/// </summary>
public static class MapUtilCore
{
    /// <summary>
    /// 和地面交点
    /// </summary>
    /// <param name="plane">地面高</param>
    /// <returns></returns>
    public static Vector3 GetPlaneInteractivePoint(Ray ray, float plane = 0)
    {
        Vector3 dir = ray.direction;

        if (Mathf.Approximately(dir.y, 0))
        {
            return Vector3.zero;
        }

        float num = (plane - ray.origin.y) / dir.y;
        return ray.origin + (ray.direction * num);
    }

    /// <summary>
    /// 根据给定的位置，获取离位置最近的课可行走位置，如果当前position可行走，则直接返回position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="maxError"></param>
    /// <returns></returns>
    public static Vector3 SampleTerrainWalkablePos(Vector3 position, float maxError = 10f)
    {
        _ = NavMesh.SamplePosition(position, out NavMeshHit hit, maxError, NavMesh.AllAreas);
        return hit.position;
    }
}