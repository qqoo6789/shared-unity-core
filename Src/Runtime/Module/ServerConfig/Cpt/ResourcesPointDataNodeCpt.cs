/*
 * @Author: xiang huan
 * @Date: 2022-06-27 14:13:48
 * @Description: 资源点数据组件
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/ServerConfig/Cpt/ResourcesPointDataNodeCpt.cs
 * 
 */
using UnityEngine;
public class ResourcesPointDataNodeCpt : MonoBehaviour, IServerDataNodeCpt
{
    [Tooltip("资源类型1. monster  2.复活点 3.npc")]
    [Header("资源类型")]
    public int ResourceType;
    [Header("配置ID")]
    public int ConfigId;
    [Header("刷新间隔时间(ms)")]
    public int UpdateInterval;
    [Header("刷新数量")]
    public int UpdateNum;
    [Header("刷新范围(半径)")]
    public float Radius;
    [Header("巡逻半径(m)")]
    public float PatrolRadius;
    [Header("巡逻速度")]
    public float PatrolSpd;
    [Header("巡逻路径")]
    public string PatrolPath;
    [Header("AI资源名字")]
    public string AIName;
    public object GetServerData()
    {
        ResourcesPointData data = new()
        {
            X = transform.position.x,
            Y = transform.position.y,
            Z = transform.position.z,
            ResourceType = ResourceType,
            ConfigId = ConfigId,
            UpdateInterval = UpdateInterval,
            UpdateNum = UpdateNum,
            Radius = Radius,
            PatrolRadius = PatrolRadius,
            PatrolSpd = PatrolSpd,
            PatrolPath = PatrolPath,
            AIName = AIName
        };
        return data;
    }
}