/*
 * @Author: xiang huan
 * @Date: 2022-12-02 10:52:02
 * @Description: 区域管理
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/Module/SceneArea/SceneAreaMgr.cs
 * 
 */


using System;
using System.Collections.Generic;
using UnityEngine;
//区域名字
public class SceneAreaMgr : SceneModuleBase
{
    private const int COUNT_HANDLE_ENTER_AREA_PER_FRAME = 10;
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
    /// 当前出发区域计数器
    /// 进入重复区域计数器+1，离开重复区域计数器-1，当计数器为0时，表示离开重复区域
    /// 进入新区域时，重置计数器
    /// </summary>
    /// <returns></returns>
    private readonly Dictionary<long, PlayerAreaRecord> _playerEnterAreaCounterDic = new();
    private readonly Queue<PlayerAreaChangedEvent> _areaChangedEventQueue = new();
    private readonly List<PlayerAreaRecord> _diffList = new();
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
        _areaChangedEventQueue.Enqueue(info);
    }

    /// <summary>
    /// 获取玩家的区域信息，如果没有则创建一个
    /// </summary>
    /// <param name="playerID"></param>
    /// <returns></returns>
    private PlayerAreaRecord GetAddPlayerEnterAreaInfo(long playerID)
    {
        if (_playerEnterAreaCounterDic.TryGetValue(playerID, out PlayerAreaRecord info))
        {
            return info;
        }
        else
        {
            info = new PlayerAreaRecord(playerID);
            info.SetPlayerID(playerID);
            _playerEnterAreaCounterDic.Add(playerID, info);
            return info;
        }
    }

    /// <summary>
    ///需要动态计算每次处理的数量
    /// </summary>
    /// <returns></returns>
    private int GetHandleCount()
    {
        // int count = COUNT_HANDLE_ENTER_AREA_PER_FRAME > _areaChangedEventQueue.Count ? _areaChangedEventQueue.Count : COUNT_HANDLE_ENTER_AREA_PER_FRAME;
        // return count;

        return _areaChangedEventQueue.Count;
    }

    private void Update()
    {
        HandleAreaChangedEvent();
    }

    private void HandleAreaChangedEvent()
    {
        int handleCount = GetHandleCount();
        for (int i = 0; i < handleCount; i++)
        {
            PlayerAreaChangedEvent info = _areaChangedEventQueue.Dequeue();
            PlayerAreaRecord playerEnterAreaInfo = GetAddPlayerEnterAreaInfo(info.PlayerID);
            playerEnterAreaInfo.ReceiveAreaChangedEvent(info);

            //TODO:去重方式可以优化
            if (!_diffList.Contains(playerEnterAreaInfo))
            {
                _diffList.Add(playerEnterAreaInfo);
            }
        }

        if (_diffList.Count > 0)
        {
            foreach (PlayerAreaRecord item in _diffList)
            {
                //上面只是进队列，这里要应用一下
                item.ApplyAreaChangedEvent();
                eSceneArea curArea = GetCurArea();
                if (item.CurArea != curArea)
                {
                    Debug.Log($"PlayerEnterAreaInfo, playerID:{item.PlayerID}, curArea:{item.CurArea}, lastArea:{curArea}");
                    OnPlayerExitCurSceneCheckArea?.Invoke(item.PlayerID, curArea);//离开当前区域事件
                    OnPlayerEnterNewSceneCheckArea?.Invoke(item.PlayerID, item.CurArea);//进入新区域事件
                }
            }
            _diffList.Clear();
        }
    }
}