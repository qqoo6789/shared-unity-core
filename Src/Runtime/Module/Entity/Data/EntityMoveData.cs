using UnityEngine;

/// <summary>
/// 实体移动数据
/// </summary>
public class EntityMoveData : EntityBaseComponent
{
    /// <summary>
    /// 移动速度 m/s
    /// </summary>
    public float Speed = 1;

    /// <summary>
    /// 着陆的 不是浮空的
    /// </summary>
    /// <value></value>
    public bool IsGrounded => _characterController != null && _characterController.isGrounded;

    private CharacterController _characterController;
    private bool _addColliderLoadEvent;

    private void Start()
    {
        if (!RefEntity.TryGetComponent(out _characterController))
        {
            _addColliderLoadEvent = true;
            RefEntity.EntityEvent.ColliderLoadFinish += OnColliderLoadFinish;
        }
    }


    private void OnDestroy()
    {
        if (_addColliderLoadEvent)
        {
            RefEntity.EntityEvent.ColliderLoadFinish -= OnColliderLoadFinish;
            _addColliderLoadEvent = false;
        }
    }

    private void OnColliderLoadFinish(GameObject collisionObject)
    {
        if (collisionObject == null)
        {
            return;
        }

        _characterController = collisionObject.GetComponent<CharacterController>();
    }
}