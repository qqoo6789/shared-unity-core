using UnityEngine;

/// <summary>
/// 畜牧食盆
/// </summary>
public class AnimalBowlCore : SharedCoreComponent
{
    [Header("食盆的id 需要唯一 也是吃的顺序")]
    public ulong BowlId;

    private void Awake()
    {
        _ = gameObject.AddComponent<AnimalBowlData>();
    }
}