/*
 * @Author: xiang huan
 * @Date: 2023-04-25 13:33:31
 * @Description: 自定义消息引用接口
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/HotFix/Module/CustomReferencePool/IMessageReference.cs
 * 
 */

public interface ICustomReference
{
    /// <summary>
    /// 清理引用。
    /// </summary>
    void Clear();
}

