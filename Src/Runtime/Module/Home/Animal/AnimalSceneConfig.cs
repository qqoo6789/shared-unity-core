using UnityEngine;

/// <summary>
/// 挂载家园场景中的畜牧模块的场景配置
/// </summary>
public class AnimalSceneConfig : MonoBehaviour
{
    [Header("动物食盆配置 不要留空 顺序代表动物饥饿找吃顺序")]
    public AnimalBowlCore[] AnimBowls;

    [Header("动物活动区域大小 中心点为本物体位置")]
    public Vector3 PlaygroundSize = new Vector3(10, 0, 10);

    /// <summary>
    /// 动物活动区域x z的范围
    /// </summary>
    /// <value></value>
    public Rect PlaygroundRect { get; private set; }

    private void Awake()
    {
        PlaygroundRect = new Rect(transform.position.x - PlaygroundSize.x * 0.5f, transform.position.z - PlaygroundSize.z * 0.5f, PlaygroundSize.x, PlaygroundSize.z);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, PlaygroundSize);
    }
#endif
}