/** 
 * @Author XQ
 * @Date 2022-08-09 09:51:55
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Data/MonsterDataCore.cs
 */
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 怪物基础数据
/// </summary>
public class MonsterDataCore : EntityBaseComponent
{
    public int configId => DRMonster == null ? -1 : DRMonster.Id;
    public DRMonster DRMonster { get; protected set; }

    public void SetMonsterConfigID(int cfgID)
    {
        DRMonster = GFEntryCore.DataTable.GetDataTable<DRMonster>().GetDataRow(cfgID);

        if (DRMonster == null)
        {
            Log.Error($"Can not find monster cfg id:{cfgID}");
        }
    }
}