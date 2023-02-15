using System.Collections.Generic;
using GameFramework.DataTable;

public class EquipmentTable
{
    private readonly DREquipment[] _rows;
    private static EquipmentTable s_inst;
    public static EquipmentTable Inst
    {
        get
        {
            if (s_inst == null)
            {
                s_inst = new EquipmentTable();
            }
            return s_inst;
        }
    }

    private Dictionary<int, DREquipment> _dic { get; set; } = new();

    public EquipmentTable()
    {
        IDataTable<DREquipment> dt = GFEntryCore.DataTable.GetDataTable<DREquipment>();
        _rows = dt.GetAllDataRows();
        foreach (DREquipment row in _rows)
        {
            _dic.Add(row.ItemId, row);
        }
    }

    /// <summary>
    /// 通过装备的itemID获取装备的配置数据
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public DREquipment GetRowByItemID(int itemID)
    {
        if (_dic.ContainsKey(itemID))
        {
            return _dic[itemID];
        }

        return null;
    }
}