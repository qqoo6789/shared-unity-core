using System;
using UnityEngine;

/// <summary>
/// 单块土地上自身的事件
/// </summary>
public class SoilEvent : MonoBehaviour
{
    /// <summary>
    /// 使用保存数据初始化状态 T0:土地的保存数据
    /// </summary>
    public Action<SoilSaveData> MsgInitStatus;

    /// <summary>
    /// 执行某个动作 T0:动作类型 T1:效果值绝对值(比如锄地效果值)，非进度动作传0 T2：动作参数（比如播种的种子cid）
    /// </summary>
    public Action<HomeDefine.eAction, int, object> MsgExecuteAction;
}