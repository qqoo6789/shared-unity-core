using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 扇形技能检测范围
/// </summary>
public class SkillShapeFan : SkillShapeBase
{
    private float _radius;
    private float _angle;
    private float _height;
    private Vector3 _forward;
    public void Init(Vector3 center, float radius, float angle, float height, Vector3 forward)
    {
        _radius = radius;
        _angle = angle;
        _forward = forward;
        _forward.y = 0;
        Anchor = center;
        InitShape = true;
    }
    public override void DrawGizmos()
    {
        GizmosTools.DrawWireSemicircle(Anchor, _forward, _radius, _angle, Color.red);
    }

    protected override Collider[] CheckAll(int targetLayer)
    {
        Vector3 pTop = Anchor + new Vector3(0, _height / 2, 0);
        Vector3 pBottom = Anchor - new Vector3(0, _height / 2, 0);
        Collider[] colliders = Physics.OverlapCapsule(pTop, pBottom, _radius, targetLayer);

        List<Collider> result = new();
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider c = colliders[i];
            Vector3 toTarget = new Vector3(c.transform.position.x, Anchor.y, c.transform.position.z) - Anchor;
            if (Vector3.Angle(c.transform.position - Anchor, _forward) <= _angle / 2)
            {
                result.Add(c);
            }
        }

        return result.ToArray();
    }
}