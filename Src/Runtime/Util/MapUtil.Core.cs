using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

public static partial class MapUtil
{
    /// <summary>
    ///  根据x,z获取地形高度
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static float GetTerrainHeight(float x, float z)
    {
        if (Terrain.activeTerrain == null)
        {
            Log.Error("Terrain is null.");
            return 0;
        }

        return Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));
    }

    /// <summary>
    /// 根据给定的position获取地形高度，其中position的x,z为平面坐标，y不关注
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static float GetTerrainHeight(Vector3 position)
    {
        return GetTerrainHeight(position.x, position.z);
    }

    /// <summary>
    /// 根据给定的position的x和z获取当前地形表面处的位置
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Vector3 SampleTerrainPosition(Vector3 position)
    {
        return new Vector3(position.x, GetTerrainHeight(position), position.z);
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