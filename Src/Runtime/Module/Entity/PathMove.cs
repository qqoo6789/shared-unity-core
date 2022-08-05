using System;
using UnityEngine;
using DG.Tweening;
using UnityGameFramework.Runtime;

/// <summary>
/// 路径移动
/// </summary>
public class PathMove : MonoBehaviour
{
    /// <summary>
    /// 是否是刚体移动
    /// </summary>
    public bool IsRigidbodyMove = false;

    [SerializeField]
    private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    private Rigidbody _refRigidbody;

    [SerializeField]
    private Vector3[] _curPath;
    /// <summary>
    /// 当前路径 不要修改 没有GC
    /// </summary>
    public Vector3[] CurPath => _curPath;
    private Action<PathMove> _arrivedCB;

    private Tweener _curMoveTweener;

    /// <summary>
    /// 是否正在移动
    /// </summary>
    public bool IsMoving => _curPath != null;

    /// <summary>
    /// 位置更新事件 T0：最新位置
    /// </summary>
    public event Action<Vector3> OnPosUpdatedEvent;
    /// <summary>
    /// 路径拐点变化 如果先监听了再移动 会立马回调下一个拐点位置 T0：下个目标拐点位置
    /// </summary>
    public event Action<Vector3> OnWaypointChangedEvent;

    private void Awake()
    {
        _moveSpeed = 1;//不能为0的初始速度
    }

    private void OnDestroy()
    {
        StopMove();

        _refRigidbody = null;
    }

    /// <summary>
    /// 设置移动速度 m/s
    /// </summary>
    /// <param name="speed"></param>
    public void SetMoveSpeed(float speed)
    {
        if (speed <= 0)
        {
            Log.Error($"path move set speed <0 ={speed}");
            return;
        }

        if (speed.ApproximatelyEquals(0))
        {
            Log.Error($"path move set speed approximately equals 0 ={speed}");
            return;
        }

        _moveSpeed = speed;
    }

    /// <summary>
    /// 移动到某个位置
    /// </summary>
    /// <param name="targetPoint"></param>
    public void MovePoint(Vector3 targetPoint, Action<PathMove> arrivedCB = null)
    {
        StopMove();

        _arrivedCB = arrivedCB;
        _curPath = new Vector3[] { targetPoint };

        ExecuteMove();
    }

    /// <summary>
    /// 按照路径移动
    /// </summary>
    /// <param name="path"></param>
    public void MovePath(Vector3[] path, Action<PathMove> arrivedCB = null)
    {
        StopMove();

        _arrivedCB = arrivedCB;
        _curPath = path;

        ExecuteMove();
    }

    /// <summary>
    /// 停止移动
    /// </summary>
    public void StopMove()
    {
        _arrivedCB = null;

        if (_curMoveTweener != null)
        {
            _curMoveTweener.Kill();
            _curMoveTweener = null;
        }

        _curPath = null;
    }

    private void ExecuteMove()
    {
        if (_curPath == null || _curPath.Length == 0)
        {
            Log.Error($"path move not find path ={_curPath}");
            return;
        }

        float duration = CalculatePathDuration(_curPath, _moveSpeed);

        if (IsRigidbodyMove)
        {
            if (CheckRigidbody())
            {
                _curMoveTweener = _refRigidbody.DOPath(_curPath, duration, PathType.Linear, PathMode.Ignore, 10, Color.green);
            }
        }
        else
        {
            _curMoveTweener = transform.DOPath(_curPath, duration, PathType.Linear, PathMode.Ignore, 10, Color.green);
        }

        if (_curMoveTweener != null)
        {
            _curMoveTweener.SetEase(Ease.Linear).onComplete += OnMoveArrived;
            _curMoveTweener.onUpdate += OnPosUpdated;
            _curMoveTweener.onWaypointChange += OnWaypointChanged;
        }
    }

    private void OnMoveArrived()
    {
        if (_arrivedCB != null)
        {
            _arrivedCB(this);
            _arrivedCB = null;
        }

        _curPath = null;
    }

    private void OnPosUpdated()
    {
        OnPosUpdatedEvent?.Invoke(transform.position);
    }

    private void OnWaypointChanged(int index)
    {
        //index 是到拐点本身   如果路径长度为4  会在0~4 一开始移动会到0 最后一个点会是4
        if (index > _curPath.Length)//正常情况最后一次回调就是index==_curPath.Length
        {
            Log.Error($"path move not find way point index:{index} curLenght:{_curPath.Length}");
            return;
        }

        //tween本意是到拐点回调 所以最后index是这样 但是我们为了回调当做目标点更新
        if (index == _curPath.Length)
        {
            return;
        }

        Vector3 nextPoint = _curPath[index];
        Vector3 moveDirVector3 = nextPoint - transform.position;
        transform.forward = new Vector3(moveDirVector3.x, transform.forward.y, moveDirVector3.z);
        OnWaypointChangedEvent?.Invoke(nextPoint);
    }

    /// <summary>
    /// 计算路径所需时间
    /// </summary>
    /// <param name="path"></param>
    /// <param name="speed">m/s</param>
    /// <returns></returns>
    private float CalculatePathDuration(Vector3[] path, float speed)
    {
        float distance = CalculateDistance(path);
        return distance / speed;
    }

    /// <summary>
    /// 计算路径距离
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private float CalculateDistance(Vector3[] path)
    {
        if (path == null || path.Length == 0)
        {
            return 0;
        }

        float distance = 0;
        for (int i = 0; i < path.Length; i++)
        {
            if (i == 0)
            {
                distance += Vector3.Distance(transform.position, path[i]);
            }
            else
            {
                distance += Vector3.Distance(path[i - 1], path[i]);
            }
        }
        return distance;
    }

    /// <summary>
    /// 检查设置刚体 返回是否满足刚体运动
    /// </summary>
    /// <returns></returns>
    private bool CheckRigidbody()
    {
        if (!IsRigidbodyMove)
        {
            return false;
        }

        if (_refRigidbody != null)
        {
            return true;
        }

        return TryGetComponent(out _refRigidbody);
    }
}