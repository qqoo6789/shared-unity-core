using System;
using UnityEngine;

/// <summary>
/// 场景破坏元素配置
/// </summary>
/// 
[ExecuteAlways]
public class DestructionElementCore : MonoBehaviour
{
    /// <summary>
    /// 这个是给服务器专用的 因为服务器场景上元素只能是core 已经存在好的 没办法服务器又继承定义自己的元素挂上去 通过这个钩子告诉其他地方有一个初始化了
    /// </summary>
    public static Action<DestructionElementCore> OnDestructionElementInitHook;

    [SerializeField]
    [Header("全局ID 自动生成 不要乱改")]
    private long _id;
    public long Id => _id;

    [Header("破坏后恢复时间 秒 -1表示不恢复")]
    public float RestoreTime = -1;

    /// <summary>
    /// 当本脚本销毁前调用事件 也是外部钩子
    /// </summary>
    public event EventHandler ScriptDestroyEvent;
    /// <summary>
    /// 当恢复时事件 也是外部钩子
    /// </summary>
    public event EventHandler RestoreEvent;

    /// <summary>
    /// 是否在被破坏状态
    /// </summary>
    /// <value></value>
    public bool IsDestroyed { get; private set; } = false;

    /// <summary>
    /// 恢复剩余时间 秒 如果配置不恢复或者没有被破坏 则为-1
    /// </summary>
    /// <value></value>
    public float RestoreRemainTime
    {
        get
        {
            if (!IsDestroyed || RestoreTime < 0)
            {
                return -1;
            }

            float remainTime = _restoreTimerDuration - (Time.realtimeSinceStartup - _restoreTimerStartTime);
            remainTime = Math.Max(remainTime, 0);//有可能定时器算出来有误差小于0 需要修正
            return remainTime;
        }
    }

    private float _restoreTimerStartTime;//恢复定时器开始时间 不一定是破坏的时间 客户端就可能中途设置定时器 只是用来算剩余时间的
    private float _restoreTimerDuration;//恢复定时器当前设定的持续时间 秒

    private void Awake()
    {
        // if (!Application.isPlaying)
        // {
        //     if (_id != default)//防止客户端每次打开场景都会重新生成
        //     {
        //         UnityEngine.Debug.LogError($"log test reset {name} ,id={GetInstanceID()}");
        //         _id = GetInstanceID();//编辑器添加时下给定一个全局ID
        //     }
        // }

        OnDestructionElementInitHook?.Invoke(this);
    }

    protected virtual void OnDestroy()
    {
        ScriptDestroyEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Reset()
    {
        UnityEngine.Debug.LogError($"log test reset {name} ,id={GetInstanceID()}");
        _id = GetInstanceID();//编辑器添加时下给定一个全局ID
    }

    /// <summary>
    /// 设置破坏状态
    /// </summary>
    /// <param name="restoreTime">恢复时间 秒 -1代表不会恢复</param>
    public virtual void SetDestroyedStatus(float restoreTime)
    {
        IsDestroyed = true;

        gameObject.SetActive(false);

        CancelRestoreTimer();

        //配置的-1代表不自动恢复
        if (restoreTime < 0)
        {
            return;
        }

        StartRestoreTimer(restoreTime);
    }

    /// <summary>
    /// 开始恢复
    /// </summary>
    protected virtual void OnRestore()
    {
        IsDestroyed = false;

        _restoreTimerStartTime = 0;
        _restoreTimerDuration = 0;

        gameObject.SetActive(true);

        RestoreEvent?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 开始恢复计时
    /// </summary>
    /// <param name="restoreTime">恢复时间 秒</param>
    private void StartRestoreTimer(float restoreTime)
    {
        _restoreTimerStartTime = Time.realtimeSinceStartup;
        _restoreTimerDuration = restoreTime;
        TimerMgr.AddTimer(GetHashCode(), restoreTime * TimeUtil.S2MS, OnRestore);
    }

    private void CancelRestoreTimer()
    {
        _restoreTimerStartTime = 0;
        _restoreTimerDuration = 0;
        _ = TimerMgr.RemoveTimer(GetHashCode());
    }
}