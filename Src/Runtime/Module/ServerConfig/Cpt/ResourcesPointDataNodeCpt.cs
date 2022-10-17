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
    [SerializeField]
    private int _resourceType;
    public int ResourceType => _resourceType;
    [Tooltip("配置 ID")]
    [SerializeField]
    private int _configId;
    public int ConfigId => _configId;

    [Tooltip("刷新间隔时间(毫秒)")]
    [SerializeField]
    private int _updateInterval;
    public int UpdateInterval => _updateInterval;
    [Tooltip("刷新数量")]
    [SerializeField]
    private int _updateNum;
    public int UpdateNum => _updateNum;
    [Tooltip("范围(半径)")]
    [SerializeField]
    private float _radius;
    public float Radius => _radius;

    [Tooltip("巡逻半径")]
    [SerializeField]
    private float _patrolRadius;
    public float PatrolRadius => _patrolRadius;

    [Tooltip("巡逻速度")]
    [SerializeField]
    private float _patrolSpd;
    public float PatrolSpd => _patrolSpd;

    [Tooltip("巡逻路径")]
    [SerializeField]
    private string _patrolPath;
    public string PatrolPath => _patrolPath;
    public object GetServerData()
    {
        ResourcesPointData data = new();
        data.X = transform.position.x;
        data.Y = transform.position.y;
        data.Z = transform.position.z;
        data.ResourceType = _resourceType;
        data.ConfigId = _configId;
        data.UpdateInterval = _updateInterval;
        data.UpdateNum = _updateNum;
        data.Radius = _radius;
        data.PatrolRadius = _patrolRadius;
        data.PatrolSpd = _patrolSpd;
        data.PatrolPath = _patrolPath;
        return data;
    }
}