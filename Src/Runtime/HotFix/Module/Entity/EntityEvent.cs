/* 
 * @Author XQ
 * @Date 2022-08-15 11:15:06
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/EntityEvent.cs
 */
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体上的内部消息组件 必然存在在实体上 里面都是各种消息Action 具体消息执行时需要加上?Invoke 判断是否为空 为了节省性能初始化不会分配事件初始值
/// </summary>
public class EntityEvent : EntityBaseComponent
{
    /// <summary>
    /// 实体数据初始化完成 已经可以使用了
    /// </summary>
    public Action EntityDataInitFinish;
    /// <summary>
    /// 实体重生
    /// </summary>
    public Action EntityBeReborn;

    /// <summary>
    /// 碰撞盒加载完成 T0：碰撞盒所在GameObject
    /// </summary>
    public Action<GameObject> ColliderLoadFinish;

    #region  移动相关事件

    /// <summary>
    /// 主动移动开始 不包括强制位移
    /// </summary>
    public Action MoveStart;
    /// <summary>
    /// 主动移动结束 不包括强制位移
    /// </summary>
    public Action MoveStop;
    /// <summary>
    /// 输入的移动路径变化了 T0:path
    /// </summary>
    public Action InputMovePathChanged;

    /// <summary>
    /// 实体路径移动到达目标点 中途停止不会广播 只有通过实体本身路径移动才会广播 单纯使用通用移动脚本直接移动的不会广播
    /// </summary>
    public Action OnEntityPathMoveArrived;
    /// <summary>
    /// 受击移动 T0:持续时间
    /// </summary>
    public Action<int> EntityBeHitMove;
    /// <summary>
    /// 实体接受到眩晕效果
    /// </summary>
    public Action EntityReceiveStunEffect;
    /// <summary>
    /// 实体移除眩晕效果
    /// </summary>
    public Action EntityRemoveStunEffect;
    /// <summary>
    /// 非移动状态的特殊移动开始 往往是技能效果的强制移动等触发
    /// </summary>
    public Action SpecialMoveStartNotMoveStatus;
    /// <summary>
    /// 实体受到伤害
    /// T0：伤害来源 T1：伤害值
    /// </summary>
    public Action<long, int> EntityBattleAddDamage;

    #endregion

    #region 战斗相关事件

    /// <summary>
    /// 输入技能释放 发送消息前需要自行检查配置 后面不会再重复检查 T0:技能输入数据
    /// </summary>
    public Action<InputSkillReleaseData> InputSkillRelease;
    /// <summary>
    /// 尝试停止持续技能的持续状态
    /// </summary>
    public Action TryStopHoldSkill;
    /// <summary>
    /// 进入技能前摇 T0:技能配置
    /// </summary>
    public Action<DRSkill> EnterSkillForward;
    /// <summary>
    /// 离开技能前摇 T0:是否是被打断离开 不是打断代表自动进入后续状态
    /// </summary>
    public Action<bool> ExitSkillForward;
    /// <summary>
    /// 进入技能后摇释放 T0:技能配置
    /// </summary>
    public Action<DRSkill> EnterSkillCast;
    /// <summary>
    /// 离开技能后摇释放
    /// </summary>
    public Action ExitSkillCast;

    /// <summary>
    /// 技能释放命中 技能输入数据
    /// </summary>
    public Action<InputSkillReleaseData> SkillCastHit;

    /// <summary>
    /// 进入死亡态
    /// </summary>
    public Action EnterDeath;

    /// <summary>
    /// 实体属性更新  T0:属性类型 T1:更新后到属性值
    /// </summary>
    public Action<eAttributeType, int> EntityAttributeUpdate;

    /// <summary>
    /// 实体Avatar更新
    /// </summary>
    public Action EntityAvatarUpdated;

    /// <summary>
    /// 主动移动开始 不包括强制位移
    /// </summary>
    public Action<EntityBase> EntityTriggerEnter;
    /// <summary>
    /// 主动移动结束 不包括强制位移
    /// </summary>
    public Action<EntityBase> EntityTriggerExit;
    /// <summary>
    /// 给目标实体应用效果之前 T0:目标实体 T1:效果数据
    /// </summary>
    public Action<EntityBase, GameMessageCore.DamageEffect> BeforeGiveSkillEffect;
    /// <summary>
    /// 给目标实体应用效果之后 T0:目标实体 T1:效果数据
    /// </summary>
    public Action<EntityBase, GameMessageCore.DamageEffect> AfterGiveSkillEffect;

    /// <summary>
    /// 实体应用效果之前 T0:效果数据
    /// </summary>
    public Action<GameMessageCore.DamageEffect> BeforeApplySkillEffect;
    /// <summary>
    /// 实体应用效果之后 T0:效果数据
    /// </summary>
    public Action<GameMessageCore.DamageEffect> AfterApplySkillEffect;
    /// <summary>
    /// 效果球列表更新
    /// </summary>
    public Action SeListUpdated;

    /// <summary>
    /// 战斗所属ID更新
    /// </summary>
    public Action<long> BattleOwnerIDUpdate;

    /// <summary>
    /// 实体改变战斗状态  T0:是否战斗
    /// </summary>
    public Action<bool> ChangeIsBattle;

    #endregion
    #region 天赋树
    /// <summary>
    /// 参数1：新增技能列表 参数2：移除技能列表
    /// </summary>
    public Action<IEnumerable<int>, IEnumerable<int>> TalentSkillUpdated;
    public Action<IEnumerable<int>> TalentSkillInited;

    /// <summary>
    /// 实体技能添加  T0:技能ID
    /// </summary>
    public Action<int> EntitySkillAdd;
    /// <summary>
    /// 实体技能删除  T0:技能ID
    /// </summary>
    public Action<int> EntitySkillRemove;
    #endregion

    #region 捕获
    /// <summary>
    /// 实体被捕获
    /// </summary>
    public Action<long> EntityBeCaptured;

    /// <summary>
    /// 停止路径移动状态  T0:停止位置
    /// </summary>
    public Action<Vector3> InputMovePathMoveStop;
    #endregion

}