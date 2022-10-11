using EasyCharacterMovement;
using UnityEngine;

/// <summary>
/// 用来控制角色控制器基础移动的基础移动组件 需要外部给出移动速度 自身有重力控制 业务层一般不直接使用 由CharacterDirectionMove和CharacterDistanceMove来控制移动速度
/// </summary>
public class CharacterMoveCtrl : EntityBaseComponent
{
    private const float AIR_FRICTION = 0f;//空中下落摩擦力

    private const float AIR_CONTROL = 0f;//空中下落时的水平移动控制系数

    private CharacterMovement _characterMovement;
    /// <summary>
    /// 当前带方向的移动速度
    /// </summary>
    /// <value></value>
    public Vector3 MoveSpeed { get; private set; }
    /// <summary>
    /// 在地面上 不是浮空状态
    /// </summary>
    public bool IsGrounded => _characterMovement != null && _characterMovement.isGrounded;
    private bool _isAddColliderLoadEvent;

    private void Start()
    {
        if (!TryGetComponent(out _characterMovement))
        {
            //直接拿不到就要等待加载完成事件
            _isAddColliderLoadEvent = true;
            RefEntity.EntityEvent.ColliderLoadFinish += OnColliderLoadFinish;
        }
    }

    private void OnDestroy()
    {
        if (_isAddColliderLoadEvent)
        {
            _isAddColliderLoadEvent = false;
            RefEntity.EntityEvent.ColliderLoadFinish -= OnColliderLoadFinish;
        }
    }

    private void FixedUpdate()
    {
        if (_characterMovement == null)
        {
            return;
        }

        if (_characterMovement.isGrounded)
        {
            GroundedMovement();
        }
        else
        {
            SkyMovement();
        }

        //前面设置好速度 这里开始移动
        _characterMovement.Move();
    }

    /// <summary>
    /// 地表移动
    /// </summary>
    private void GroundedMovement()
    {
        _characterMovement.velocity = MoveSpeed;
    }

    /// <summary>
    /// 空中移动 在空中 会应用重力和空中摩擦力系数
    /// </summary>
    private void SkyMovement()
    {
        Vector3 velocity = _characterMovement.velocity;

        //有外部移动 水平上根据系数调整速度
        if (MoveSpeed != Vector3.zero)
        {
            Vector3 horizontalVelocity = Vector3.MoveTowards(velocity.onlyXZ(), MoveSpeed.onlyXZ(), AIR_CONTROL * Time.deltaTime);
            velocity = horizontalVelocity + velocity.onlyY();
        }

        //加重力
        velocity += Physics.gravity * Time.deltaTime;

        //加上摩擦力
        velocity -= velocity * AIR_FRICTION * Time.deltaTime;

        _characterMovement.velocity = velocity;
    }

    /// <summary>
    /// 设置移动速度 有速度时就会移动 现在不允许多个业务同时控制 将来需要时需要在stop里面减去业务速度
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void SetMoveSpeed(Vector3 moveSpeed)
    {
        MoveSpeed = moveSpeed;
    }

    /// <summary>
    /// 停止移动 重力不受这里影响
    /// </summary>
    public void StopMove()
    {
        MoveSpeed = Vector3.zero;
    }

    private void OnColliderLoadFinish(GameObject go)
    {
        _characterMovement = go.GetComponent<CharacterMovement>();
    }
}