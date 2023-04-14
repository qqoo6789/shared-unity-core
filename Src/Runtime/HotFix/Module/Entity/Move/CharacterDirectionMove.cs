using UnityEngine;

/// <summary>
/// 依靠CharacterController控制角色直线运动
/// </summary>
public sealed class CharacterDirectionMove : DirectionMove
{
    private CharacterMoveCtrl _controller;

    protected override void Start()
    {
        base.Start();

        _controller = GetComponent<CharacterMoveCtrl>();
    }

    protected override void ApplyMotion(Vector3 motion)
    {
        _controller.SetMoveSpeed(motion / Time.deltaTime);
    }

    private void Update()
    {
        if (_controller == null)
        {
            return;
        }

        //这里主要是预览时需要用这个停止移动 正式代码其实走不到这种情况
        if (!CheckIsMove() && _controller.MoveSpeed != Vector3.zero)
        {
            _controller.StopMove();
            return;
        }

        TickMove(Time.deltaTime);
    }

    public override void StopMove()
    {
        _controller.StopMove();

        base.StopMove();
    }

    protected override void FinishMove()
    {
        base.FinishMove();

        _controller.StopMove();
    }
}