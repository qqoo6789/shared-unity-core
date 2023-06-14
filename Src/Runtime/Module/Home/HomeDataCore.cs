using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 家园数据core
/// </summary>
public class HomeDataCore : MonoBehaviour
{
    /// <summary>
    /// 家园主人的用户id
    /// </summary>
    /// <value></value>
    public long OwnerPlayerId { get; private set; }
    public string OwnerPlayerName { get; private set; }

    public int UsedSoilFertileValue { get; private set; }
    public int MaxSoilFertileValue { get; private set; }
    public int UsedAnimalHappyValue { get; private set; }
    public int MaxAnimalHappyValue { get; private set; }

    public void SetOwnerInfo(long ownerUserId, string ownerName)
    {
        OwnerPlayerId = ownerUserId;
        OwnerPlayerName = ownerName;
    }

    protected virtual void Start()
    {
        MessageCore.HomeUsedSoilFertileChanged += OnHomeUsedSoilFertileChanged;
    }

    protected virtual void OnDestroy()
    {
        MessageCore.HomeUsedSoilFertileChanged -= OnHomeUsedSoilFertileChanged;
    }

    private void OnHomeUsedSoilFertileChanged(ulong soilId, int deltaFertile)
    {
        UsedSoilFertileValue += deltaFertile;

        if (UsedSoilFertileValue < 0)
        {
            Log.Error($"土地肥沃值不应该小于0,当前值为{UsedSoilFertileValue}");
            UsedSoilFertileValue = 0;
        }
    }

    public virtual void SetUsedSoilFertileValue(int fertileValue)
    {
        UsedSoilFertileValue = fertileValue;
    }

    public virtual void SetUsedAnimalHappyValue(int happyValue)
    {
        UsedAnimalHappyValue = happyValue;
    }

    public virtual void SetMaxFertileAndHappyValue(int maxFertile, int maxHappy)
    {
        MaxSoilFertileValue = maxFertile;
        MaxAnimalHappyValue = maxHappy;
    }

    /// <summary>
    /// 剩余的土地肥沃值 >= 0
    /// </summary>
    /// <returns></returns>
    public int GetRemainSoilFertileValue()
    {
        return Mathf.Max(0, MaxSoilFertileValue - UsedSoilFertileValue);
    }

    /// <summary>
    /// 剩余的动物快乐值 >= 0
    /// </summary>
    /// <returns></returns>
    public int GetRemainAnimalHappyValue()
    {
        return Mathf.Max(0, MaxAnimalHappyValue - UsedAnimalHappyValue);
    }
}