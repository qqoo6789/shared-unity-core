using UnityEngine;

/// <summary>
/// 挂载家园场景中的畜牧模块的场景配置
/// </summary>
public class AnimalSceneConfig : MonoBehaviour
{
    [Header("动物食盆配置 不要留空 顺序代表动物饥饿找吃顺序")]
    public AnimalBowlCore[] AnimBowls;

    [Header("动物活动区域范围")]
    public Bounds PlaygroundArea;
}