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
}