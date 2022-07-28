using System.Collections.Generic;
using UnityEngine;
public class SkillHitSystem : MonoBehaviour
{
    public void CheckSkill(SkillShapeBase shape, int targetLayer, int blockLayer)
    {
        List<Collider> colliders = shape.Check(targetLayer, blockLayer);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);
        }
    }

    public void ProcessHitEntity()
    {

    }
}