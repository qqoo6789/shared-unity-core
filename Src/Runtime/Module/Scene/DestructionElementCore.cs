using UnityEngine;

/// <summary>
/// 场景破坏元素配置
/// </summary>
public class DestructionElementCore : MonoBehaviour
{
    [SerializeField]
    private long _id;
    public long Id => _id;

    [Header("破坏后恢复时间 秒 -1表示不恢复")]
    public float RestoreTime = -1;

    /// <summary>
    /// 是否在被破坏状态
    /// </summary>
    /// <value></value>
    public bool IsDestroyed { get; private set; } = false;

    /// <summary>
    /// 设置破坏状态
    /// </summary>
    /// <param name="restoreTime">恢复时间 秒 -1代表不会恢复</param>
    public void SetDestroyedStatus(float restoreTime)
    {
        IsDestroyed = true;

        CancelRestoreTimer();

        //配置的-1代表不自动恢复
        if (restoreTime < 0)
        {
            return;
        }

        StartRestoreTimer(restoreTime);
    }

    /// <summary>
    /// 开始恢复计时
    /// </summary>
    /// <param name="restoreTime">恢复时间 秒</param>
    private void StartRestoreTimer(float restoreTime)
    {
        TimerMgr.AddTimer(GetHashCode(), restoreTime * TimeUtil.S2MS, OnRestore);
    }

    private void CancelRestoreTimer()
    {
        _ = TimerMgr.RemoveTimer(GetHashCode());
    }

    /// <summary>
    /// 开始恢复
    /// </summary>
    protected virtual void OnRestore()
    {
        IsDestroyed = false;
    }
}