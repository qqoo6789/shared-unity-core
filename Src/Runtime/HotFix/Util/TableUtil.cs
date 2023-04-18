using UnityGameFramework.Runtime;
/// <summary>
/// 通用配置表工具类
/// </summary>
public static class TableUtil
{
    /// <summary>
    /// 配置中的家园动作数组转实际枚举 配置中的是左移位数
    /// </summary>
    /// <param name="drAction"></param>
    /// <returns></returns>
    public static HomeDefine.eAction ToHomeAction(int[] drAction)
    {
        if (drAction == null || drAction.Length == 0)
        {
            return HomeDefine.eAction.None;
        }

        HomeDefine.eAction action = HomeDefine.eAction.None;
        foreach (int item in drAction)
        {
            action |= ToHomeAction(item);
        }
        return action;
    }

    /// <summary>
    /// 配置中的家园动作转实际枚举 配置中的是左移位数
    /// </summary>
    /// <param name="drAction"></param>
    /// <returns></returns>
    public static HomeDefine.eAction ToHomeAction(int drAction)
    {
        if (drAction == 0)
        {
            return HomeDefine.eAction.None;
        }

        return (HomeDefine.eAction)(1 << drAction);
    }

    /// <summary>
    /// 配置表中的字符串格式化输入 xxx{0}bbbb{1}
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string StringFormat(string format, params object[] args)
    {
        try
        {
            string res = string.Format(format, args);
            return res;
        }
        catch (System.Exception e)
        {
            Log.Error($"table StringFormat error format = {format} args = {args} e = {e}");
            return format;
        }
    }

    public static string GetLanguage(int id)
    {
        DRLanguage drLanguage = GFEntryCore.DataTable.GetDataTable<DRLanguage>().GetDataRow(id);
        if (drLanguage == null)
        {
            Log.Error($"GetLanguage DRLanguage is null id = {id}");
            return $"#{id}";
        }
        return drLanguage.Value;
    }

    public static DRGameValue GetGameValue(eGameValueID id)
    {
        DRGameValue drGameValue = GFEntryCore.DataTable.GetDataTable<DRGameValue>().GetDataRow((int)id);
        if (drGameValue == null)
        {
            Log.Error($"GetGameValue DRGameValue is null id = {id}");
            throw new System.Exception($"GetGameValue not find id = {id}");
        }
        return drGameValue;
    }
}