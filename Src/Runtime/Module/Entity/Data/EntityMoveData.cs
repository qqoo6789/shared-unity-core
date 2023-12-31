
/// <summary>
/// 实体移动数据
/// </summary>
public class EntityMoveData : EntityBaseComponent
{
    /// <summary>
    /// 移动速度 m/s
    /// </summary>
    public float Speed = 1;

    /// <summary>
    /// 着陆的 不是浮空的
    /// </summary>
    /// <value></value>
    public bool IsGrounded => _characterController != null && _characterController.IsGrounded;

    private CharacterMoveCtrl _characterController;

    protected virtual void Start()
    {
        _characterController = RefEntity.GetComponent<CharacterMoveCtrl>();
    }
    /// <summary>
    /// 设置移动速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetMoveSpeed(float speed)
    {
        Speed = speed;
    }
}