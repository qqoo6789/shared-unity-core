using CMF;
using UnityEngine;

/// <summary>
/// 用来控制角色控制器基础移动的基础移动组件 需要外部给出移动速度 自身有重力控制 业务层一般不直接使用 由CharacterDirectionMove和CharacterDistanceMove来控制移动速度
/// </summary>
public class CharacterMoveCtrl : EntityBaseComponent
{
    private const float AIR_FRICTION = 0f;//空中下落摩擦力

    // private const float AIR_CONTROL = 0f;//空中下落时的水平移动控制系数

    private Mover _mover;

    /// <summary>
    /// 当前带方向的移动速度
    /// </summary>
    /// <value></value>
    public Vector3 MoveSpeed { get; private set; }
    /// <summary>
    /// 在地面上 不是浮空状态
    /// </summary>
    public bool IsGrounded => _mover != null && _mover.IsGrounded();
    private Vector3 _lastVelocity = Vector3.zero;

    private bool _isAddColliderLoadEvent;

    private void Start()
    {
        if (!TryGetComponent(out _mover))
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
        if (_mover == null)
        {
            return;
        }

        _mover.CheckForGround();

        // bool _isSliding = _mover.IsGrounded() && IsGroundTooSteep();

        if (_mover.IsGrounded())
        {
            GroundedMovement();
        }
        else
        {
            SkyMovement();
        }

        _mover.SetExtendSensorRange(_mover.IsGrounded());
        // 给移动器正式应用速度
        _mover.SetVelocity(_lastVelocity);
    }

    /// <summary>
    /// 地表移动
    /// </summary>
    private void GroundedMovement()
    {
        _lastVelocity = MoveSpeed;

        if (_lastVelocity.y <= 0f)
        {
            _lastVelocity.y = 0f;
        }
    }

    /// <summary>
    /// 空中移动 在空中 会应用重力和空中摩擦力系数
    /// </summary>
    private void SkyMovement()
    {
        Vector3 velocity = _lastVelocity;

        //这里在ECM2插件下是好的  在现在的CMF插件下会导致角色浮空容易瞬移到无穷远的地方 原因未知  功能现在不需要 先不搞
        // //有外部移动 水平上根据系数调整速度
        // if (MoveSpeed != Vector3.zero)
        // {
        //     Vector3 horizontalVelocity = Vector3.MoveTowards(velocity.OnlyXZ(), MoveSpeed.OnlyXZ(), AIR_CONTROL * Time.deltaTime);
        //     velocity = horizontalVelocity + velocity.OnlyY();
        // }

        //加重力
        velocity += Physics.gravity * Time.deltaTime;

        //加上摩擦力
        velocity -= AIR_FRICTION * Time.deltaTime * velocity;

        _lastVelocity = velocity;
    }

    //Returns true if angle between controller and ground normal is too big (> slope limit), i.e. ground is too steep;
    private bool IsGroundTooSteep()
    {
        return !_mover.IsGrounded() || Vector3.Angle(_mover.GetGroundNormal(), Vector3.up) > MoveDefine.MOVE_SLOPE_LIMIT;
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
        _mover = go.GetComponent<Mover>();
    }
}