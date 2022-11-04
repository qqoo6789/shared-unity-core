/** 
 * @Author XQ
 * @Date 2022-08-09 09:51:55
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/PlayerRoleDataCore.cs
 */
using System.Collections.Generic;
using GameMessageCore;
using UnityGameFramework.Runtime;

/// <summary>
/// 玩家场景角色数据
/// </summary>
public class PlayerRoleDataCore : EntityBaseComponent
{
    /// <summary>
    /// 性别
    /// </summary>
    /// <value></value>
    public string Gender { get; protected set; }

    /// <summary>
    /// 角色配置ID
    /// </summary>
    /// <value></value>
    public int RoleCfgID => DRRole == null ? -1 : DRRole.Id;

    /// <summary>
    /// 角色配置表
    /// </summary>
    /// <value></value>
    public DRRole DRRole { get; protected set; }

    /// <summary>
    /// 外观数据
    /// </summary>
    /// <value></value>
    public PlayerFeature RoleFeature { get; protected set; }

    /// <summary>
    /// 角色穿着数据
    /// 用字典方便业务层使用
    /// </summary>
    /// <value></value>
    public Dictionary<AvatarPosition, PlayerAvatar> WearDic { get; protected set; } = new();

    public void SetGender(string gender)
    {
        Gender = gender;
    }

    public void SetRoleCfgID(int roleCfgID)
    {
        DRRole = GFEntryCore.DataTable.GetDataTable<DRRole>().GetDataRow(roleCfgID);

        if (DRRole == null)
        {
            Log.Error($"Can not find role cfg id:{roleCfgID}");
        }
    }

    public void SetRoleFeature(PlayerFeature feature)
    {
        RoleFeature = feature;
    }

    public void SetRoleAvatars(IEnumerable<PlayerAvatar> avatars)
    {
        WearDic.Clear();
        foreach (PlayerAvatar avatar in avatars)
        {
            WearDic.Add(avatar.Position, avatar);
        }
    }
}