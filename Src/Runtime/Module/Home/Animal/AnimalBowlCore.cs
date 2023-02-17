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

    /// <summary>
    /// 食盆被消耗掉食物
    /// </summary>
    /// <param name="costFood"></param>
    public virtual void CostFood(int costFood)
    {
        Data.SaveData.RemainFoodCapacity = Mathf.Max(0, Data.SaveData.RemainFoodCapacity - costFood);
        if (Data.SaveData.RemainFoodCapacity <= 0)
        {
            Data.SaveData.FoodCid = 0;
        }
    }
}