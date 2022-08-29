/** 
 * @Author XQ
 * @Date 2022-08-09 10:25:20
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Move/DirectionMove.cs
 */
using UnityEngine;

/// <summary>
/// 执行方向移动 拿到方向 每帧执行 需要子类来驱动具体执行
/// </summary>
[RequireComponent(typeof(EntityInputData))]
public abstract class DirectionMove : EntityBaseComponent
{
    protected EntityInputData InputData;

    /// <summary>
    /// 移动速度 u/s
    /// </summary>
    public float MoveSpeed = 5;
    /// <summary>
    /// 正在移动
    /// </summary>
    /// <value></value>
    protected bool IsMoving { get; private set; } = true;//默认true 让美术预览时自动移动

    protected virtual void Start()
    {
        InputData = GetComponent<EntityInputData>();
    }

    /// <summary>
    /// 改变移动状态
    /// </summary>
    /// <param name="move"></param>
    public void ChangeMoveStatus(bool move)
    {
        if (IsMoving == move)
        {
            return;
        }

        IsMoving = move;
    }

    /// <summary>
    /// 走一步 需要在合适时机触发
    /// </summary>
    /// <param name="tickDelay"></param>
    protected void TickMove(float tickDelay)
    {
        if (!CheckIsMove())
        {
            return;
        }

        Vector3 moveDir = InputData.InputMoveDirection.Value;
        moveDir.Normalize();
        moveDir *= MoveSpeed * tickDelay;
        transform.forward = moveDir;
        ApplyMotion(moveDir);
    }

    /// <summary>
    /// 检查是否在移动
    /// </summary>
    /// <returns></returns>
    protected bool CheckIsMove()
    {
        return enabled && IsMoving && InputData.InputMoveDirection != null;
    }

    /// <summary>
    /// 子类应用目标移动增量 每帧的增量
    /// </summary>
    /// <param name="motion">本帧的移动增量</param>
    protected abstract void ApplyMotion(Vector3 motion);
}