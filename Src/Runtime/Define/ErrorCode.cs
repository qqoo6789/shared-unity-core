/** 
 * @Author XQ
 * @Date 2022-08-05 12:54:47
 * @FilePath /Assets/Plugins/SharedCore/Src/Runtime/Define/ErrorCode.cs
 */
/// <summary>
/// 统一errorCode 首位代表常规错误码还是s2s 前3位模块 后3位模块内 预留头2位将来用
/// </summary>
public enum eErrorCode : int
{
    success = 0,

    #region  常规错误码 主要是S2C 但是有些通用
    selfMoveFail = 0001001,
    pickDropNotExist = 0002001,//拾取物品不存在
    pickDropNotYours = 0002002,//拾取物品不是自己的
    pickDropOverdue = 0002003,//拾取物品过期
    pickDropOverRange = 0002004,//拾取物品超出范围
    pickDropPickerIllegal = 0002005,//拾取者非法
    inputSkillFail = 0003001,
    inputSkillStatusFail = 0003002,
    inputSkillCDFail = 0003003,

    playerReviveAlive = 0004001, //玩家还存活
    playerReviveCD = 0004002, //玩家复活CD中
    entityCDDataCoreNull = 0004003, //玩家角色数据为空
    playerRevivePointNull = 0004004, //玩家附近复活点为空

    homeCollectResourceNotExist = 0005001, //家园采集资源不存在
    homeActionNotSupport = 0005002, //家园操作不支持
    homeItemCostError = 0005003, //家园道具扣除异常

    #endregion

    #region  S2S错误码
    notFindUserData = 1001404
    #endregion
}