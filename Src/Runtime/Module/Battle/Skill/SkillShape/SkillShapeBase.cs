using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能形状基类，所有的技能形状都应该继承自这个类
/// </summary>
public abstract class SkillShapeBase
{
    protected Vector3 Anchor;
    /// <summary>
    /// 检测范围内收到攻击的碰撞体
    /// </summary>
    /// <param name="targetLayer">技能目标层</param>
    /// <param name="blockLayer">技能阻挡曾</param>
    /// <returns></returns>
    public List<Collider> CheckHited(int targetLayer, int blockLayer)
    {
        List<Collider> hitColliderList = new();
        Collider[] allColliders = CheckAll(targetLayer);
        if (allColliders != null && allColliders.Length > 0)
        {
            foreach (Collider collider in allColliders)
            {
                if (SkillUtil.CheckP2P(Anchor, collider.transform.position, blockLayer))
                {
                    hitColliderList.Add(collider);
                }
            }
        }
        return hitColliderList;
    }

    /// <summary>
    /// 检测范围内收到攻击的碰撞体
    /// 这里没有传blockLayer，不考虑遮挡关系，范围内所有碰撞体都会被检测到
    /// </summary>
    /// <param name="targetLayer"></param>
    /// <returns></returns>
    public Collider[] CheckHited(int targetLayer)
    {
        return CheckAll(targetLayer);
    }

    /// <summary>
    /// 检测范围内targetLayer层的所有碰撞体
    /// </summary>
    /// <param name="targetLayer"></param>
    /// <returns></returns>
    protected abstract Collider[] CheckAll(int targetLayer);
}