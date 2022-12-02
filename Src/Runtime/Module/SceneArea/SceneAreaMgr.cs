/*
 * @Author: xiang huan
 * @Date: 2022-12-02 10:52:02
 * @Description: 区域管理
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/SceneArea/SceneAreaMgr.cs
 * 
 */


using System.Collections.Generic;
//区域名字
public class SceneAreaMgr : SceneModuleBase
{
    public eSceneArea defaultArea;
    private readonly List<eSceneArea> _areaQueue = new();
    /// <summary>
    /// 设置默认区域
    /// </summary>
    public void SetDefaultArea(eSceneArea area)
    {
        defaultArea = area;
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
        return defaultArea;
    }
}