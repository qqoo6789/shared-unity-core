using UnityEngine;
/// <summary>
/// 球形技能范围
/// </summary>
public class SkillShapeSphere : SkillShapeBase
{
    private Vector3 _center;
    private float _radius;

    public SkillShapeSphere(Vector3 center, float radius)
    {
        _center = center;
        _radius = radius;
        Anchor = center;
    }

    protected override Collider[] CheckAll(int targetLayer)
    {
        return Physics.OverlapSphere(_center, _radius, targetLayer);
    }
}