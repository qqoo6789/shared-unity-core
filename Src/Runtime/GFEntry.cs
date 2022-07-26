/*
 * @Author: xiang huan
 * @Date: 2022-07-26 15:38:17
 * @Description: 共享库GFEntry引用
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/GFEntry.cs
 * 
 */
using GameFramework.DataTable;

static internal class GFEntry
{
    /// <summary>
    /// 获取数据表组件。
    /// </summary>
    public static IDataTableManager DataTable
    {
        get;
        private set;
    }
    public static void SetDataTable(IDataTableManager dataTable)
    {
        DataTable = dataTable;
    }
}
