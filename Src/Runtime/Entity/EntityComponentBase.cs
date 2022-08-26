using UnityEngine;
/// <summary>
/// 自定义实体组件基类，所有自定义的实体组件都要继承这个类
/// 提供实体组件的基础接口
/// </summary>

/// <summary>
/// 获取组件所挂载的实体
/// </summary>
public class EntityComponentBase : MonoBehaviour
{
    private EntityBase _refEntity;
    /// <summary>
    /// 获取组件所挂载的实体的反引用
    /// </summary>
    /// <value></value>
    protected EntityBase RefEntity
    {
        get
        {
            if (_refEntity == null)
            {
                _refEntity = GFEntryCore.GetModule<EntityMgr<EntityBase, EntityFactory<EntityBase>>>().GetEntityWithRoot(gameObject);
            }

            return _refEntity;
        }
    }
}