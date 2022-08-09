using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
/// <summary>
/// 技能形状基类，所有的技能形状都应该继承自这个类
/// </summary>
public abstract class SkillShapeBase : IReference
{
    /// <summary>
    /// 子类实现shape的初始化时赋值为true
    /// </summary>
    protected bool InitShape = false;
    /// <summary>
    /// 子类需要按具体的形状来赋值
    /// </summary>
    protected Vector3 Anchor;
    /// <summary>
    /// 检测范围内收到攻击的碰撞体
    /// 可能返回null
    /// </summary>
    /// <param name="targetLayer">技能目标层</param>
    /// <param name="blockLayer">技能阻挡曾</param>
    /// <returns></returns>
    public List<Collider> CheckHited(int targetLayer, int blockLayer)
    {
        if (!InitShape)
        {
            Log.Error("SkillShapeBase.CheckHited() - shape not init,return null");
            return null;
        }

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
    /// 可能返null
    /// </summary>
    /// <param name="targetLayer"></param>
    /// <returns></returns>
    public Collider[] CheckHited(int targetLayer)
    {
        if (!InitShape)
        {
            Log.Error("SkillShapeBase.CheckHited() - shape not init,return null");
            return null;
        }

        return CheckAll(targetLayer);
    }

    /// <summary>
    /// 检测范围内targetLayer层的所有碰撞体
    /// </summary>
    /// <param name="targetLayer"></param>
    /// <returns></returns>
    protected abstract Collider[] CheckAll(int targetLayer);

    public void Clear()
    {
        InitShape = false;
    }

    /// <summary>
    /// 初始化技能形状
    /// 需要手动初始化技形状
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Create<T>() where T : SkillShapeBase, new()
    {
        T shape = ReferencePool.Acquire<T>();
        return shape;
    }

    /// <summary>
    /// 技能形状回池
    /// </summary>
    /// <param name="shape"></param>
    public void Release(SkillShapeBase shape)
    {
        ReferencePool.Release(shape);
    }
}