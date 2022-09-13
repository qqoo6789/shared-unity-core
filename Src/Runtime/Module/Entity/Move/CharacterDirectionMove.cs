using UnityEngine;

/// <summary>
/// 依靠CharacterController控制角色直线运动
/// </summary>
public sealed class CharacterDirectionMove : DirectionMove
{
    [Header("是否使用重力")]
    public bool UseGravity = true;

    private CharacterController _controller;
    private bool _isAddColliderLoadEvent;
    private AutoGravity _autoGravity;

    protected override void Start()
    {
        base.Start();

        if (!TryGetComponent(out _controller))
        {
            //直接拿不到就要等待加载完成事件
            _isAddColliderLoadEvent = true;
            RefEntity.EntityEvent.ColliderLoadFinish += OnColliderLoadFinish;
        }

        _ = TryGetComponent(out _autoGravity);
    }

    private void OnDestroy()
    {
        if (_isAddColliderLoadEvent)
        {
            _isAddColliderLoadEvent = false;
            RefEntity.EntityEvent.ColliderLoadFinish -= OnColliderLoadFinish;
        }
    }

    private void OnColliderLoadFinish(GameObject go)
    {
        _controller = go.GetComponent<CharacterController>();
    }

    protected override void ApplyMotion(Vector3 motion)
    {
        if (UseGravity)
        {
            _ = _controller.SimpleMove(motion / Time.deltaTime);
        }
        else
        {
            _ = _controller.Move(motion);
        }
    }

    private void Update()
    {
        if (_controller == null)
        {
            return;
        }

        if (!CheckIsMove())
        {
            if (UseGravity)
            {
                _ = _controller.SimpleMove(Vector3.zero);//没有移动也需要设置为0 否则不会应用重力
            }
            return;
        }

        TickMove(Time.deltaTime);
    }

    public override void StartMove()
    {
        base.StartMove();

        if (_autoGravity != null)
        {
            _autoGravity.StopGravity();
        }
    }

    public override void StopMove()
    {
        if (_autoGravity != null)
        {
            _autoGravity.StartGravity();
        }

        base.StopMove();
    }

    protected override void FinishMove()
    {
        base.FinishMove();

        if (_autoGravity != null)
        {
            _autoGravity.StartGravity();
        }
    }
}