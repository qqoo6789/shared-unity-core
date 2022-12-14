/*
 * @Author: xiang huan
 * @Date: 2022-12-02 10:52:02
 * @Description: 区域管理
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/Module/SceneArea/SceneAreaMgr.cs
 * 
 */


using System;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
//区域名字
public class SceneAreaMgr : SceneModuleBase
{
    private const int COUNT_HANDLE_AREA_CHANGED_PER_FRAME = 10;
    /// <summary>
    /// 家进入新的场景的检查区域事件
    /// <param long>玩家实体ID</param>
    /// <param eSceneArea>新区域标识</param>
    /// </summary>
    public Action<long, eSceneArea> OnPlayerEnterNewSceneCheckArea = delegate { };
    /// <summary>
    /// 玩家退出当前场景的检查区域事件
    /// <param long>玩家实体ID</param>
    /// <param eSceneArea>新区域标识</param>
    /// </summary>
    public Action<long, eSceneArea> OnPlayerExitCurSceneCheckArea = delegate { };
    private eSceneArea _defaultArea;
    private readonly List<eSceneArea> _areaQueue = new();

    /// <summary>
    /// 记录玩家当前的区域信息
    /// </summary>
    /// <returns></returns>
    private readonly Dictionary<long, PlayerAreaRecord> _playerAreaRecordDic = new();
    /// <summary>
    /// 区域变更信息列表
    /// </summary>
    /// <returns></returns>
    private readonly List<PlayerAreaRecord> _changedAreaList = new();
    /// <summary>
    /// 设置默认区域
    /// </summary>
    public void SetDefaultArea(eSceneArea area)
    {
        _defaultArea = area;
    }

    /// <summary>
    /// 进入区域
    /// </summary>
    public void EnterArea(eSceneArea area)
    {
        _areaQueue.Add(area);
    }

    /// <summary>
    /// 离开区域
    /// </summary>
    public void ExitArea(eSceneArea area)
    {
        _ = _areaQueue.Remove(area);
    }

    /// <summary>
    /// 获取当前区域
    /// </summary>
    public eSceneArea GetCurArea()
    {
        if (_areaQueue.Count > 0)
        {
            return _areaQueue[^1];
        }
        return _defaultArea;
    }

    /// <summary>
    /// 接收一个区域改变事件
    /// </summary>
    /// <param name="info"></param>
    public void ReceiveAreaChangedEvent(PlayerAreaChangedEvent info)
    {
        PlayerAreaRecord record = GetAddPlayerAreaRecord(info.PlayerID);
        if (info.Type == eAreaChangedType.enter)
        {
            record.PushToEnterPendingList(info.Data);
        }
        else
        {
            record.PushToExitPendingList(info.Data);
        }

        if (!_changedAreaList.Contains(record))
        {
            _changedAreaList.Add(record);
        }
    }

    /// <summary>
    /// 获取玩家的区域信息，如果没有则创建一个
    /// </summary>
    /// <param name="playerID"></param>
    /// <returns></returns>
    private PlayerAreaRecord GetAddPlayerAreaRecord(long playerID)
    {
        if (_playerAreaRecordDic.TryGetValue(playerID, out PlayerAreaRecord info))
        {
            return info;
        }

        info = new PlayerAreaRecord(playerID);
        _playerAreaRecordDic.Add(playerID, info);
        return info;
    }

    /// <summary>
    ///需要动态计算每次处理的数量
    /// </summary>
    /// <returns></returns>
    private int GetHandleCount()
    {
        // int count = COUNT_HANDLE_AREA_CHANGED_PER_FRAME > _changedAreaList.Count ? _changedAreaList.Count : COUNT_HANDLE_AREA_CHANGED_PER_FRAME;
        // return count;

        return _changedAreaList.Count;
    }

    private void Update()
    {
        ApplyAreaChanged();
    }

    private void ApplyAreaChanged()
    {
        if (_changedAreaList.Count <= 0)
        {
            return;
        }

        foreach (PlayerAreaRecord playerRecord in _changedAreaList)
        {
            eSceneArea playerLastArea = playerRecord.CurArea;
            playerRecord.ApplyAreaChanged();
            if (playerRecord.CurArea != playerLastArea)
            {
                Log.Info($"PlayerEnterAreaInfo, playerID:{playerRecord.PlayerID}, curArea:{playerRecord.CurArea}, lastArea:{playerLastArea}");
                OnPlayerExitCurSceneCheckArea?.Invoke(playerRecord.PlayerID, playerLastArea);//离开当前区域事件
                OnPlayerEnterNewSceneCheckArea?.Invoke(playerRecord.PlayerID, playerRecord.CurArea);//进入新区域事件
            }
        }
        _changedAreaList.Clear();
    }
}