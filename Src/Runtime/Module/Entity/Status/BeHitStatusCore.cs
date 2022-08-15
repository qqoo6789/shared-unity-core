
/*
 * @Author: xiang huan
 * @Date: 2022-07-25 15:56:56
 * @Description: 受击状态 理论上受击状态只有表现,服务器用不到
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Status/BeHitStatusCore.cs
 * 
 */
using System;
using GameFramework.Fsm;

/// <summary>
/// 受击状态通用状态基类
/// </summary>
public abstract class BeHitStatusCore : ListenEventStatusCore, IEntityCanMove, IEntityCanSkill
{
    public static new string Name => "beHit";
    private EntityBattleDataCore _battleData;

    protected override Type[] EventFunctionTypes => new Type[] {
        typeof(OnInputSkillInBattleStatusEventFunc),
        typeof(BeHitMoveEventFunc)
     };

    public override string StatusName => Name;
    protected override void OnEnter(IFsm<EntityStatusCtrl> fsm)
    {
        base.OnEnter(fsm);
        _battleData = StatusCtrl.GetComponent<EntityBattleDataCore>();
    }

    protected override void OnLeave(IFsm<EntityStatusCtrl> fsm, bool isShutdown)
    {
        _battleData = null;
        base.OnLeave(fsm, isShutdown);
    }

    protected virtual void OnBeHitComplete()
    {
        ChangeState(OwnerFsm, IdleStatusCore.Name);
    }
    protected override void OnUpdate(IFsm<EntityStatusCtrl> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (_battleData && !_battleData.IsLive())
        {
            ChangeState(fsm, DeathStatusCore.Name);
            return;
        }
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