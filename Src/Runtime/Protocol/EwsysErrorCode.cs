// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ewsys_error_code.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MelandGame3 {

  /// <summary>Holder for reflection information generated from ewsys_error_code.proto</summary>
  public static partial class EwsysErrorCodeReflection {

    #region Descriptor
    /// <summary>File descriptor for ewsys_error_code.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static EwsysErrorCodeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChZld3N5c19lcnJvcl9jb2RlLnByb3RvEgtNZWxhbmRHYW1lMyr3CAoLRXdF",
            "cnJvckNvZGUSGwoXRXdFcnJvckNvZGVfRUVDX1Vua25vd24QABIxCiRFRUNf",
            "Q2xpZW50X1N0YXJ0VXBfTmF0aXZlU2VydmVyX0ZhaWwQ77H/////////ARIo",
            "ChtFRUNfQ2xpZW50X0xvYWRBY2NvdW50X0ZhaWwQ7rH/////////ARIkChdF",
            "RUNfQ2xpZW50X0xvYWRNYXBfRmFpbBDtsf////////8BEiUKGEVFQ19DbGll",
            "bnRfQ3JlYXRNYXBfRmFpbBDssf////////8BEiUKGEVFQ19DbGllbnRfSm9p",
            "blJvb21fRmFpbBDrsf////////8BEikKHEVFQ19DbGllbnRfTG9hZFJvbGVJ",
            "bmZvX0ZhaWwQ6rH/////////ARInChpFRUNfQ2xpZW50X0xvYWRDb25maWdf",
            "RmFpbBDpsf////////8BEisKHkVFQ19DbGllbnRfTG9hZExlc3NvbkRhdGFf",
            "RmFpbBDosf////////8BEi8KIkVFQ19DbGllbnRfSm9pbl9MZXNzb25TZWN0",
            "aW9uX0ZhaWwQ57H/////////ARIxCiRFRUNfQ2xpZW50X0Nvbm5lY3RfTmF0",
            "aXZlU2VydmVyX0ZhaWwQ5rH/////////ARIvCiJFRUNfQ2xpZW50X05hdGl2",
            "ZVNlcnZlcl9Ob1JzcF9GYWlsEOWx/////////wESLwoiRUVDX1NlcnZlcl9T",
            "dGFydFVwX0dhbWVTZXJ2ZXJfRmFpbBDf4/7///////8BEigKG0VFQ19TZXJ2",
            "ZXJfTG9hZEFjY291bnRfRmFpbBDe4/7///////8BEiQKF0VFQ19TZXJ2ZXJf",
            "TG9hZE1hcF9GYWlsEN3j/v///////wESJQoYRUVDX1NlcnZlcl9DcmVhdE1h",
            "cF9GYWlsENzj/v///////wESJQoYRUVDX1NlcnZlcl9Kb2luUm9vbV9GYWls",
            "ENvj/v///////wESKQocRUVDX1NlcnZlcl9Mb2FkUm9sZUluZm9fRmFpbBDa",
            "4/7///////8BEicKGkVFQ19TZXJ2ZXJfTG9hZENvbmZpZ19GYWlsENnj/v//",
            "/////wESLgohRUVDX1BsYXRmb3JtX1N0YXJ0VXBfTWFwSGFsbF9GYWlsEM+V",
            "/v///////wESLgohRUVDX1BsYXRmb3JtX1N0YXJ0VXBfQWNjb3VudF9GYWls",
            "EM6V/v///////wESLQogRUVDX1BsYXRmb3JtX1N0YXJ0VXBfQmFubmVyX0Zh",
            "aWwQzZX+////////ARIqCh1FRUNfUGxhdGZvcm1fTG9hZEFjY291bnRfRmFp",
            "bBDMlf7///////8BEi4KIUVFQ19QbGF0Zm9ybV9DcmVhdFNlcnZlck5vZGVf",
            "RmFpbBDLlf7///////8BEisKHkVFQ19QbGF0Zm9ybV9Mb2FkUm9sZUluZm9f",
            "RmFpbBDKlf7///////8BEikKHEVFQ19QbGF0Zm9ybV9Mb2FkQ29uZmlnX0Zh",
            "aWwQyZX+////////AWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::MelandGame3.EwErrorCode), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum EwErrorCode {
    /// <summary>
    /// -------------------------------------------------------------------------
    /// 预警上送的错误类型码，文档中每定义一个补齐一个， 保持定义对应
    /// </summary>
    [pbr::OriginalName("EwErrorCode_EEC_Unknown")] EecUnknown = 0,
    /// <summary>
    ///******    -10001  ~ -20000  客户端错误码范围 ******
    /// </summary>
    [pbr::OriginalName("EEC_Client_StartUp_NativeServer_Fail")] EecClientStartUpNativeServerFail = -10001,
    /// <summary>
    ///  客户端加载账号信息失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_LoadAccount_Fail")] EecClientLoadAccountFail = -10002,
    /// <summary>
    ///  客户端加载地图数据失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_LoadMap_Fail")] EecClientLoadMapFail = -10003,
    /// <summary>
    ///  客户端创建地图失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_CreatMap_Fail")] EecClientCreatMapFail = -10004,
    /// <summary>
    ///  客户端进入房间失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_JoinRoom_Fail")] EecClientJoinRoomFail = -10005,
    /// <summary>
    ///  客户端加载角色信息失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_LoadRoleInfo_Fail")] EecClientLoadRoleInfoFail = -10006,
    /// <summary>
    ///  客户端加载配置文件失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_LoadConfig_Fail")] EecClientLoadConfigFail = -10007,
    /// <summary>
    ///  客户端加载课程数据失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_LoadLessonData_Fail")] EecClientLoadLessonDataFail = -10008,
    /// <summary>
    ///  客户端进入课程章节失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_Join_LessonSection_Fail")] EecClientJoinLessonSectionFail = -10009,
    /// <summary>
    ///  客户端连接 NativeServer失败
    /// </summary>
    [pbr::OriginalName("EEC_Client_Connect_NativeServer_Fail")] EecClientConnectNativeServerFail = -10010,
    /// <summary>
    ///  客户端请求 NativeServer消息无回应
    /// </summary>
    [pbr::OriginalName("EEC_Client_NativeServer_NoRsp_Fail")] EecClientNativeServerNoRspFail = -10011,
    /// <summary>
    ///******    -20001  ~ -30000  服务端错误码范围 ******
    /// </summary>
    [pbr::OriginalName("EEC_Server_StartUp_GameServer_Fail")] EecServerStartUpGameServerFail = -20001,
    /// <summary>
    ///  服务器加载账号信息失败
    /// </summary>
    [pbr::OriginalName("EEC_Server_LoadAccount_Fail")] EecServerLoadAccountFail = -20002,
    /// <summary>
    ///  服务器加载地图失败
    /// </summary>
    [pbr::OriginalName("EEC_Server_LoadMap_Fail")] EecServerLoadMapFail = -20003,
    /// <summary>
    ///  服务器创建地图失败
    /// </summary>
    [pbr::OriginalName("EEC_Server_CreatMap_Fail")] EecServerCreatMapFail = -20004,
    /// <summary>
    ///  玩家进入房间失败
    /// </summary>
    [pbr::OriginalName("EEC_Server_JoinRoom_Fail")] EecServerJoinRoomFail = -20005,
    /// <summary>
    ///  服务器加载角色信息失败
    /// </summary>
    [pbr::OriginalName("EEC_Server_LoadRoleInfo_Fail")] EecServerLoadRoleInfoFail = -20006,
    /// <summary>
    ///  服务器加载配置文件失败
    /// </summary>
    [pbr::OriginalName("EEC_Server_LoadConfig_Fail")] EecServerLoadConfigFail = -20007,
    /// <summary>
    ///******    -30001  ~ -40000  平台服务器错误码范围(hall,accout等) ******
    /// </summary>
    [pbr::OriginalName("EEC_Platform_StartUp_MapHall_Fail")] EecPlatformStartUpMapHallFail = -30001,
    /// <summary>
    ///  平台启动Account server失败
    /// </summary>
    [pbr::OriginalName("EEC_Platform_StartUp_Account_Fail")] EecPlatformStartUpAccountFail = -30002,
    /// <summary>
    ///  平台启动Banner server失败
    /// </summary>
    [pbr::OriginalName("EEC_Platform_StartUp_Banner_Fail")] EecPlatformStartUpBannerFail = -30003,
    /// <summary>
    ///  平台查询账号信息 失败
    /// </summary>
    [pbr::OriginalName("EEC_Platform_LoadAccount_Fail")] EecPlatformLoadAccountFail = -30004,
    /// <summary>
    ///  平台创建云端game失败
    /// </summary>
    [pbr::OriginalName("EEC_Platform_CreatServerNode_Fail")] EecPlatformCreatServerNodeFail = -30005,
    /// <summary>
    ///  平台加载角色信息失败
    /// </summary>
    [pbr::OriginalName("EEC_Platform_LoadRoleInfo_Fail")] EecPlatformLoadRoleInfoFail = -30006,
    /// <summary>
    ///  平台加载配置文件失败
    /// </summary>
    [pbr::OriginalName("EEC_Platform_LoadConfig_Fail")] EecPlatformLoadConfigFail = -30007,
  }

  #endregion

}

#endregion Designer generated code