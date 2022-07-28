using UnityEngine;

/// <summary>
/// 执行方向移动 拿到方向 每帧执行 enable来控制脚本执行
/// </summary>
[RequireComponent(typeof(EntityInputData))]
public abstract class DirectionMove : MonoBehaviour
{
    protected EntityInputData InputData;

    /// <summary>
    /// 移动速度 u/s
    /// </summary>
    public float MoveSpeed = 5;

    protected virtual void Start()
    {
        InputData = GetComponent<EntityInputData>();
    }

    /// <summary>
    /// 走一步 需要在合适时机触发
    /// </summary>
    /// <param name="tickDelay"></param>
    protected void TickMove(float tickDelay)
    {
        if (!enabled)
        {
            return;
        }

        if (InputData.InputMoveDirection == null)
        {
            return;
        }

        Vector3 moveDir = InputData.InputMoveDirection.Value;
        moveDir.Normalize();
        moveDir *= MoveSpeed * tickDelay;
        transform.forward = moveDir;
        ApplyPosition(transform.position + moveDir);
    }

    /// <summary>
    /// 子类应用目标位置
    /// </summary>
    /// <param name="targetPos"></param>
    protected abstract void ApplyPosition(Vector3 targetPos);
}