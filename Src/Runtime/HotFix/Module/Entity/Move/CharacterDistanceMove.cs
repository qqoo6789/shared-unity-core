using UnityEngine;

/// <summary>
/// 使用角色控制器的距离移动
/// </summary>
public sealed class CharacterDistanceMove : DistanceMove
{
    private CharacterMoveCtrl _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterMoveCtrl>();
    }

    private void Update()
    {
        if (_controller == null)
        {
            return;
        }

        TickMove(Time.deltaTime);
    }

    protected override void ApplyMotion(Vector3 motion)
    {
        _controller.SetMoveSpeed(motion / Time.deltaTime);
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