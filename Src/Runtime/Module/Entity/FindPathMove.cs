using System;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 寻路移动基类 业务层直接获取基类操作 需要外部自己设置PathMove组件和其中速度参数 寻路组件不重复设置 子类可能是navmesh寻路 也可能是A星等别的方式
/// </summary>
[RequireComponent(typeof(PathMove))]
public abstract class FindPathMove : MonoBehaviour
{
    private PathMove _pathMove;
    protected PathMove PathMove => _pathMove;
    protected Action<FindPathMove> MoveFinishCB;

    /// <summary>
    /// 当期目标点 停止移动后会置空
    /// </summary>
    /// <value></value>
    protected Vector3? Destination { get; private set; }

    protected virtual void Start()
    {
        if (!TryGetComponent(out _pathMove))
        {
            Log.Error($"find path move not find path move component,name={gameObject.name}");
        }
    }

    protected virtual void OnDestroy()
    {
        StopMove();

        _pathMove = null;
    }

    /// <summary>
    /// 移动到某个点 可能会寻路失败 返回false
    /// </summary>
    /// <param name="destination">目的地 如果目的地不能走路 有可能最终走到附近一点点可以走路的地方</param>
    /// <param name="moveFinishCB">移动到终点后回调</param>
    /// <returns></returns>
    public bool MoveToPosition(Vector3 destination, Action<FindPathMove> moveFinishCB = null)
    {
        if (_pathMove == null)
        {
            return false;
        }

        StopMove();

        Destination = destination;
        MoveFinishCB = moveFinishCB;

        Vector3[] path = FindPath(destination);
        if (path == null || path.Length == 0)
        {
            return false;
        }

        _pathMove.MovePath(path, (target) =>
        {
            //移动到达终点
            MoveFinishCB?.Invoke(this);

            Destination = null;
            MoveFinishCB = null;
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

        Destination = null;
        MoveFinishCB = null;
    }

    /// <summary>
    /// 子类实现找路逻辑
    /// </summary>
    /// <param name="destination">寻路目的地</param>
    /// <returns>找不到路径或者出错直接返回null即可</returns>
    protected abstract Vector3[] FindPath(Vector3 destination);
}