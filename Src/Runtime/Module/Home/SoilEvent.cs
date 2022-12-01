using System;
using UnityEngine;

/// <summary>
/// 单块土地上自身的事件
/// </summary>
public class SoilEvent : MonoBehaviour
{
    public Action<SoilSaveData> MsgInitStatus;
}