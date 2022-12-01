using UnityEngine;

/// <summary>
/// 单块土地上的数据
/// </summary>
public class SoilData : MonoBehaviour
{
    public ulong Id { get; private set; }

    internal void SetId(ulong id)
    {
        Id = id;
    }
}