/*
 * @Author: xiang huan
 * @Date: 2023-04-21 10:58:10
 * @Description: 实体网络数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/HotFix/Module/Entity/Data/EntityBaseNetDataCore.cs
 * 
 */
using GameMessageCore;

public class EntityBaseNetDataCore : EntityBaseComponent
{
    protected EntityWithLocation EntityWithLocation;
    /// <summary>
    /// 创建数据
    /// </summary>
    public virtual void CreateData()
    {
        EntityWithLocation = new();
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    public virtual void UpdateData()
    {

    }

    /// <summary>
    /// 获取数据
    /// </summary>
    public virtual EntityWithLocation GetData()
    {
        if (EntityWithLocation == null || EntityWithLocation.IsLock)
        {
            CreateData();
        }
        UpdateData();
        EntityWithLocation.IsLock = true;
        return EntityWithLocation;
    }

}