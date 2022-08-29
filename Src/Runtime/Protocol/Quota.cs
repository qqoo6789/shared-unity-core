// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: quota.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MelandGame3 {

  /// <summary>Holder for reflection information generated from quota.proto</summary>
  public static partial class QuotaReflection {

    #region Descriptor
    /// <summary>File descriptor for quota.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static QuotaReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgtxdW90YS5wcm90bxILTWVsYW5kR2FtZTMilQEKDFF1b3RhU2V0dGluZxIq",
            "CgpxdW90YV90eXBlGAEgASgOMhYuTWVsYW5kR2FtZTMuUXVvdGFUeXBlEhUK",
            "DXF1b3RhX3N1YnR5cGUYAiABKAUSEQoJbmVlZF9zeW5jGAMgASgIEi8KCmNs",
            "ZWFuX3R5cGUYBCABKA4yGy5NZWxhbmRHYW1lMy5RdW90YUNsZWFuVHlwZSq7",
            "AQoJUXVvdGFUeXBlEh4KGlF1b3RhVHlwZV9RdW90YUtpbGxNb25zdGVyEAAS",
            "HgoaUXVvdGFUeXBlX1F1b3RhQmlydGhQb3NJZHgQARIiCh5RdW90YVR5cGVf",
            "UXVvdGFFbXB0eVBsYXllckFyZWEQAhIlCiFRdW90YVR5cGVfUXVvdGFHZXRF",
            "bXB0eVBsYXllckFyZWEQAxIjCh9RdW90YVR5cGVfUXVvdGFMZXNzb25UZW1w",
            "bGF0ZUlkEAQqnAEKDlF1b3RhQ2xlYW5UeXBlEiEKHVF1b3RhQ2xlYW5UeXBl",
            "X1F1b3RhQ2xlYW5Ob25lEAASIAocUXVvdGFDbGVhblR5cGVfUXVvdGFDbGVh",
            "bkRheRABEiEKHVF1b3RhQ2xlYW5UeXBlX1F1b3RhQ2xlYW5XZWVrEAISIgoe",
            "UXVvdGFDbGVhblR5cGVfUXVvdGFDbGVhbk1vbnRoEANiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::MelandGame3.QuotaType), typeof(global::MelandGame3.QuotaCleanType), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MelandGame3.QuotaSetting), global::MelandGame3.QuotaSetting.Parser, new[]{ "QuotaType", "QuotaSubtype", "NeedSync", "CleanType" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  /// <summary>
  ///次数类型
  /// </summary>
  public enum QuotaType {
    /// <summary>
    /// 击杀怪物数量
    /// </summary>
    [pbr::OriginalName("QuotaType_QuotaKillMonster")] QuotaKillMonster = 0,
    /// <summary>
    /// 出生点序号
    /// </summary>
    [pbr::OriginalName("QuotaType_QuotaBirthPosIdx")] QuotaBirthPosIdx = 1,
    /// <summary>
    /// 空领地数量
    /// </summary>
    [pbr::OriginalName("QuotaType_QuotaEmptyPlayerArea")] QuotaEmptyPlayerArea = 2,
    /// <summary>
    /// 已领取空领地数量
    /// </summary>
    [pbr::OriginalName("QuotaType_QuotaGetEmptyPlayerArea")] QuotaGetEmptyPlayerArea = 3,
    /// <summary>
    /// 提交作业的模板id
    /// </summary>
    [pbr::OriginalName("QuotaType_QuotaLessonTemplateId")] QuotaLessonTemplateId = 4,
  }

  /// <summary>
  ///次数清理类型
  /// </summary>
  public enum QuotaCleanType {
    /// <summary>
    ///不清理
    /// </summary>
    [pbr::OriginalName("QuotaCleanType_QuotaCleanNone")] QuotaCleanNone = 0,
    /// <summary>
    ///每日清理
    /// </summary>
    [pbr::OriginalName("QuotaCleanType_QuotaCleanDay")] QuotaCleanDay = 1,
    /// <summary>
    ///每周清理
    /// </summary>
    [pbr::OriginalName("QuotaCleanType_QuotaCleanWeek")] QuotaCleanWeek = 2,
    /// <summary>
    ///每月清理
    /// </summary>
    [pbr::OriginalName("QuotaCleanType_QuotaCleanMonth")] QuotaCleanMonth = 3,
  }

  #endregion

  #region Messages
  public sealed partial class QuotaSetting : pb::IMessage<QuotaSetting>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<QuotaSetting> _parser = new pb::MessageParser<QuotaSetting>(() => new QuotaSetting());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<QuotaSetting> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MelandGame3.QuotaReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public QuotaSetting() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public QuotaSetting(QuotaSetting other) : this() {
      quotaType_ = other.quotaType_;
      quotaSubtype_ = other.quotaSubtype_;
      needSync_ = other.needSync_;
      cleanType_ = other.cleanType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public QuotaSetting Clone() {
      return new QuotaSetting(this);
    }

    /// <summary>Field number for the "quota_type" field.</summary>
    public const int QuotaTypeFieldNumber = 1;
    private global::MelandGame3.QuotaType quotaType_ = global::MelandGame3.QuotaType.QuotaKillMonster;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::MelandGame3.QuotaType QuotaType {
      get { return quotaType_; }
      set {
        quotaType_ = value;
      }
    }

    /// <summary>Field number for the "quota_subtype" field.</summary>
    public const int QuotaSubtypeFieldNumber = 2;
    private int quotaSubtype_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int QuotaSubtype {
      get { return quotaSubtype_; }
      set {
        quotaSubtype_ = value;
      }
    }

    /// <summary>Field number for the "need_sync" field.</summary>
    public const int NeedSyncFieldNumber = 3;
    private bool needSync_;
    /// <summary>
    ///是否需要同步给前端
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool NeedSync {
      get { return needSync_; }
      set {
        needSync_ = value;
      }
    }

    /// <summary>Field number for the "clean_type" field.</summary>
    public const int CleanTypeFieldNumber = 4;
    private global::MelandGame3.QuotaCleanType cleanType_ = global::MelandGame3.QuotaCleanType.QuotaCleanNone;
    /// <summary>
    ///清理类型
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::MelandGame3.QuotaCleanType CleanType {
      get { return cleanType_; }
      set {
        cleanType_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as QuotaSetting);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(QuotaSetting other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (QuotaType != other.QuotaType) return false;
      if (QuotaSubtype != other.QuotaSubtype) return false;
      if (NeedSync != other.NeedSync) return false;
      if (CleanType != other.CleanType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (QuotaType != global::MelandGame3.QuotaType.QuotaKillMonster) hash ^= QuotaType.GetHashCode();
      if (QuotaSubtype != 0) hash ^= QuotaSubtype.GetHashCode();
      if (NeedSync != false) hash ^= NeedSync.GetHashCode();
      if (CleanType != global::MelandGame3.QuotaCleanType.QuotaCleanNone) hash ^= CleanType.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (QuotaType != global::MelandGame3.QuotaType.QuotaKillMonster) {
        output.WriteRawTag(8);
        output.WriteEnum((int) QuotaType);
      }
      if (QuotaSubtype != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(QuotaSubtype);
      }
      if (NeedSync != false) {
        output.WriteRawTag(24);
        output.WriteBool(NeedSync);
      }
      if (CleanType != global::MelandGame3.QuotaCleanType.QuotaCleanNone) {
        output.WriteRawTag(32);
        output.WriteEnum((int) CleanType);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (QuotaType != global::MelandGame3.QuotaType.QuotaKillMonster) {
        output.WriteRawTag(8);
        output.WriteEnum((int) QuotaType);
      }
      if (QuotaSubtype != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(QuotaSubtype);
      }
      if (NeedSync != false) {
        output.WriteRawTag(24);
        output.WriteBool(NeedSync);
      }
      if (CleanType != global::MelandGame3.QuotaCleanType.QuotaCleanNone) {
        output.WriteRawTag(32);
        output.WriteEnum((int) CleanType);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (QuotaType != global::MelandGame3.QuotaType.QuotaKillMonster) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) QuotaType);
      }
      if (QuotaSubtype != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(QuotaSubtype);
      }
      if (NeedSync != false) {
        size += 1 + 1;
      }
      if (CleanType != global::MelandGame3.QuotaCleanType.QuotaCleanNone) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) CleanType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(QuotaSetting other) {
      if (other == null) {
        return;
      }
      if (other.QuotaType != global::MelandGame3.QuotaType.QuotaKillMonster) {
        QuotaType = other.QuotaType;
      }
      if (other.QuotaSubtype != 0) {
        QuotaSubtype = other.QuotaSubtype;
      }
      if (other.NeedSync != false) {
        NeedSync = other.NeedSync;
      }
      if (other.CleanType != global::MelandGame3.QuotaCleanType.QuotaCleanNone) {
        CleanType = other.CleanType;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            QuotaType = (global::MelandGame3.QuotaType) input.ReadEnum();
            break;
          }
          case 16: {
            QuotaSubtype = input.ReadInt32();
            break;
          }
          case 24: {
            NeedSync = input.ReadBool();
            break;
          }
          case 32: {
            CleanType = (global::MelandGame3.QuotaCleanType) input.ReadEnum();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            QuotaType = (global::MelandGame3.QuotaType) input.ReadEnum();
            break;
          }
          case 16: {
            QuotaSubtype = input.ReadInt32();
            break;
          }
          case 24: {
            NeedSync = input.ReadBool();
            break;
          }
          case 32: {
            CleanType = (global::MelandGame3.QuotaCleanType) input.ReadEnum();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code