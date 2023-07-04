using CMF;
using UnityEngine;

/// <summary>
/// 用来控制角色控制器基础移动的基础移动组件 需要外部给出移动速度 自身有重力控制 业务层一般不直接使用 由CharacterDirectionMove和CharacterDistanceMove来控制移动速度
/// </summary>
public class CharacterMoveCtrl : EntityBaseComponent
{
    private const float AIR_HORIZONTAL_FRICTION = 0.3f;//浮空时水平上的阻力 可以减少翻滚技能等的滑行距离 体验更加友好 每秒减少的比例

    private const float AIR_CONTROL = 3f;//空中下落时的水平移动控制系数 每秒减少的速度

    private Mover _mover;

    /// <summary>
    /// 当前带方向的移动速度
    /// </summary>
    /// <value></value>
    public Vector3 MoveSpeed { get; private set; }

    /// <summary>
    /// 当前带方向的物理移动速度
    /// </summary>
    /// <value></value>
    public Vector3 PhysicsMoveSpeed { get; private set; }
    /// <summary>
    /// 在地面上 不是浮空状态
    /// </summary>
    public bool IsGrounded => _mover != null && _mover.IsGrounded();
    private Vector3 _lastVelocity = Vector3.zero;
    private bool _enableGravity = true;

    private bool _isAddColliderLoadEvent;

    private bool _isPhysics;

    private bool _isMove = false;

    private Sensor.CastType? _specialCastType;//特殊的mover组件检测类型 因为某些时间需要用到特殊的检测类型提高精准度

    private void Start()
    {
        if (!TryGetComponent(out _mover))
        {
            //直接拿不到就要等待加载完成事件
            _isAddColliderLoadEvent = true;
            RefEntity.EntityEvent.ColliderLoadFinish += OnColliderLoadFinish;
        }
        else
        {
            RefreshSettingMover();
        }
        RefEntity.EntityEvent.SetPos += OnSetPosition;
    }

    private void OnDestroy()
    {
        if (_isAddColliderLoadEvent)
        {
            _isAddColliderLoadEvent = false;
            RefEntity.EntityEvent.ColliderLoadFinish -= OnColliderLoadFinish;
        }
        RefEntity.EntityEvent.SetPos -= OnSetPosition;
    }

    /// <summary>
    /// 设置特殊的检测类型 可以提高精准度 给null代表取消特殊设置
    /// </summary>
    /// <param name="castType"></param>
    public void SetSpecialCastType(Sensor.CastType? castType)
    {
        _specialCastType = castType;

        if (castType != null)
        {
            RefreshSettingMover();
        }
    }

    private void OnSetPosition(Vector3 pos)
    {
        //位置改变时 激活移动器
        if (!enabled)
        {
            enabled = true;
        }
    }
    private void FixedUpdate()
    {
        if (_mover == null)
        {
            return;
        }

        if (_specialCastType == null)
        {
            _mover.SimpleCheckForGround();
        }
        else
        {
            _mover.CheckForGround();
        }
        // bool _isSliding = _mover.IsGrounded() && IsGroundTooSteep();
        bool isGrounded = _mover.IsGrounded();
        Vector3 curSpeed;
        if (_enableGravity)
        {
            if (!_isPhysics)
            {
                if (_mover.IsGrounded())
                {
                    GroundedMovement();
                }
                else
                {
                    SkyMovement();
                }
                curSpeed = _lastVelocity;
            }
            else
            {

                PhysicsMovement();
                isGrounded = isGrounded && (PhysicsMoveSpeed.y <= 0);
                curSpeed = PhysicsMoveSpeed;
            }
        }
        else
        {
            GroundedMovement();
            curSpeed = _lastVelocity;
        }

        _mover.SetExtendSensorRange(isGrounded);
        // 给移动器正式应用速度
        _mover.SetVelocity(curSpeed);
        //如果在地面上 且没有移动速度 则返回，避免频繁调用地面检测函数
        if (_mover.IsGrounded() && !IsMove())
        {
            enabled = false;
            return;
        }

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

        //加重力
        velocity += Physics.gravity * Time.deltaTime;

        if (MoveDefine.ENABLE_MOVE_IN_AIR)
        {
            Vector3 horizontalVelocity = Vector3.MoveTowards(velocity.OnlyXZ(), MoveSpeed.OnlyXZ(), AIR_CONTROL * Time.deltaTime);

            velocity = horizontalVelocity + velocity.OnlyY();

            //加上水平摩擦力
            velocity -= AIR_HORIZONTAL_FRICTION * Time.deltaTime * velocity.OnlyXZ();
        }

        _lastVelocity = velocity;
    }

    /// <summary>
    /// 物理移动
    /// </summary>
    private void PhysicsMovement()
    {
        Vector3 velocity = PhysicsMoveSpeed;
        if (_mover.IsGrounded())
        {
            if (velocity.y <= 0)
            {
                velocity = Vector3.zero;
            }
        }
        else
        {
            velocity += Physics.gravity * Time.deltaTime;
        }

        PhysicsMoveSpeed = velocity;

        //物理运动结束
        if (PhysicsMoveSpeed.ApproximatelyEquals(Vector3.zero))
        {
            _isPhysics = false;
        }
    }

    // //Returns true if angle between controller and ground normal is too big (> slope limit), i.e. ground is too steep;
    // private bool IsGroundTooSteep()
    // {
    //     return !_mover.IsGrounded() || Vector3.Angle(_mover.GetGroundNormal(), Vector3.up) > MoveDefine.MOVE_SLOPE_LIMIT;
    // }

    /// <summary>
    /// 设置移动速度 有速度时就会移动 会标记为移动状态 现在不允许多个业务同时控制 将来需要时需要在stop里面减去业务速度
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void SetMoveSpeed(Vector3 moveSpeed)
    {
        _isMove = true;
        _isPhysics = false;
        MoveSpeed = moveSpeed;
        enabled = true;
    }

    /// <summary>
    /// 设置物理移动速度
    /// </summary>
    /// <param name="moveSpeed"></param>
    public void SetPhysicsMoveSpeed(Vector3 moveSpeed)
    {
        _isPhysics = true;
        PhysicsMoveSpeed = moveSpeed;
        enabled = true;
    }

    /// <summary>
    /// 停止移动 重力不受这里影响
    /// </summary>
    public void StopMove()
    {
        MoveSpeed = Vector3.zero;
        _isMove = false;
    }

    /// <summary>
    /// 设置是否启用重力 默认是启用的
    /// </summary>
    /// <param name="enable"></param>
    public void SetEnableGravity(bool enable)
    {
        _enableGravity = enable;
    }

    private void OnColliderLoadFinish(GameObject go)
    {
        _mover = go.GetComponent<Mover>();
        RefreshSettingMover();
    }

    private void RefreshSettingMover()
    {
        if (_mover == null)
        {
            return;
        }

        _mover.SetMoveCtrl(this);

        if (_specialCastType != null)
        {
            _mover.SetSensorCastType(_specialCastType.Value);
        }
    }

    /// <summary>
    /// 是否正在移动
    /// </summary>
    /// <returns></returns>
    public bool IsMove()
    {
        return _isMove || _isPhysics || _mover.IsMove;
    }
}
