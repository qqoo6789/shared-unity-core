using UnityEngine;

/// <summary>
/// 家园系统的定义
/// </summary>
public static class HomeDefine
{
    public const int COLLECT_RESOURCE_DEATH_TIME = 10;//采集资源死亡时间 ms
    public const float PROGRESS_FULL_ANIM_TIME = 0.7f;//进度满了后的动画时间 需要等待 秒

    public static readonly Vector3 SOIL_SIZE = new(1.2f, 1.2f, 1.2f);  //土地格子大小
    public static readonly Vector3 EMPTY_SIZE = Vector3.one; //空地格子大小

    public const float HOME_PROGRESS_ACTION_BACK_PROTECT_TIME = 2000;//家园进度动作回退保护时间 ms

    public const int ACTION_MAX_PROGRESS_PROTECT = 100;//进度动作最大值的保护性值 防止异常报错 正常不会用到

    /// <summary>
    /// 需要消耗道具的动作集合
    /// </summary>
    public const eAction NEED_COST_ITEM_ACTION_MASK = eAction.Sowing | eAction.Manure | eAction.PutAnimalFood;
    /// <summary>
    /// 持续性进度动作集合 比如浇水
    /// </summary>
    public const eAction HOLD_PROGRESS_ACTION_MASK = eAction.Watering | eAction.Appease;
    /// <summary>
    /// 分段进度动作集合 比如锄地
    /// </summary>
    public const eAction SEGMENT_PROGRESS_ACTION_MASK = eAction.Hoeing | eAction.Mining | eAction.Cut | eAction.Shearing | eAction.Milking;
    /// <summary>
    /// 支持进度的动作集合
    /// </summary>
    public const eAction PROGRESS_ACTION_MASK = HOLD_PROGRESS_ACTION_MASK | SEGMENT_PROGRESS_ACTION_MASK;
    /// <summary>
    /// 需要计算伤害的动作集合
    /// </summary>
    public const eAction NEED_CALCULATE_DAMAGE_ACTION_MASK = SEGMENT_PROGRESS_ACTION_MASK ^ (eAction.Hoeing | eAction.Shearing | eAction.Milking);


    #region 畜牧动物

    public const string ANIMAL_AI_NAME = "HomeAnimalAI";

    /// <summary>
    /// 动物找吃的距离食盆的距离
    /// </summary>
    public const float ANIMAL_EAT_FOOD_DISTANCE = 2f;

    /// <summary>
    /// 畜牧动物死亡后能触发的对话的最大距离
    /// </summary>
    public const int ANIMAL_DEATH_DIALOG_TRIGGER_DISTANCE = 3;
    /// <summary>
    /// 动物死亡对话的对话名
    /// </summary>
    public const string ANIMAL_DEATH_DIALOG_CONVERSATION_NAME = "DeathDialog";
    /// <summary>
    /// 动物可以收获的进度值
    /// </summary>
    public const int ANIMAL_CAN_HARVEST_PROCESS = 100;

    #endregion

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
        /// 已播种已湿润
        /// </summary>
        SeedWet = 1 << 2,
        /// <summary>
        /// 生长已干涸
        /// </summary>
        GrowingThirsty = 1 << 3,
        /// <summary>
        /// 生长中已湿润
        /// </summary>
        GrowingWet = 1 << 4,
        /// <summary>
        /// 干枯
        /// </summary>
        Withered = 1 << 5,
        /// <summary>
        /// 等待收获
        /// </summary>
        Harvest = 1 << 6,
        /// <summary>
        /// 腐烂收获（播种专精不够）
        /// </summary>
        RotHarvest = 1 << 7,
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
        /// 浇水（有线形进度）
        /// </summary>
        Watering = 1 << 3,
        /// <summary>
        /// 施肥
        /// </summary>
        Manure = 1 << 4,
        /// <summary>
        /// 收获收割（没有进度）
        /// </summary>
        Harvest = 1 << 5,
        /// <summary>
        /// 斧头砍树（有进度）
        /// </summary>
        Cut = 1 << 6,
        /// <summary>
        /// 镐子挖矿（有进度）
        /// </summary>
        Mining = 1 << 7,
        /// <summary>
        /// 铲除植物
        /// </summary>
        Eradicate = 1 << 8,
        /// <summary>
        /// 放置动物食物
        /// </summary>
        PutAnimalFood = 1 << 9,
        /// <summary>
        /// 安抚动物
        /// </summary>
        Appease = 1 << 10,
        /// <summary>
        /// 剪毛
        /// </summary>
        Shearing = 1 << 11,
        /// <summary>
        /// 挤奶
        /// </summary>
        Milking = 1 << 12,
        /// <summary>
        /// 操作遗言
        /// </summary>
        LastWords = 1 << 13,
        /// <summary>
        /// 攻击敌人 怪物 boss（这个给伤害计算分类用的 家园并不使用）
        /// </summary>
        AttackEnemy = 1 << 31,
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
        /// 采集物
        /// </summary>
        HomeResource = 1 << 1,
        /// <summary>
        /// 动物食盆
        /// </summary>
        AnimalBowl = 1 << 2,
        /// <summary>
        /// 动物
        /// </summary>
        Animal = 1 << 3,
    }

    public enum eHomeResourcesAreaType : int
    {
        empty,    //空地
        farmland, //农田
    }
}