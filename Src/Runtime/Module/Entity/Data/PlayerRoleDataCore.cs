/** 
 * @Author XQ
 * @Date 2022-08-09 09:51:55
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/PlayerRoleDataCore.cs
 */
using MelandGame3;
using UnityEngine;
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
    /// 玩家复活CD
    /// </summary>
    /// <value></value>
    public long ReviveCD { get; private set; } = 0;

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

    /// <summary>
    /// 设置复活CD
    /// </summary>
    public void SetReviveCD(long time)
    {
        ReviveCD = time;
    }
    /// <summary>
    /// 是否复活CD中
    /// </summary>
    public bool IsReviveCD()
    {
        long curTimeStamp = TimeUtil.GetTimeStamp();
        return ReviveCD > curTimeStamp;
    }
}