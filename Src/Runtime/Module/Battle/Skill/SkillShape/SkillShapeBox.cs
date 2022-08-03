using UnityEngine;

/// <summary>
/// 盒形范围技能
/// </summary>
public class SkillShapeBox : SkillShapeBase
{
    private Vector3 _halfSize;
    private Quaternion _rotation;
    private Vector3 _center;


    /// <summary>
    /// 全参数构造函数
    /// </summary>
    /// <param name="center">box中心点</param>
    /// <param name="halfSize">中心点到box的xyz面的距离，主要如果要表达变长为10的立方体，这是halfSize的值应该是new Vector3(10,10,10)</param>
    /// <param name="rotation">box的旋转状态</param>
    /// <param name="anchor">技能锚点</param>
    public SkillShapeBox(Vector3 center, Vector3 halfSize, Quaternion rotation, Vector3 anchor)
    {
        _center = center;
        _halfSize = halfSize;
        _rotation = rotation;
        Anchor = anchor;
    }

    public SkillShapeBox(Vector3 center, Vector3 halfSize, Quaternion rotation)
    {
        _center = center;
        _halfSize = halfSize;
        _rotation = rotation;
        Anchor = center;
    }

    public SkillShapeBox(Vector3 center, Vector3 halfSize)
    {
        _center = center;
        _halfSize = halfSize;
        _rotation = Quaternion.identity;
        Anchor = center;
    }

    public SkillShapeBox(Vector3 center, Vector3 halfSize, Vector3 anchor)
    {
        _center = center;
        _halfSize = halfSize;
        _rotation = Quaternion.identity;
        Anchor = anchor;
    }

    protected override Collider[] CheckAll(int targetLayer)
    {
        return Physics.OverlapBox(_center, _halfSize, _rotation, targetLayer);
    }
}