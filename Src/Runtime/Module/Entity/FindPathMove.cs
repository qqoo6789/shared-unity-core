using System;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 寻路移动基类 业务层直接获取基类操作 需要外部自己设置PathMove组件和其中速度参数 寻路组件不重复设置 子类可能是navmesh寻路 也可能是A星等别的方式
/// </summary>
public abstract class FindPathMove : MonoBehaviour
{
    private PathMove _pathMove;

    protected virtual void Start()
    {
        if (!TryGetComponent(out _pathMove))
        {
            Log.Error($"find path move not find path move component,name={gameObject.name}");
        }
    }

    protected virtual void OnDestroy()
    {
        if (_pathMove != null)
        {
            _pathMove.StopMove();
            _pathMove = null;
        }
    }

    /// <summary>
    /// 移动到某个点 可能会寻路失败 返回false
    /// </summary>
    /// <param name="destination">目的地 如果目的地不能走路 有可能最终走到附近一点点可以走路的地方</param>
    /// <param name="moveFinishCB">移动到终点后回调</param>
    /// <returns></returns>
    public bool MoveToPosition(Vector3 destination, Action<FindPathMove> moveFinishCB)
    {
        if (_pathMove == null)
        {
            return false;
        }

        StopMove();

        Vector3[] path = FindPath(destination);
        if (path == null || path.Length == 0)
        {
            return false;
        }

        _pathMove.MovePath(path, (target) =>
        {
            moveFinishCB?.Invoke(this);
        });
        return true;
    }

    /// <summary>
    /// 停止移动
    /// </summary>
    public void StopMove()
    {
        if (_pathMove != null)
        {
            _pathMove.StopMove();
        }
    }

    /// <summary>
    /// 子类实现找路逻辑
    /// </summary>
    /// <param name="destination">寻路目的地</param>
    /// <returns>找不到路径或者出错直接返回null即可</returns>
    protected abstract Vector3[] FindPath(Vector3 destination);
}