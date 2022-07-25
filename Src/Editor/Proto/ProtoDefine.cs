using System.IO;
using SharedCore.Editor;
public static class ProtoDefine
{
    // protos 临时目录 meland-unity/Temp/OutPbmessage
    public static readonly string TempOutPbmessageDir = Path.Combine(Constant.TempPath, "OutPbmessage");
    // 输出的 xxproto.cs 路径 meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Protocol
    public static readonly string PbmessageCsPath = Path.Combine(Constant.SharedCorePath, "Src/Runtime/Protocol");
    // window protoc 解析器 meland-unity/Packages/Google.Protobuf.Tools.3.20.1/tools/windows_x64/protoc.exe
    public static readonly string ProtocWindow = Path.Combine(Constant.ProjectPath, "Packages/Google.Protobuf.Tools.3.20.1/tools/windows_x64/protoc.exe");
    // mac protoc 解析器 meland-unity/Packages/Google.Protobuf.Tools.3.20.1/tools/macosx_x64/protoc
    public static readonly string ProtocMac = Path.Combine(Constant.ProjectPath, "Packages/Google.Protobuf.Tools.3.20.1/tools/macosx_x64/protoc");
}