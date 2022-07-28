using UnityEngine;

/// <summary>
/// 普通的方向移动
/// </summary>
public class NormalDirectionMove : DirectionMove
{
    private void Update()
    {
        TickMove(Time.deltaTime);
    }

    protected override void ApplyPosition(Vector3 targetPos)
    {
        transform.position = targetPos;
    }
}