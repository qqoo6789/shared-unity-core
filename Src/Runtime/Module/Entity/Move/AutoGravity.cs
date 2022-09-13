using UnityEngine;

/// <summary>
/// 自动添加重力效果 但是由于会和其他移动叠加重力 需要在其他移动中关闭 除非后面完全我们自己控制重力
/// </summary>
public class AutoGravity : EntityBaseComponent
{
    private CharacterController _characterController;
    private bool _addColliderLoadEvent;
    private bool _isControllerFirstUpdate = true; //角色控制器是否第一次更新

    private void Start()
    {
        if (!RefEntity.TryGetComponent(out _characterController))
        {
            RefEntity.EntityEvent.ColliderLoadFinish += OnColliderLoadFinish;
            _addColliderLoadEvent = true;
        }
    }

    private void OnDestroy()
    {
        if (_addColliderLoadEvent)
        {
            RefEntity.EntityEvent.ColliderLoadFinish -= OnColliderLoadFinish;
            _addColliderLoadEvent = false;
        }
    }

    private void OnColliderLoadFinish(GameObject obj)
    {
        _characterController = obj.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController == null)
        {
            return;
        }

        if (_isControllerFirstUpdate)//需要等一帧 否则应用重力后会把坐标改到00坐标了 猜测是刚添加好角色控制器需要等待一帧执行他的start进行初始化
        {
            _isControllerFirstUpdate = false;
            return;
        }

        _characterController.SimpleMove(Vector3.zero);
    }

    /// <summary>
    /// 开始重力 会和其他角色控制器移动重力叠加 其他移动时这里应该关闭
    /// </summary>
    public void StartGravity()
    {
        enabled = true;
    }

    /// <summary>
    /// 关闭重力 其他移动时需要调用这里关闭重力叠加
    /// </summary>
    public void StopGravity()
    {
        enabled = false;
    }
}