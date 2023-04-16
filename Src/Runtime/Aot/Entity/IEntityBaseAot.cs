using UnityEngine;
/// <summary>
/// aot中的 entity base，
/// 真正的EntityBase在热更模块中，由于aot不能直接访问hotfix中的类型，这边提供一个最基础的类和虚方法给其他模使用 eg:Pixel Crushers的Sequencer.cs
/// 可能会有其他更优做法
/// </summary>
public interface IEntityBaseAot
{
    Vector3 Position { get; }
    GameObject EntityRoot { get; }
    Transform GetTransform();
    //酌情添加
}