/*
 * @Author: xiang huan
 * @Date: 2022-07-26 15:38:17
 * @Description: 共享库GFEntry引用
 * @FilePath: /Assets/Plugins/SharedCore/Src/Runtime/GFEntry.cs
 * 
 */
using GameFramework.DataTable;

public static class GFEntryCore
{
    /// <summary>
    /// 获取数据表组件。
    /// </summary>
    public static IDataTableComponent DataTable
    {
        get;
        private set;
    }
    public static void SetDataTable(IDataTableComponent dataTable)
    {
        DataTable = dataTable;
    }

    public static SkillEffectCoreFactory SkillEffectFactory
    {
        get;
        private set;
    }

    public static void SetSkillEffectFactory(SkillEffectCoreFactory skillEffectFactory)
    {
        SkillEffectFactory = skillEffectFactory;
    }
}
