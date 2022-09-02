/*
 * @Author: xiang huan
 * @Date: 2022-08-24 14:32:34
 * @Description: 技能范围绘制
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Battle/Skill/SkillShape/SkillShapeGizmos.cs
 * 
 */
using UnityEngine;
public class SkillShapeGizmos : MonoBehaviour
{
    private SkillShapeBase _shape;
    public void StartDraw(int[] parameters, GameObject entity, Vector3 dir)
    {
        if (_shape != null)
        {
            StopDraw();
        }
        Vector3 startPos;
        if (entity.TryGetComponent(out EntityCollisionCore entityCollision) && entityCollision.BodyCollision != null)
        {
            startPos = entityCollision.BodyCollision.bounds.center;
        }
        else
        {
            startPos = entity.transform.position;
        }
        _shape = SkillShapeFactory.CreateOneSkillShape(parameters, startPos, dir);
    }
    public void StopDraw()
    {
        if (_shape != null)
        {
            SkillShapeBase.Release(_shape);
            _shape = null;
        }
    }
    private void OnDrawGizmos()
    {
        if (_shape != null)
        {
            _shape.DrawGizmos();
        }
    }
}