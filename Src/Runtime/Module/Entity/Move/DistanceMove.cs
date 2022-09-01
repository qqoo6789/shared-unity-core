using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 按距离朝某个方向移动 能不能移动过去 取决于是否有碰撞体等其他子类实现 但是会按照速度消耗掉所有移动距离位置 不会立马停止移动
/// </summary>
public abstract class DistanceMove : EntityMoveBase
{
    private float _remainDistance = 0;//剩下未移动的距离
    private Vector3 _directionUnit;//移动方向单位

    public void MoveTo(Vector3 dir, float distance, float speed)
    {
        if (dir == Vector3.zero)
        {
            Log.Error($"distance move to dir is zero");
            return;
        }

        if (distance <= 0)
        {
            Log.Error($"distance move set distance <0 ={distance}");
            return;
        }

        _remainDistance = distance;
        SetMoveSpeed(speed);
        _directionUnit = dir.normalized;

        StartMove();
    }

    /// <summary>
    /// 走一步 需要在合适时机触发
    /// </summary>
    /// <param name="tickDelay"></param>
    protected void TickMove(float tickDelay)
    {
        if (_remainDistance <= 0)
        {
            return;
        }

        float stepDistance = MoveSpeed * tickDelay;

        bool isArrived = false;
        if (_remainDistance <= stepDistance)
        {
            stepDistance = _remainDistance;
            _remainDistance = -1;
            isArrived = true;
        }
        else
        {
            _remainDistance -= stepDistance;
        }

        Vector3 motion = _directionUnit * stepDistance;
        ApplyMotion(motion);

        if (isArrived)
        {
            FinishMove();
        }
    }

    /// <summary>
    /// 子类应用目标移动增量 每帧的增量
    /// </summary>
    /// <param name="motion">本帧本次的移动增量 由于可能剩余距离不足一帧位移 所以可能没有一帧距离那么多</param>
    protected abstract void ApplyMotion(Vector3 motion);
}