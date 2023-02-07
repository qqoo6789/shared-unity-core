/*
 * @Author: xiang huan
 * @Date: 2022-08-26 14:25:46
 * @Description: 实体碰撞盒
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Entity/EntityCollisionCore.cs
 * 
 */
using CMF;
using UnityEngine;
using System.Collections.Generic;

public abstract class EntityCollisionCore : EntityBaseComponent
{
    public GameObject CollisionObject { get; private set; }
    public Collider BodyCollision { get; private set; }  //躯干碰撞盒

    public HashSet<long> EntityTriggerSet = new(); //触碰实体Map
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
        if (prefab.TryGetComponent(out Mover prefabMover))
        {
            //先手动创建移动碰撞的依赖组件
            _ = RefEntity.AddComponent<Rigidbody>();
            CapsuleCollider collider = RefEntity.AddComponent<CapsuleCollider>();

            Mover mover = RefEntity.AddComponent<Mover>();
            mover.SetColliderHeight(prefabMover.ColliderHeight);
            mover.SetColliderThickness(prefabMover.ColliderThickness);
            mover.SetColliderOffset(prefabMover.ColliderOffset);
            mover.SetStepHeightRatio(MoveDefine.MOVE_STEP_HEIGHT_RATIO);
            // characterMovement.center = prefabCharacterMovement.center;
            // characterMovement.slopeLimit = MoveDefine.MOVE_SLOPE_LIMIT;
            // characterMovement.stepOffset = MoveDefine.MOVE_STEP_HEIGHT;
            // characterMovement.collisionLayers = MLayerMask.MASK_SCENE_OBSTRUCTION;
            // characterMovement.skinWidth = MoveDefine.MOVE_SKIN_WIDTH;
            RefEntity.EntityRoot.layer = prefab.layer;//暂时直接直接赋值成碰撞盒的层

            CollisionObject = collider.gameObject;
            BodyCollision = collider;
            // Log.Error($"not find CharacterMovement of collider prefab name:{RefEntity.BaseData.Id}");
        }
        else
        {
            // 静态实体
            CollisionObject = GameObject.Instantiate(prefab);
            CollisionObject.transform.parent = RefEntity.GetTransform();
            CollisionObject.transform.localPosition = Vector3.zero;
            CollisionObject.transform.localRotation = Quaternion.identity;
            BodyCollision = CollisionObject.GetComponent<Collider>();
        }



        EntityReferenceData refData = RefEntity.AddComponent<EntityReferenceData>();
        refData.SetEntity(RefEntity);

        if (RefEntity.TryGetComponent(out RoleBaseDataCore roleData))
        {
            roleData.SetHeight(prefabMover.ColliderHeight);
            roleData.SetRadius(prefabMover.ColliderThickness * 0.5f);
        }

        RefEntity.EntityEvent.ColliderLoadFinish?.Invoke(CollisionObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer & MLayerMask.ENTITY_TRIGGER) <= 0)
        {
            return;
        }

        EntityBase entity = GFEntryCore.GetModule<IEntityMgr>().GetEntityWithRoot<EntityBase>(other.gameObject);
        if (entity != null && entity.Inited)
        {
            if (!EntityTriggerSet.Contains(entity.BaseData.Id))
            {
                _ = EntityTriggerSet.Add(entity.BaseData.Id);
            }
            RefEntity.EntityEvent.EntityTriggerEnter?.Invoke(entity);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.layer & MLayerMask.ENTITY_TRIGGER) <= 0)
        {
            return;
        }
        EntityBase entity = GFEntryCore.GetModule<IEntityMgr>().GetEntityWithRoot<EntityBase>(other.gameObject);
        if (entity != null && entity.Inited)
        {
            _ = EntityTriggerSet.Remove(entity.BaseData.Id);
            RefEntity.EntityEvent.EntityTriggerExit?.Invoke(entity);
        }
    }
    private void OnDestroy()
    {
        UnloadCollision();
    }
}