/** 
 * @Author XQ
 * @Date 2022-08-09 09:51:55
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/RoleBaseDataCore.cs
 */

using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 实体角色基础数据
/// </summary>
public class RoleBaseDataCore : EntityBaseComponent
{
    public string Name { get; protected set; }
    /// <summary>
    /// 配置的高度 不参与缩放的原始高度
    /// </summary>
    /// <value></value>
    public float Height { get; private set; } = 1;
    /// <summary>
    /// 配置的半径 不参与缩放的原始半径
    /// </summary>
    /// <value></value>
    public float Radius { get; private set; } = 0.5f;

    /// <summary>
    /// 中心位置 世界坐标
    /// </summary>
    /// <returns></returns>
    public Vector3 CenterPos => transform.TransformPoint(new Vector3(0, Height * 0.5f, 0));

    /// <summary>
    /// 顶部位置 世界坐标
    /// </summary>
    /// <returns></returns>
    public Vector3 TopPos => transform.TransformPoint(new Vector3(0, Height, 0));

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetHeight(float height)
    {
        if (height < 0.01)
        {
            height = 0.01f;
        }

        Height = height;

        if (TryGetComponent(out NavMeshAgent agent))
        {
            agent.height = Height;
        }
    }

    public void SetRadius(float radius)
    {
        if (radius < 0.01)
        {
            radius = 0.01f;
        }

        Radius = radius;

        if (TryGetComponent(out NavMeshAgent agent))
        {
            agent.radius = radius;
        }
    }
}