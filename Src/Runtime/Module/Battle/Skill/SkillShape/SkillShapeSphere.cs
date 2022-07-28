using UnityEngine;

public class SkillShapeSphere : SkillShapeBase
{
    private Vector3 _center;
    private float _radius;

    public SkillShapeSphere(Vector3 center, float radius)
    {
        _center = center;
        _radius = radius;
    }

    protected override Collider[] CheckAll()
    {
        return Physics.OverlapSphere(_center, _radius, TargetLayer);
    }

    public override Vector3 GetAnchor()
    {
        return _center;
    }
}