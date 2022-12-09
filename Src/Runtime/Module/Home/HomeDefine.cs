using UnityEngine;

/// <summary>
/// 家园系统的定义
/// </summary>
public static class HomeDefine
{
    //TODO: 需要正式配置
    public const int SOIL_X_NUM = 10;
    public const int SOIL_Z_NUM = 10;
    public static readonly Vector3 SOIL_SIZE = Vector3.one;  //土地格子大小
    public static readonly Vector3 EMPTY_SIZE = Vector3.one; //空地格子大小

    /// <summary>
    /// 土地状态间的数据定义key
    /// </summary>
    public static class SoilStatusDataName
    {
        public const string IS_INIT_STATUS = "IS_INIT_STATUS";//标记是否是初始化状态
    }

    /// <summary>
    /// 土壤状态标记 需要和土地状态机每个状态对应
    /// </summary>
    public enum eSoilStatus
    {
        /// <summary>
        ///闲置空白
        /// </summary>
        Idle = 0,
        /// <summary>
        /// 松土完
        /// </summary>
        Loose = 1 << 0,
        /// <summary>
        /// 生长中
        /// </summary>
        Growing = 1 << 1,
        /// <summary>
        /// 口渴
        /// </summary>
        Thirsty = 1 << 2,
        /// <summary>
        /// 干枯
        /// </summary>
        Withered = 1 << 3,
        /// <summary>
        /// 等待收获
        /// </summary>
        Harvest = 1 << 4,
    }

    /// <summary>
    /// 可采集资源类型 对应不同武器可操作不同类型
    /// </summary>
    public enum eResourceType
    {
        Unknown = 0,
        /// <summary>
        /// 土壤作物
        /// </summary>
        Soil = 1 << 0,
        /// <summary>
        /// 岩石 矿石
        /// </summary>
        Rock = 1 << 1,
        Tree = 1 << 2,
        Grass = 1 << 3,
    }

    public enum eHomeResourcesAreaType : int
    {
        empty,    //空地
        farmland, //农田
    }
}