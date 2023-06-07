/*
 * @Author: xiang huan
 * @Date: 2022-12-02 10:52:02
 * @Description: 区域管理
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/Module/SceneArea/SceneAreaMgr.cs
 * 
 */


using System;
using System.Collections.Generic;
//区域名字
public class SceneAreaMgr : SceneModuleBase
{
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
    public eSceneArea DefaultArea { get; private set; }
    // private readonly List<eSceneArea> _areaQueue = new();
    /// <summary>
    /// 设置默认区域
    /// </summary>
    public void SetDefaultArea(eSceneArea area)
    {
        DefaultArea = area;
    }

    /// <summary>
    /// 进入区域
    /// </summary>
    // public void EnterArea(eSceneArea area)
    // {
    //     _areaQueue.Add(area);
    // }

    // /// <summary>
    // /// 离开区域
    // /// </summary>
    // public void ExitArea(eSceneArea area)
    // {
    //     _ = _areaQueue.Remove(area);
    // }

    /// <summary>
    /// 获取当前区域
    /// </summary>
    // public eSceneArea GetCurArea()
    // {
    //     if (_areaQueue.Count > 0)
    //     {
    //         return _areaQueue[^1];
    //     }
    //     return DefaultArea;
    // }
}