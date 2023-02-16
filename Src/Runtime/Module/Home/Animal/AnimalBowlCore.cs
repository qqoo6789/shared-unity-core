using UnityEngine;

/// <summary>
/// 畜牧食盆
/// </summary>
public class AnimalBowlCore : MonoBehaviour
{
    public AnimalBowlData Data { get; private set; }
    private void Awake()
    {
        Data = gameObject.AddComponent<AnimalBowlData>();
    }
}