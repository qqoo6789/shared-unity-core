using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

/// <summary>
/// 走NavMesh方式的寻路移动组件
/// </summary>
public class NavMeshFindPathMove : FindPathMove
{
    private NavMeshAgent _refNavMeshAgent;//寻路代理
    private NavMeshPath _buffResultPath;//缓存避免GC的寻路结果

    protected override void Start()
    {
        base.Start();

        if (!TryGetComponent(out _refNavMeshAgent))
        {
            Log.Error($"NavMeshFindPathMove not find agent");
            return;
        }

        _buffResultPath = new();
    }

    protected override Vector3[] FindPath(Vector3 destination)
    {
        if (_refNavMeshAgent == null)
        {
            return null;
        }

        if (!_refNavMeshAgent.CalculatePath(destination, _buffResultPath))
        {
            return null;
        }

        if (_buffResultPath.corners == null || _buffResultPath.corners.Length <= 1)//<=1 是因为第一个点是起始位置 马上要剔除的
        {
            return null;
        }

        Vector3[] validPath = new Vector3[_buffResultPath.corners.Length - 1];
        System.Array.Copy(_buffResultPath.corners, 1, validPath, 0, validPath.Length);//第一个点是起始位置 需要剔除
        return validPath;
    }
}