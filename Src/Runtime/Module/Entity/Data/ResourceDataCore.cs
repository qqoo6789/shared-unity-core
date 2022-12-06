/*
 * @Author: xiang huan
 * @Date: 2022-10-14 15:42:12
 * @Description: npc数据
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/ResourceDataCore.cs
 * 
 */

using UnityGameFramework.Runtime;

/// <summary>
/// 怪物基础数据
/// </summary>
public class ResourceDataCore : EntityBaseComponent
{
    public int ConfigId => DRHomeResources == null ? -1 : DRHomeResources.Id;
    public DRHomeResources DRHomeResources { get; protected set; }

    public ResourcesPointData PointData { get; protected set; }

    public void SetConfigID(int cfgID)
    {
        DRHomeResources = GFEntryCore.DataTable.GetDataTable<DRHomeResources>().GetDataRow(cfgID);

        if (DRHomeResources == null)
        {
            Log.Error($"Can not find DRHomeResources cfg id:{cfgID}");
        }
    }
}