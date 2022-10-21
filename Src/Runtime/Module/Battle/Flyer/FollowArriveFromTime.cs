using DG.Tweening;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 以固定时间跟随到达
/// </summary>
public class FollowArriveFromTime : ArriveAutoDestroy
{
    public Transform Target { get; private set; }
    private Tweener _tweener;

    /// <summary>
    /// 开始跟随
    /// </summary>
    /// <param name="target"></param>
    /// <param name="costTime">多久后到达 秒</param>
    public void StartMove(Transform target, float costTime)
    {
        Target = target;

        if (costTime <= 0)
        {
            Log.Error("FollowArriveFromTime cost time is zero.");
            OnArrived();
            return;
        }

        StartTween(costTime);
    }

    private void StartTween(float costTime)
    {
        if (_tweener != null)
        {
            StopTween();
        }

        _tweener = transform.DOMove(Target.position, costTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            StopTween();
            OnArrived();
        });
    }

    private void OnDestroy()
    {
        StopTween();
    }

    private void StopTween()
    {
        if (_tweener == null)
        {
            return;
        }

        _tweener.Kill();
        _tweener = null;
    }

    private void Update()
    {
        if (_tweener == null)
        {
            return;
        }

        float remainTime = _tweener.Duration() - _tweener.position;
        _ = _tweener.ChangeEndValue(Target.position, remainTime, true);
        transform.LookAt(Target);
    }
}