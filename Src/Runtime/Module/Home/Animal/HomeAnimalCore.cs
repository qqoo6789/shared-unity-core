using UnityEngine;

/// <summary>
/// 家园动物core
/// </summary>
public class HomeAnimalCore : MonoBehaviour
{
    private void Awake()
    {
        _ = gameObject.AddComponent<AnimalDataCore>();
    }
}