using System.Collections.Generic;
using UnityEngine;
public abstract class SkillShapeBase
{
    /// <summary>
    /// 技能目标层级
    /// </summary>
    protected int TargetLayer;
    /// <summary>
    /// 技能阻挡层级
    /// </summary>
    protected int BlockLayer;
    /// <summary>
    /// 技能范围锚点，作为检测是否命中目标射线的起点
    /// </summary>
    /// <returns></returns>
    protected Vector3 Anchor => GetAnchor();

    public void SetBlockLayer(int layer)
    {
        BlockLayer = layer;
    }

    public void SetBlockLayer(params string[] layerNames)
    {
        BlockLayer = LayerMask.GetMask(layerNames);
    }

    public void SetTargetLayer(int layer)
    {
        TargetLayer = layer;
    }

    public void SetTargetLayer(params string[] layerNames)
    {
        TargetLayer = LayerMask.GetMask(layerNames);
    }

    /// <summary>
    /// 检测范围内收到攻击的碰撞体
    /// </summary>
    /// <param name="targetLayer">技能目标层</param>
    /// <param name="blockLayer">技能阻挡曾</param>
    /// <returns></returns>
    public List<Collider> Check(int targetLayer, int blockLayer)
    {
        TargetLayer = targetLayer;
        BlockLayer = blockLayer;
        List<Collider> hitColliderList = new();
        Collider[] allColliders = CheckAll();
        if (allColliders != null && allColliders.Length > 0)
        {
            foreach (var collider in allColliders)
            {
                if (SkillUtil.CheckP2P(Anchor, collider.transform.position, BlockLayer))
                {
                    hitColliderList.Add(collider);
                }
            }
        }
        return hitColliderList;
    }

    /// <summary>
    /// 检测范围内收到攻击的碰撞体
    /// 这里没有传blockLayer,不考虑遮挡关系，直接返回所有碰撞器
    /// </summary>
    /// <param name="targetLayer"></param>
    /// <returns></returns>
    public Collider[] Check(int targetLayer)
    {
        return CheckAll();
    }

    protected abstract Collider[] CheckAll();
    public abstract Vector3 GetAnchor();
}