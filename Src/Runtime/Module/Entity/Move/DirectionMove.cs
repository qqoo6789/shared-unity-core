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
public abstract class DirectionMove : EntityMoveBase
{
    protected EntityInputData InputData;

    /// <summary>
    /// 开启移动
    /// </summary>
    /// <value></value>
    private bool _enableMove = true;//默认true 让美术预览时自动移动

    protected virtual void Start()
    {
        InputData = GetComponent<EntityInputData>();
    }

    public override void StartMove()
    {
        base.StartMove();
        _enableMove = true;
    }

    public override void StopMove()
    {
        _enableMove = false;
        base.StopMove();
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
        return enabled && _enableMove && InputData.InputMoveDirection != null;
    }

    /// <summary>
    /// 子类应用目标移动增量 每帧的增量
    /// </summary>
    /// <param name="motion">本帧的移动增量</param>
    protected abstract void ApplyMotion(Vector3 motion);
}