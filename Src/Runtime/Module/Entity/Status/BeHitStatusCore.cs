/*
 * @Author: xiang huan
 * @Date: 2022-07-25 15:56:56
 * @Description: 受击状态 理论上受击状态只有表现,服务器用不到
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/BeHitStatusCore.cs
 * 
 */
using System;
using GameFramework.Fsm;

/// <summary>
/// 受击状态通用状态基类
/// </summary>
public abstract class BeHitStatusCore : EntityStatusCore
{
    /// <summary>
    /// 子类确定Idle状态类型
    /// </summary>
    /// <value></value>
    protected virtual Type IdleStatusType => typeof(IdleStatusCore);

    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

    protected virtual void OnBeHitComplete()
    {
        ChangeState(OwnerFsm, IdleStatusType);
    }
}