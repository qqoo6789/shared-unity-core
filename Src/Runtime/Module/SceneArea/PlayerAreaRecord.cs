using System.Collections.Generic;
using UnityGameFramework.Runtime;

/// <summary>
/// 玩家进出区域记录
/// </summary>
public class PlayerAreaRecord
{
    public long PlayerID;
    /// <summary>
    /// 当前区域堆栈
    /// 为什么不用栈？因为存在移除中间区域的可能，所以不能用栈，只能用List
    /// 为什么不用链表？因为数量级不会很大，List的长度撑死也就十几个，所以用List，用起来方便
    /// </summary>
    /// <returns></returns>
    private readonly List<SceneAreaInfo> _areaList = new();
    /// <summary>
    /// 进入区域处理队列
    /// </summary>
    /// <returns></returns>
    private readonly List<SceneAreaInfo> _readyToEnterList = new();
    /// <summary>
    /// 退出区域处理队列
    /// </summary>
    /// <returns></returns>
    private readonly List<SceneAreaInfo> _readyToExitList = new();
    /// <summary>
    /// player当前所在区域，默认值为大世界
    /// </summary>
    /// <value></value>
    public eSceneArea CurArea { get; private set; } = eSceneArea.world;

    public PlayerAreaRecord(long playerID)
    {
        PlayerID = playerID;
    }

    public void SetPlayerID(long playerID)
    {
        PlayerID = playerID;
    }

    /// <summary>
    /// 接受区域变化事件，进入区域和离开区域都会触发
    /// 先存入队列，等帧结束后再进行通过ApplyAreaChangedEvent处理
    /// </summary>
    /// <param name="info"></param>
    public void ReceiveAreaChangedEvent(PlayerAreaChangedEvent info)
    {
        if (info.Type == eAreaChangedType.enter)
        {
            PushToEnterPendingList(info.Data);
        }
        else
        {
            PushToExitPendingList(info.Data);
        }
    }

    /// <summary>
    /// 放进进入区域待处理队列
    /// 因为有多个区域同时进入的可能，所以需要按照优先级进行排序，等帧结束后再进行进入区域的处理
    /// </summary>
    /// <param name="info"></param>
    public void PushToEnterPendingList(SceneAreaInfo info)
    {
        int insertIndex = 0;
        for (; insertIndex < _readyToEnterList.Count; insertIndex++)
        {
            if (info.Priority < _readyToEnterList[insertIndex].Priority)
            {
                break;
            }
        }
        _readyToEnterList.Insert(insertIndex, info);
    }

    /// <summary>
    /// 放进退出区域待处理队列
    /// 因为有多个区域同时退出的可能，所以需要按照优先级进行排序，等帧结束后再进行退出区域的处理
    /// </summary>
    /// <param name="info"></param>
    public void PushToExitPendingList(SceneAreaInfo info)
    {
        int insertIndex = 0;
        for (; insertIndex < _readyToExitList.Count; insertIndex++)
        {
            if (info.Priority < _readyToExitList[insertIndex].Priority)
            {
                break;
            }
        }
        _readyToExitList.Insert(insertIndex, info);
    }

    /// <summary>
    /// 进入区域处理，会立刻改变当前区域
    /// </summary>
    /// <param name="info"></param>
    public void EnterAreaTrigger(SceneAreaInfo info)
    {
        _areaList.Add(info);
        CurArea = info.Area;
    }

    /// <summary>
    /// 退出区域触发器，会立刻改变当前区域
    /// </summary>
    /// <param name="area"></param>
    /// <returns></returns>
    public void ExitAreaTrigger(SceneAreaInfo area)
    {
        if (_areaList.Count == 0)
        {
            Log.Error("ExitArea error, areaQueue is empty");
            return;
        }

        for (int i = _areaList.Count - 1; i >= 0; i--)
        {
            if (_areaList[i].AreaID == area.AreaID)
            {
                Log.Info($"remove area:{area}");
                _areaList.RemoveAt(i);
                break;
            }
        }

        CurArea = _areaList.Count == 0 ? eSceneArea.world : _areaList[^1].Area;
    }

    public void ApplyAreaChanged()
    {
        foreach (SceneAreaInfo info in _readyToEnterList)
        {
            EnterAreaTrigger(info);
        }

        foreach (SceneAreaInfo info in _readyToExitList)
        {
            ExitAreaTrigger(info);
        }

        _readyToEnterList.Clear();
        _readyToExitList.Clear();
    }
}