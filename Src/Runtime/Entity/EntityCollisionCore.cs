/*
 * @Author: xiang huan
 * @Date: 2022-08-26 14:25:46
 * @Description: 实体碰撞盒
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Entity/EntityCollisionCore.cs
 * 
 */
using UnityEngine;
public abstract class EntityCollisionCore : EntityBaseComponent
{
    public GameObject CollisionObject { get; private set; }
    public Collider BodyCollision { get; private set; }  //躯干碰撞盒
    /// <summary>
    /// 加载碰撞预制体
    /// </summary>
    public abstract void LoadCollision(string assetPath);

    /// <summary>
    /// 卸载碰撞预制体
    /// </summary>
    public virtual void UnloadCollision()
    {
        if (CollisionObject == null)
        {
            return;
        }
        Destroy(CollisionObject);
        CollisionObject = null;
        BodyCollision = null;
    }

    /// <summary>
    /// 初始化碰撞盒
    /// </summary>
    public virtual void Init(GameObject collisionObject)
    {
        if (collisionObject == null)
        {
            return;
        }
        CollisionObject = collisionObject;
        CollisionObject.transform.parent = RefEntity.GetTransform();
        CollisionObject.transform.localPosition = Vector3.zero;
        CollisionObject.transform.localRotation = Quaternion.identity;
        EntityReferenceData refData = CollisionObject.AddComponent<EntityReferenceData>();
        refData.SetEntity(RefEntity);
        BodyCollision = CollisionObject.GetComponent<Collider>();
    }
    private void OnDestroy()
    {
        UnloadCollision();
    }
}