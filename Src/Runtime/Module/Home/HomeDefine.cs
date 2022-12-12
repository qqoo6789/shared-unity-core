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
        Loose,
        /// <summary>
        /// 生长中
        /// </summary>
        Growing,
        /// <summary>
        /// 口渴
        /// </summary>
        Thirsty,
        /// <summary>
        /// 干枯
        /// </summary>
        Withered,
        /// <summary>
        /// 等待收获
        /// </summary>
        Harvest,
    }
}