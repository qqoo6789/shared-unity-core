using System;
using DG.Tweening;
using UnityEngine;
using UnityGameFramework.Runtime;
using static HomeDefine;

/// <summary>
/// 家园动作进度数据 挂在在每个土地或者采集物上
/// </summary>
public class HomeActionProgressData : MonoBehaviour
{
    private int _fullTimeStamp;//进度充满的时间戳 只在设置HoldToFull才有效
    private Action<HomeActionProgressData> _onProgressHoldFull;//进度满了后回调
    private Tweener _tween;
    /// <summary>
    /// 正在holdToFull中
    /// </summary>
    public bool HoldToFullHappening => _tween != null;

    /// <summary>
    /// 当前是否在进度型操作中 单一动作 不会是复合动作 None表示不在
    /// </summary>
    /// <value></value>
    public eAction CurProgressAction { get; private set; }

    /// <summary>
    /// 当前进度动作的进度值
    /// </summary>
    /// <value></value>
    public float CurProgressActionValue { get; private set; }

    /// <summary>
    /// 当前进度动作的最大值
    /// </summary>
    /// <value></value>
    public float CurProgressActionMaxValue { get; private set; }

    private void Update()
    {
        UpdateProgressActionValueLost();
    }

    //在进度型操作中 进度值会一直衰减
    private void UpdateProgressActionValueLost()
    {
        if (CurProgressAction == eAction.None)
        {
            return;
        }

        if (CurProgressActionValue >= CurProgressActionMaxValue)//如果已经满了 不会衰减 此时在等服务器回包进入下一个阶段
        {
            return;
        }

        CurProgressActionValue -= SOIL_PROGRESS_ACTION_LOST_SPEED * Time.deltaTime;
        CurProgressActionValue = Mathf.Max(0, CurProgressActionValue);
    }

    /// <summary>
    /// 开始一个有进度的动作 之后就会有了进度值逻辑
    /// </summary>
    /// <param name="action"></param>
    /// <param name="maxValue"></param>
    public void StartProgressAction(eAction action, float maxValue)
    {
        if ((PROGRESS_ACTION_MASK & action) == 0)
        {
            Log.Error($"不支持的进度操作:{action}");
            return;
        }

        if (CurProgressAction == action)
        {
            Log.Error("当前正在进行的进度操作和要开始的操作一样");
            return;
        }

        StopHoldToFull();

        CurProgressAction = action;
        CurProgressActionMaxValue = maxValue;
        CurProgressActionValue = 0;
    }

    /// <summary>
    /// 结束进度值逻辑
    /// </summary>
    public void EndProgressAction()
    {
        CurProgressAction = eAction.None;
        CurProgressActionMaxValue = 0;
        CurProgressActionValue = 0;

        StopHoldToFull();
    }

    /// <summary>
    /// 设置hold到满的时间
    /// </summary>
    /// <param name="fullTime">多久后充满 秒</param>
    /// <param name="onProgressHoldFull"></param>
    public void SetHoldToFull(float fullTime, Action<HomeActionProgressData> onProgressHoldFull = null)
    {
        StopHoldToFull();

        if (fullTime <= 0)
        {
            Log.Error("fullTime必须大于0");
            onProgressHoldFull?.Invoke(this);
            return;
        }

        _fullTimeStamp = (int)(TimeUtil.GetTimeStamp() + (fullTime * TimeUtil.S2MS));
        _onProgressHoldFull = onProgressHoldFull;
        _tween = DOTween.To(() => CurProgressActionValue, x => CurProgressActionValue = x, CurProgressActionMaxValue, fullTime);
        _tween.onComplete += () =>
        {
            CurProgressActionValue = CurProgressActionMaxValue;//防止精度问题
            _onProgressHoldFull?.Invoke(this);
            _onProgressHoldFull = null;
            _fullTimeStamp = 0;
            _tween = null;
        };
    }

    /// <summary>
    /// 停止hold到满的缓动
    /// </summary>
    public void StopHoldToFull()
    {
        if (_tween == null)
        {
            return;
        }

        _tween.Kill();
        _tween = null;
        _onProgressHoldFull = null;
        _fullTimeStamp = 0;
    }

    /// <summary>
    /// 直接设置进度值 会停掉hold到满的缓动
    /// </summary>
    /// <param name="progressValue">进度值</param>
    public void SetProgressValue(int progressValue)
    {
        StopHoldToFull();

        CurProgressActionValue = progressValue;
    }

    /// <summary>
    /// 获取初始化网络传递的进度信息 如果没有任何进度返回null
    /// </summary>
    /// <returns></returns>
    public GameMessageCore.CollectResourceProgressResult GetInitProxyProgressInfo()
    {
        if (CurProgressAction == eAction.None)
        {
            return null;
        }

        if (HoldToFullHappening)
        {
            return new()
            {
                TotalProgress = Mathf.CeilToInt(CurProgressActionValue),
                ProgressFullStamp = _fullTimeStamp,
            };
        }

        if (CurProgressActionValue <= 0)
        {
            return null;
        }

        return new()
        {
            TotalProgress = Mathf.CeilToInt(CurProgressActionValue),
        };
    }
}