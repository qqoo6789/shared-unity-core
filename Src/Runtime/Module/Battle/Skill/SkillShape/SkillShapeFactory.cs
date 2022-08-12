/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:49:14
 * @Description: 技能碰撞检测生成工厂
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Battle/Skill/SkillShape/SkillShapeFactory.cs
 * 
 */
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;


public class SkillShapeFactory
{
    public delegate SkillShapeBase CreateSkillShape(int[] parameters, EntityBase entity, Vector3 dir);
    protected static Dictionary<BattleDefine.eSkillShapeId, CreateSkillShape> SkillShapeMap = new()
    {
        {BattleDefine.eSkillShapeId.SkillShapeBox, CreateSkillShapeBox},
        {BattleDefine.eSkillShapeId.SkillShapeSphere, CreateSkillShapeSphere},
        {BattleDefine.eSkillShapeId.SkillShapeCapsule, CreateSkillShapeCapsule},
    };

    /// <summary>
    /// 创建技能形状
    /// </summary>
    /// <returns></returns>
    public static SkillShapeBase CreateOneSkillShape(int[] parameters, EntityBase entity, Vector3 dir)
    {
        if (parameters == null || parameters.Length < 1)
        {
            Log.Error($"CreateOneSkillShape Error parameters is Unknown");
            return null;
        }

        int shapeID = parameters[0];
        if (!SkillShapeMap.ContainsKey((BattleDefine.eSkillShapeId)shapeID))
        {
            Log.Error($"CreateOneSkillShape Error shapeID is Unknown shapeID = {shapeID}");
            return null;
        }
        SkillShapeBase shape = null;

        try
        {
            CreateSkillShape func = SkillShapeMap[(BattleDefine.eSkillShapeId)shapeID];
            shape = func.Invoke(parameters, entity, dir);
        }
        catch (System.Exception)
        {
            Log.Error($"CreateOneSkillShape Create Error shapeID = {shapeID}");
        }

        return shape;
    }

    private static SkillShapeBase CreateSkillShapeBox(int[] parameters, EntityBase entity, Vector3 dir)
    {
        if (parameters.Length < 4)
        {
            return null;
        }

        float xHalf = parameters[1] * MathUtil.CM2M / 2; //长
        float zHalf = parameters[2] * MathUtil.CM2M / 2; //宽
        float yHalf = parameters[3] * MathUtil.CM2M / 2; //高
        Vector3 halfSize = new(xHalf, yHalf, zHalf);
        Vector3 forward = dir != Vector3.zero ? dir : entity.Transform.forward;
        Vector3 moveDir = forward.normalized * xHalf;
        Vector3 anchor = entity.Transform.position;
        Vector3 centerPos = anchor + moveDir;
        Quaternion rotation = entity.Transform.rotation;


        SkillShapeBox shape = SkillShapeBase.Create<SkillShapeBox>();
        shape.Init(centerPos, halfSize, rotation, anchor);
        return shape;
    }

    private static SkillShapeBase CreateSkillShapeSphere(int[] parameters, EntityBase entity, Vector3 dir)
    {
        if (parameters.Length < 2)
        {
            return null;
        }

        float radius = parameters[1] * MathUtil.CM2M; //半径
        Vector3 centerPos = entity.Transform.position;
        SkillShapeSphere shape = SkillShapeBase.Create<SkillShapeSphere>();
        shape.Init(centerPos, radius);
        return shape;
    }
    private static SkillShapeBase CreateSkillShapeCapsule(int[] parameters, EntityBase entity, Vector3 dir)
    {

        float height = parameters[1] * MathUtil.CM2M; //高度
        float radius = parameters[2] * MathUtil.CM2M; //半径
        Vector3 centerPos = entity.Transform.position;
        Vector3 pos1 = centerPos - (height / 2 * Vector3.up);
        Vector3 pos2 = centerPos + (height / 2 * Vector3.up);

        SkillShapeCapsule shape = SkillShapeBase.Create<SkillShapeCapsule>();
        shape.Init(pos1, pos2, radius, centerPos);
        return shape;
    }
}