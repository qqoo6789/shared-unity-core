/** 
 * @Author XQ
 * @Date 2022-08-09 09:51:55
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/RoleBaseDataCore.cs
 */
using UnityEngine;

/// <summary>
/// 实体角色基础数据
/// </summary>
public class RoleBaseDataCore : EntityBaseComponent
{
    public string Name { get; protected set; }

    public void SetName(string name)
    {
        Name = name;
    }
}