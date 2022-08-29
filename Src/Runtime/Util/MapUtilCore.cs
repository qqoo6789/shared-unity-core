using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

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
    /// 根据给定的位置，获取离位置最近的课可行走位置
    /// </summary>
    /// <param name="position"></param>
    /// <param name="walkablePos">地表可行走结果点</param>
    /// <param name="maxError"></param>
    /// <returns>寻路异常返回false out原始位置</returns>
    public static bool SampleTerrainWalkablePos(Vector3 position, out Vector3 walkablePos, float maxError = 10f)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, maxError, NavMesh.AllAreas))
        {
            Log.Warning($"SampleTerrainWalkablePos not find position:{position}");
            walkablePos = position;
            return false;
        }

        walkablePos = hit.position;
        return true;
    }
}