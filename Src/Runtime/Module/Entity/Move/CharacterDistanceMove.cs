using System;
using UnityEngine;

/// <summary>
/// 使用角色控制器的距离移动
/// </summary>
public sealed class CharacterDistanceMove : DistanceMove
{
    [Header("是否使用重力")]
    public bool UseGravity = true;

    private CharacterController _controller;
    private bool _isAddColliderLoadEvent;

    private void Start()
    {
        if (!TryGetComponent(out _controller))
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

    private void OnColliderLoadFinish(GameObject go)
    {
        _controller = go.GetComponent<CharacterController>();
    }

    private void Update()
    {
        TickMove(Time.deltaTime);
    }

    protected override void ApplyMotion(Vector3 motion)
    {
        //可能没加载好碰撞器
        if (_controller == null)
        {
            return;
        }

        if (UseGravity)
        {
            //这里由于给的是速度 所以如果本来motion已经不足一帧位移 这里也会按照一帧来算 所以实际可能比预计多处小于一帧的距离 但问题不大
            _ = _controller.SimpleMove(motion.normalized * MoveSpeed);
        }
        else
        {
            _ = _controller.Move(motion);
        }
    }
}