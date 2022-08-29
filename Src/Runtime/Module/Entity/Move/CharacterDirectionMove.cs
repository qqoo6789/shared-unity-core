using UnityEngine;

/// <summary>
/// 依靠CharacterController控制角色直线运动
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class CharacterDirectionMove : DirectionMove
{
    [Header("是否使用重力")]
    public bool UseGravity = true;

    private CharacterController _controller;

    protected override void Start()
    {
        base.Start();

        _controller = GetComponent<CharacterController>();
    }

    protected override void ApplyMotion(Vector3 motion)
    {
        if (UseGravity)
        {
            _controller.SimpleMove(motion / Time.deltaTime);
        }
        else
        {
            _controller.Move(motion);
        }
    }

    private void Update()
    {
        if (!CheckIsMove())
        {
            if (UseGravity)
            {
                _controller.SimpleMove(Vector3.zero);//没有移动也需要设置为0 否则不会应用重力
            }
            return;
        }

        TickMove(Time.deltaTime);
    }
}