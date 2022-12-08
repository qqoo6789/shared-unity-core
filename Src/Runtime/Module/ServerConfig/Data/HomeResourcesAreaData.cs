using System.Numerics;
/*
 * @Author: xiang huan
 * @Date: 2022-12-06 10:19:06
 * @Description: 家园资源区域刷新
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/ServerConfig/Data/HomeResourcesAreaData.cs
 * 
 */
using System.Collections.Generic;

public enum eHomeResourcesAreaType : int
{
    empty,    //空地
    farmland, //农田
}

public class HomeResourcesAreaData : DataNodeBase
{
    public int Id;
    public eHomeResourcesAreaType AreaType;
    public Vector3 Scale;
    public int UpdateInterval;
    public List<HomeResourcesAreaPointData> PointList;
}