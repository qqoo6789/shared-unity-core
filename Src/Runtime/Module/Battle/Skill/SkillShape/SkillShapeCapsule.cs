using UnityEngine;

public class SkillShapeCapsule : SkillShapeBase
{
    private Vector3 _p1;
    private Vector3 _p2;
    private Vector3 _center;
    private float _radius;
    public SkillShapeCapsule(Vector3 p1, Vector3 p2, float radius)
    {
        _p1 = p1;
        _p2 = p2;
        _radius = radius;
        _center = (_p1 + _p2) / 2;
    }
    protected override Collider[] CheckAll()
    {
        return Physics.OverlapCapsule(_p1, _p2, _radius, TargetLayer);
    }

    public override Vector3 GetAnchor()
    {
        return _center;
    }
}