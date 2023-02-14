using UnityEngine;

/// <summary>
/// 畜牧食盆
/// </summary>
public class AnimalBowlCore : MonoBehaviour
{
    private void Awake()
    {
        _ = gameObject.AddComponent<AnimalBowlData>();
    }
}