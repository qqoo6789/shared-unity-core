using System;
using UnityEngine;

/// <summary>
/// 到达自动销毁
/// </summary>
public class ArriveAutoDestroy : MonoBehaviour
{
    /// <summary>
    /// 到达事件
    /// </summary>
    public event EventHandler ArrivedEvent;

    /// <summary>
    /// 到达 子类调用
    /// </summary>
    protected void OnArrived()
    {
        ArrivedEvent?.Invoke(this, null);

        Destroy(this);//自我销毁
    }
}