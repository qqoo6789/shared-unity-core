using UnityEngine;

/// <summary>
/// 刚体方向移动
/// </summary>
public class RigidbodyDirectionMove : DirectionMove
{
    public Rigidbody RefRigid;
    public Vector3 PushDownForce = Vector3.zero;// Vector3.down* 20f;//下压力 防止弹起 //TODO:现在场景没有地表碰撞 不能加向下力 否则一直往下掉

    private void FixedUpdate()
    {
        RefRigid.AddForce(PushDownForce, ForceMode.Force);//持续下压 否则在持续碰撞下会逐渐上移 暂时没有使用重力

        TickMove(Time.fixedDeltaTime);
    }

    protected override void ApplyPosition(Vector3 targetPos)
    {
        RefRigid.MovePosition(targetPos);
    }
}