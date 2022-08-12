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
    #endregion

    #region  S2S错误码
    notFindUserData = 1001404
    #endregion
}