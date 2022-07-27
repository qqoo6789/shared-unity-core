using System.Threading;
using System.Threading.Tasks;
using GameFramework;
using GameFramework.DataTable;

/// <summary>
/// 临时接口 读表生效时迁移 功能不完整 只是为了将来替换方便
/// </summary>
public static class TemporaryInterface
{
    public static IDataTable<T> GetDataTable<T>() where T : IDataRow
    {
        return null;
    }


    //模拟 UniTask的定时器
    public static Task Delay(int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
    {
        return new Task(null);
    }
}

public enum PlayerLoopTiming
{
    Initialization = 0,
    LastInitialization = 1,

    EarlyUpdate = 2,
    LastEarlyUpdate = 3,

    FixedUpdate = 4,
    LastFixedUpdate = 5,

    PreUpdate = 6,
    LastPreUpdate = 7,

    Update = 8,
    LastUpdate = 9,

    PreLateUpdate = 10,
    LastPreLateUpdate = 11,

    PostLateUpdate = 12,
    LastPostLateUpdate = 13,

#if UNITY_2020_2_OR_NEWER
    // Unity 2020.2 added TimeUpdate https://docs.unity3d.com/2020.2/Documentation/ScriptReference/PlayerLoop.TimeUpdate.html
    TimeUpdate = 14,
    LastTimeUpdate = 15,
#endif
}

/// <summary>
/// System.Int32 变量类。
/// </summary>
public sealed class VarInt32 : Variable<int>
{
    /// <summary>
    /// 初始化 System.Int32 变量类的新实例。
    /// </summary>
    public VarInt32()
    {
    }

    /// <summary>
    /// 从 System.Int32 到 System.Int32 变量类的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator VarInt32(int value)
    {
        VarInt32 varValue = ReferencePool.Acquire<VarInt32>();
        varValue.Value = value;
        return varValue;
    }

    /// <summary>
    /// 从 System.Int32 变量类到 System.Int32 的隐式转换。
    /// </summary>
    /// <param name="value">值。</param>
    public static implicit operator int(VarInt32 value)
    {
        return value.Value;
    }
}