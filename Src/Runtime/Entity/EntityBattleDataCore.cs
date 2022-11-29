/*
 * @Author: xiang huan
 * @Date: 2022-09-13 17:26:26
 * @Description: 战斗数据
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/Entity/EntityBattleDataCore.cs
 * 
 */
using System.Collections.Generic;
using GameMessageCore;
using UnityGameFramework.Runtime;

/// <summary>
/// 实体上的战斗数据 双端通用的核心数据
/// </summary>
public class EntityBattleDataCore : EntityBaseComponent
{
    /// <summary>
    /// 当前血量
    /// </summary>
    /// <value></value>
    public int HP { get; protected set; }
    /// <summary>
    /// 最大血量
    /// </summary>
    /// <value></value>
    public int HPMAX { get; protected set; }
    /// <summary>
    /// 血量回复
    /// </summary>
    /// <value></value>
    public int HPRecovery { get; protected set; }
    /// <summary>
    /// 攻击力
    /// </summary>
    public int Att;
    /// <summary>
    /// 防御力
    /// </summary>
    public int Def;
    /// <summary>
    /// 普通攻击速度
    /// </summary>
    public int AttSpeed;
    /// <summary>
    /// 暴击率
    /// </summary>
    public int CritRate;
    /// <summary>
    /// 暴击伤害
    /// </summary>
    public int CritDmg;
    /// <summary>
    /// 命中率
    /// </summary>
    public int HitRate;
    /// <summary>
    /// miss率
    /// </summary>
    public int MissRate;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed;
    /// <summary>
    /// 等级
    /// </summary>
    public int Level;
    /// <summary>
    /// 经验
    /// </summary>
    public long Exp;
    /// <summary>
    /// 是否在战斗状态中
    /// </summary>
    /// <value></value>
    public bool IsInBattle { get; protected set; }
    /// <summary>
    /// 翻滚距离
    /// </summary>
    public float RollDistance;
    /// <summary>
    /// 死亡原因 只在hp<=0时有效
    /// </summary>
    /// <value></value>
    public DamageState DeathReason { get; private set; }
    /// <summary>
    /// 战斗状态map  <状态key，添加计数>
    /// </summary>
    private readonly Dictionary<BattleDefine.eBattleState, int> _battleStateMap = new();

    public virtual void SetHP(int hp)
    {
        HP = hp;

        if (hp > 0)
        {
            DeathReason = DamageState.Normal;
        }
    }

    /// <summary>
    /// 设置死亡原因 在死亡时如果有特殊原因必须设置 否则默认为被攻击死亡
    /// </summary>
    /// <param name="reason"></param>
    public void SetDeathReason(DamageState reason)
    {
        DeathReason = reason;
    }

    public virtual void SetHPMAX(int hpMax)
    {
        HPMAX = hpMax;
    }

    /// <summary>
    ///  改变战斗状态
    /// </summary>
    /// <param name="isInBattle"></param>
    /// <returns>改变成功或者失败 状态没变为失败 主要给子类覆写使用</returns>
    public virtual bool ChangeBattleStatus(bool isInBattle)
    {
        if (IsInBattle == isInBattle)
        {
            return false;
        }

        IsInBattle = isInBattle;
        return true;
    }

    /// <summary>
    /// 添加一个战斗状态
    /// </summary>
    /// <param name="key"> 状态key</param>
    /// <returns>/returns>
    public void AddBattleState(BattleDefine.eBattleState key)
    {
        if (_battleStateMap.TryGetValue(key, out int num))
        {
            _battleStateMap[key] = num + 1;
        }
        else
        {
            _battleStateMap.Add(key, 1);
        }
    }

    /// <summary>
    /// 删除一个战斗状态
    /// </summary>
    /// <param name="key"> 状态key</param>
    /// <returns>/returns>
    public void RemoveBattleState(BattleDefine.eBattleState key)
    {
        if (_battleStateMap.TryGetValue(key, out int num))
        {
            _battleStateMap[key] = num - 1;
            if (_battleStateMap[key] <= 0)
            {
                _ = _battleStateMap.Remove(key);
            }
        }
        else
        {
            Log.Error($"RemoveBattleState Not Find State = {key}");
        }
    }

    /// <summary>
    /// 是否存在战斗状态
    /// </summary>
    /// <param name="key"> 状态key</param>
    /// <returns>/returns>
    public bool HasBattleState(BattleDefine.eBattleState key)
    {
        return _battleStateMap.ContainsKey(key);
    }

    /// <summary>
    /// 是否存活
    /// </summary>
    public bool IsLive()
    {
        return HP > 0;
    }
}