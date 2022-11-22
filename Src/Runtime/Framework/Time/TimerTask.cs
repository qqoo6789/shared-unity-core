using System;
using GameFramework;
using UnityEngine;

/// <summary>
/// 定时器任务
/// </summary>
public class TimerTask : IReference
{
    public int UID;
    /// <summary>
    /// 持续时间 毫秒
    /// </summary>
    public float Duration;
    public Action FinishCB;

    /// <summary>
    /// 过期时间 真实时间
    /// </summary>
    public float ExpireTime;

    public void Init(int uid, float duration, Action finishCB)
    {
        UID = uid;
        Duration = duration;
        FinishCB = finishCB;

        Start();
    }

    private void Start()
    {
        ExpireTime = Time.realtimeSinceStartup + (Duration * TimeUtil.MS2S);
    }

    public void Clear()
    {
        UID = -1;
        Duration = 0;
        FinishCB = null;
    }

    public static TimerTask Create()
    {
        return ReferencePool.Acquire<TimerTask>();
    }

    public static void Release(TimerTask task)
    {
        ReferencePool.Release(task);
    }
}