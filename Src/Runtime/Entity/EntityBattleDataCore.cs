using System.Collections.Generic;
using UnityEngine;

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
    /// 等级
    /// </summary>
    public int Level;
    /// <summary>
    /// 是否在战斗状态中
    /// </summary>
    /// <value></value>
    public bool IsInBattle { get; protected set; }
    /// <summary>
    /// 是否无敌中
    /// </summary>
    /// <value></value>
    public bool IsInvincible { get; protected set; }
    /// <summary>
    /// 翻滚距离
    /// </summary>
    public float RollDistance;
    /// <summary>
    /// 战斗效果map  <效果key，添加计数>
    /// </summary>
    private Dictionary<BattleDefine.eBattleEffectKey, int> _battleEffectMap = new();

    public virtual void SetHP(int hp)
    {
        HP = hp;
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
    /// 添加一个战斗效果
    /// </summary>
    /// <param name="key"> 效果key</param>
    /// <returns>/returns>
    public void AddBattleEffect(BattleDefine.eBattleEffectKey key)
    {
        if (_battleEffectMap.TryGetValue(key, out int num))
        {
            _battleEffectMap[key] = num + 1;
        }
        else
        {
            _battleEffectMap.Add(key, 1);
        }
    }

    /// <summary>
    /// 删除一个战斗效果
    /// </summary>
    /// <param name="key"> 效果key</param>
    /// <returns>/returns>
    public void RemoveBattleEffect(BattleDefine.eBattleEffectKey key)
    {
        if (_battleEffectMap.TryGetValue(key, out int num))
        {
            _battleEffectMap[key] = num - 1;
            if (_battleEffectMap[key] <= 0)
            {
                _ = _battleEffectMap.Remove(key);
            }
        }
    }

    /// <summary>
    /// 删除一个战斗效果
    /// </summary>
    /// <param name="key"> 效果key</param>
    /// <returns>/returns>
    public bool HasBattleEffect(BattleDefine.eBattleEffectKey key)
    {
        return _battleEffectMap.ContainsKey(key);
    }

    /// <summary>
    /// 是否存活
    /// </summary>
    public bool IsLive()
    {
        return HP > 0;
    }
}