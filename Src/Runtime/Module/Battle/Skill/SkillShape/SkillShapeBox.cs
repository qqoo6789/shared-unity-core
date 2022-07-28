using UnityEngine;

public class SkillShapeBox : SkillShapeBase
{
    private Vector3 _center;
    private Vector3 _halfSize;
    private Quaternion _rotation;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="center">box中心点</param>
    /// <param name="halfSize">中心点到box的xyz面的距离，主要如果要表达变长为10的立方体，这是halfSize的值应该是new Vector3(10,10,10)</param>
    /// <param name="rotation">box的旋转状态</param>
    public SkillShapeBox(Vector3 center, Vector3 halfSize, Quaternion rotation)
    {
        _center = center;
        _halfSize = halfSize;
        _rotation = rotation;
    }

    public SkillShapeBox(Vector3 center, Vector3 halfSize)
    {
        _center = center;
        _halfSize = halfSize;
        _rotation = Quaternion.identity;
    }

    protected override Collider[] CheckAll()
    {
        return Physics.OverlapBox(_center, _halfSize, Quaternion.identity, TargetLayer);
    }

    public override Vector3 GetAnchor()
    {
        return _center;
    }
}