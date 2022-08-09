using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

public static partial class MapUtil
{
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