using GameMessageCore;
/// <summary>
/// 动物基础数据 用于数据管理面板的数据
/// </summary>
[System.Serializable]
public class AnimBaseData
{
    /// <summary>
    /// 动物全局Id 和存档数据id一致
    /// </summary>
    public ulong AnimId;
    /// <summary>
    /// 玩家取得名字
    /// </summary>
    public string Name;
    /// <summary>
    /// 动物配置id
    /// </summary>
    public int Cid;
    /// <summary>
    /// 好感度数值
    /// </summary>
    public int Favorability;

    public AnimBaseData()
    {

    }

    public AnimBaseData(ProxyAnimalBaseData data)
    {
        AnimId = data.AnimId;
        Name = data.Name;
        Cid = data.Cid;
    }

    public ProxyAnimalBaseData ToProxyAnimalBaseData()
    {
        return new ProxyAnimalBaseData()
        {
            AnimId = AnimId,
            Name = Name,
            Cid = Cid,
        };
    }
}