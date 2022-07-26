/*
 * @Author: xiang huan
 * @Date: 2022-07-26 15:53:11
 * @Description: 共享库GFEntry设置工具
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Utility/SharedGFEntryUtil.cs
 * 
 */



using GameFramework.DataTable;

public static class SharedGFEntryUtil
{
    public static void SetDataTable(IDataTableManager dataTable)
    {
        GFEntry.SetDataTable(dataTable);
    }
}
