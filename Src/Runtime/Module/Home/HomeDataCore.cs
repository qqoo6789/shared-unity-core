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

    private EntityBase _fertileListenEntity;//监听某个实体(农场主)的肥沃值和快乐值

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

    protected void SetFertileHappyListenEntity(EntityBase role)
    {
        _fertileListenEntity = role;

        if (_fertileListenEntity == null)
        {
            return;
        }

        role.EntityEvent.EntityAttributeUpdate += OnEntityAttributeUpdate;
        UpdateFertileAndHappyMax();
    }

    protected void CancelFertileHappyListenEntity()
    {
        if (_fertileListenEntity == null)
        {
            return;
        }

        _fertileListenEntity.EntityEvent.EntityAttributeUpdate -= OnEntityAttributeUpdate;
        _fertileListenEntity = null;
    }

    private void OnEntityAttributeUpdate(eAttributeType type, int value)
    {
        if (type is not eAttributeType.MaxFertility and not eAttributeType.MaxPetHappiness)
        {
            return;
        }

        UpdateFertileAndHappyMax();
    }

    protected virtual void UpdateFertileAndHappyMax()
    {
        if (_fertileListenEntity == null)
        {
            return;
        }

        EntityAttributeData attr = _fertileListenEntity.EntityAttributeData;
        SetMaxFertileAndHappyValue((int)attr.GetRealValue(eAttributeType.MaxFertility), (int)attr.GetRealValue(eAttributeType.MaxPetHappiness));
    }
}