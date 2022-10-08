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
    /// 角色穿着数据字典
    /// </summary>
    /// <value></value>
    public Dictionary<AvatarPosition, int> AvatarDic { get; protected set; } = new();

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
        AvatarDic.Clear();
        foreach (PlayerAvatar avatar in avatars)
        {
            AvatarDic[avatar.Position] = avatar.ObjectId;
        }
    }

    /// <summary>
    /// 获取角色最终的外观数据
    /// </summary>
    /// <returns></returns>
    public List<int> GetRoleFinalAvatars()
    {
        List<int> finalAvatars = new()
        {
            GetWearAvatarPart(AvatarPosition.Head) == 0?  RoleFeature.Hair : GetWearAvatarPart(AvatarPosition.Head),
            GetWearAvatarPart(AvatarPosition.Hand) == 0?  RoleFeature.Glove : GetWearAvatarPart(AvatarPosition.Hand),
            GetWearAvatarPart(AvatarPosition.Coat) == 0?  RoleFeature.Clothes : GetWearAvatarPart(AvatarPosition.Coat),
            GetWearAvatarPart(AvatarPosition.Pant) == 0?  RoleFeature.Pants : GetWearAvatarPart(AvatarPosition.Pant),
        };
        if (GetWearAvatarPart(AvatarPosition.Weapon) != 0)
        {
            finalAvatars.Add(GetWearAvatarPart(AvatarPosition.Weapon));
        }
        return finalAvatars;
    }

    public int GetWearAvatarPart(AvatarPosition position)
    {
        if (AvatarDic.TryGetValue(position, out int avatarID))
        {
            return avatarID;
        }

        return 0;
    }
}