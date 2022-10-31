using System;
using UnityEngine;

/// <summary>
/// 技能飞行物
/// </summary>
public abstract class SkillFlyerCore : MonoBehaviour
{
    public int FlyerID { get; private set; }
    public long FormEntityID { get; private set; }
    public DRSkill DRSkill { get; private set; }
    public long TargetEntityID { get; private set; }//没有为-1
    public Vector3? Dir { get; private set; }//没有为Null

    public void Init(int flyerID, long formEntityID, DRSkill drSkill)
    {
        FlyerID = flyerID;
        FormEntityID = formEntityID;
        DRSkill = drSkill;

        OnInit();
    }

    public void SetTargetEntityID(long targetID)
    {
        TargetEntityID = targetID;
        Dir = null;
    }

    public void SetDir(Vector3 dir)
    {
        Dir = dir;
        TargetEntityID = -1;
    }

    /// <summary>
    /// 移动到达时事件回调 子类实现
    /// </summary>
    public abstract void OnMoveArrived(object sender, EventArgs e);

    /// <summary>
    /// 初始化完成 子类可选实现
    /// </summary>
    protected virtual void OnInit() { }
}