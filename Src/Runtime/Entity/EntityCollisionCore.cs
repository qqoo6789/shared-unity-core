/*
 * @Author: xiang huan
 * @Date: 2022-08-26 14:25:46
 * @Description: 实体碰撞盒
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Entity/EntityCollisionCore.cs
 * 
 */
using EasyCharacterMovement;
using UnityEngine;
using UnityGameFramework.Runtime;

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
        //Destroy(CollisionObject);
        CollisionObject = null;
        BodyCollision = null;
    }

    //勿删
    // /// <summary>
    // /// 初始化碰撞盒
    // /// </summary>
    // public virtual void Init(GameObject collisionObject)
    // {
    //     if (collisionObject == null)
    //     {
    //         return;
    //     }
    //     CollisionObject = collisionObject;
    //     CollisionObject.transform.parent = RefEntity.GetTransform();
    //     CollisionObject.transform.localPosition = Vector3.zero;
    //     CollisionObject.transform.localRotation = Quaternion.identity;
    //     EntityReferenceData refData = CollisionObject.AddComponent<EntityReferenceData>();
    //     refData.SetEntity(RefEntity);
    //     BodyCollision = CollisionObject.GetComponent<Collider>();
    // }

    /// <summary>
    /// 初始化碰撞盒
    /// </summary>
    public virtual void Init(GameObject prefab)
    {
        if (prefab == null)
        {
            return;
        }

        //因为现在移动主要都是依赖角色控制器 但是角色控制器控制的移动只能是控制器所在对象 所以只能将预制件中的碰撞器参数复制到实体根上来移动 这是暂时折中办法
        if (!prefab.TryGetComponent(out CharacterMovement prefabCharacterMovement))
        {
            Log.Error($"not find CharacterMovement of collider prefab name:{RefEntity.BaseData.Id}");
            return;
        }

        //先手动创建移动碰撞的依赖组件
        _ = RefEntity.AddComponent<Rigidbody>();
        CapsuleCollider collider = RefEntity.AddComponent<CapsuleCollider>();

        CharacterMovement characterMovement = RefEntity.AddComponent<CharacterMovement>();
        characterMovement.radius = prefabCharacterMovement.radius;
        characterMovement.height = prefabCharacterMovement.height;
        // characterMovement.center = prefabCharacterMovement.center;
        characterMovement.slopeLimit = MoveDefine.MOVE_SLOPE_LIMIT;
        characterMovement.stepOffset = MoveDefine.MOVE_STEP_HEIGHT;
        characterMovement.collisionLayers = MLayerMask.MASK_SCENE_OBSTRUCTION;
        // characterMovement.skinWidth = MoveDefine.MOVE_SKIN_WIDTH;
        RefEntity.EntityRoot.layer = prefab.layer;//暂时直接直接赋值成碰撞盒的层

        CollisionObject = collider.gameObject;
        BodyCollision = collider;

        EntityReferenceData refData = RefEntity.AddComponent<EntityReferenceData>();
        refData.SetEntity(RefEntity);

        RefEntity.EntityEvent.ColliderLoadFinish?.Invoke(CollisionObject);
    }

    private void OnDestroy()
    {
        UnloadCollision();
    }
}