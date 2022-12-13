using UnityEngine;
using UnityGameFramework.Runtime;
/// <summary>
/// 区域入口检测器
/// </summary>

[ExecuteAlways]
public class SceneAreaEntryChecker : MonoBehaviour
{
    /// <summary>
    /// 区域标识
    /// </summary>
    public eSceneArea Area = eSceneArea.world;
    public eSceneAreaPriority Priority = eSceneAreaPriority.none;

    private void Start()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            //避免铺地图的时候忘记设置tag
            gameObject.tag = MTag.SCENE_AREA_CHECKER;
        }
#endif
        if (TryGetComponent(out Collider collider))
        {
            if (!collider.isTrigger)
            {
                Log.Warning("collider must be trigger");
                collider.isTrigger = true;
            }
        }
        else
        {
            Log.Error("SceneEntryChecker must have a collider component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != MLayerMask.PLAYER)
        {
            return;
        }

        EntityBase entity = GFEntryCore.GetModule<IEntityMgr>().GetEntityWithRoot<EntityBase>(other.gameObject);
        if (entity != null && entity.Inited)
        {
            OnPlayerEnterNewSceneArea(entity.BaseData.Id, Area);
        }
    }

    private void OnPlayerEnterNewSceneArea(long playerID, eSceneArea newArea)
    {
        Log.Info($"OnPlayerEnterNewScene,{gameObject.name} {playerID}, {newArea}");
        SceneAreaInfo info = new(newArea, Priority, GetHashCode());
        GFEntryCore.GetModule<SceneAreaMgr>().ReceiveAreaChangedEvent(new(eAreaChangedType.enter, playerID, info));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != MLayerMask.PLAYER)
        {
            return;
        }

        EntityBase entity = GFEntryCore.GetModule<IEntityMgr>().GetEntityWithRoot<EntityBase>(other.gameObject);
        if (entity != null && entity.Inited)
        {
            OnPlayerExitCurSceneArea(entity.BaseData.Id, Area);
        }
    }

    private void OnPlayerExitCurSceneArea(long playerID, eSceneArea newArea)
    {
        Log.Info($"OnPlayerExitCurScene, {gameObject.name} {playerID}, {newArea}");
        SceneAreaInfo info = new(newArea, Priority, GetHashCode());
        GFEntryCore.GetModule<SceneAreaMgr>().ReceiveAreaChangedEvent(new(eAreaChangedType.exit, playerID, info));
    }

#if UNITY_EDITOR
    public bool DrawArea = true;
    public Color AreaColor = Color.green;
    private void OnDrawGizmos()
    {
        if (!DrawArea)
        {
            return;
        }
        //根据碰撞盒子绘制区域
        Gizmos.color = AreaColor;
        if (TryGetComponent(out Collider collider))
        {
            if (collider is BoxCollider box)
            {
                Gizmos.DrawWireCube(transform.position + box.center, new Vector3(box.size.x * transform.lossyScale.x, box.size.y * transform.lossyScale.y, box.size.z * transform.lossyScale.z));
            }
            else if (collider is SphereCollider sphere)
            {
                Gizmos.DrawWireSphere(transform.position + sphere.center, sphere.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z));
            }
            else if (collider is CapsuleCollider capsule)
            {
                //to do
                // DrawCapsule(transform.position + capsule.center, capsule.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.z), capsule.height * transform.lossyScale.y, transform.rotation);
            }
            else if (collider is MeshCollider mesh)
            {
                Gizmos.DrawWireMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.lossyScale);
            }
        }
    }

    // 绘制胶囊体线框
    private void DrawCapsule(Vector3 center, float radius, float height, Quaternion rotation)
    {
        Vector3 point1 = center + (rotation * new Vector3(0, height * 0.5f, 0));
        Vector3 point2 = center + (rotation * new Vector3(0, -height * 0.5f, 0));
        Gizmos.DrawWireSphere(point1, radius);
        Gizmos.DrawWireSphere(point2, radius);
        Gizmos.DrawLine(point1 + (rotation * new Vector3(radius, 0, 0)), point2 + (rotation * new Vector3(radius, 0, 0)));
        Gizmos.DrawLine(point1 + (rotation * new Vector3(-radius, 0, 0)), point2 + (rotation * new Vector3(-radius, 0, 0)));
        Gizmos.DrawLine(point1 + (rotation * new Vector3(0, 0, radius)), point2 + (rotation * new Vector3(0, 0, radius)));
        Gizmos.DrawLine(point1 + (rotation * new Vector3(0, 0, -radius)), point2 + (rotation * new Vector3(0, 0, -radius)));
    }
#endif
}