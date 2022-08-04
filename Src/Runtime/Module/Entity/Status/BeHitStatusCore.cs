/*
 * @Author: xiang huan
 * @Date: 2022-07-25 15:56:56
 * @Description: 受击状态 理论上受击状态只有表现,服务器用不到
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/BeHitStatusCore.cs
 * 
 */
using GameFramework.Fsm;

/// <summary>
/// 受击状态通用状态基类
/// </summary>
public abstract class BeHitStatusCore : EntityStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name => "beHit";

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
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }

    public bool CheckCanMove()
    {
        return true;
    }

    public bool CheckCanSkill()
    {
        return true;
    }
}