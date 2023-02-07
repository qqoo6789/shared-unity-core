using UnityEngine;
using static HomeDefine;

/// <summary>
/// 家园动作进度数据 挂在在每个土地或者采集物上
/// </summary>
public class HomeActionProgressData : MonoBehaviour
{
    public eAction CurProgressAction { get; private set; }

    public float CurProgressActionValue { get; set; }

    public float CurProgressActionMaxValue { get; private set; }

    private void Update()
    {
        UpdateProgressActionValueLost();
    }

    //在进度型操作中 进度值会一直衰减
    private void UpdateProgressActionValueLost()
    {
        if (CurProgressAction == eAction.None)
        {
            return;
        }

        if (CurProgressActionValue >= CurProgressActionMaxValue)//如果已经满了 不会衰减 此时在等服务器回包进入下一个阶段
        {
            return;
        }

        CurProgressActionValue -= SOIL_PROGRESS_ACTION_LOST_SPEED * Time.deltaTime;
        CurProgressActionValue = Mathf.Max(0, CurProgressActionValue);
    }

    public void StartProgressAction(eAction action, float maxValue)
    {
        if ((HomeDefine.PROGRESS_ACTION_MASK & action) == 0)
        {
            MLog.Error(eLogTag.home, $"不支持的进度操作:{action}");
            return;
        }

        if (CurProgressAction == action)
        {
            MLog.Error(eLogTag.home, "当前正在进行的进度操作和要开始的操作一样");
            return;
        }

        CurProgressAction = action;
        CurProgressActionMaxValue = maxValue;
        CurProgressActionValue = 0;
    }

    public void EndProgressAction()
    {
        CurProgressAction = eAction.None;
        CurProgressActionMaxValue = 0;
        CurProgressActionValue = 0;
    }
}