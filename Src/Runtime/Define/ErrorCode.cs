/// <summary>
/// 统一errorCode 首位代表常规错误码还是s2s 前3位模块 后3位模块内 预留头2位将来用
/// </summary>
public enum eErrorCode : int
{
    success = 0,

    #region  常规错误码 主要是S2C 但是有些通用
    SelfMoveFail = 0001001,
    #endregion

    #region  S2S错误码
    NotFindUserData = 1001404
    #endregion
}