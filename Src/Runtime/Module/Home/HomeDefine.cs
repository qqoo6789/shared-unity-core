using UnityEngine;

/// <summary>
/// 家园系统的定义
/// </summary>
public static class HomeDefine
{
    //TODO: 需要正式配置
    public const int SOIL_X_NUM = 10;
    public const int SOIL_Z_NUM = 10;
    public static readonly Vector3 SOIL_SIZE = Vector3.one;

    //TODO:需要正式配置时间
    public const int SOIL_NEED_HOEING_EFFECT_VALUE = 100;//锄地需要的效果值
    public const int SOIL_HOEING_EFFECT_VALUE_LOST_SPEED = 20;//锄地效果值减少速度  每秒
    public const int SOIL_NEED_WATERING_EFFECT_VALUE = 100;//浇水需要的效果值
    public const int SOIL_WATERING_EFFECT_VALUE_LOST_SPEED = 10;//浇水效果值减少速度 每秒
    public const int SOIL_FROM_LOOSE_TO_IDLE_TIME = 20;//3 * 24 * 60 * 60 //土壤从松土到空白的时间 秒

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
        /// 已播种干涸
        /// </summary>
        SeedThirsty = 1 << 1,
        /// <summary>
        /// 生长中
        /// </summary>
        Growing = 1 << 2,
        /// <summary>
        /// 生长干涸
        /// </summary>
        GrowingThirsty = 1 << 3,
        /// <summary>
        /// 干枯
        /// </summary>
        Withered = 1 << 4,
        /// <summary>
        /// 等待收获
        /// </summary>
        Harvest = 1 << 5,
        /// <summary>
        /// 腐烂收获（播种专精不够）
        /// </summary>
        RotHarvest = 1 << 6,
    }

    /// <summary>
    /// 家园系统的玩家动作
    /// </summary>
    public enum eAction
    {
        None = 0,
        /// <summary>
        /// 锄头锄地
        /// </summary>
        Hoeing = 1 << 1,
        /// <summary>
        /// 播种
        /// </summary>
        Sowing = 1 << 2,
        /// <summary>
        /// 浇水
        /// </summary>
        Watering = 1 << 3,
        /// <summary>
        /// 收获
        /// </summary>
        Harvest = 1 << 4,
        /// <summary>
        /// 施肥
        /// </summary>
        Manure = 1 << 5,
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