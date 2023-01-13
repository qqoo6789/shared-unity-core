/*
 * @Author: xiang huan
 * @Date: 2022-09-13 17:26:26
 * @Description: 实体属性数据
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Attribute/EntityAttributeData.cs
 * 
 */
using System.Collections.Generic;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;

public class EntityAttributeData : EntityBaseComponent
{
    private readonly Dictionary<eAttributeType, IntAttribute> _attributeMap = new();
    private readonly Dictionary<eAttributeType, int> _defaultMap = new();
    private void Awake()
    {
        IDataTable<DREntityAttribute> dtAircraft = GFEntryCore.DataTable.GetDataTable<DREntityAttribute>();
        DREntityAttribute[] attributes = dtAircraft.GetAllDataRows();
        for (int i = 0; i < attributes.Length; i++)
        {
            _defaultMap.Add((eAttributeType)attributes[i].Id, attributes[i].DefaultValue);
        }
    }

    /// <summary>
    /// 获取属性值
    /// </summary>
    public int GetValue(eAttributeType type)
    {
        if (_attributeMap.TryGetValue(type, out IntAttribute attribute))
        {
            return attribute.Value;
        }
        //默认值
        if (_defaultMap.TryGetValue(type, out int value))
        {
            return value;
        }
        Log.Error($"EntityAttributeData GetValue Not Find Attribute Type = {type}");
        return 0;
    }
    /// <summary>
    /// 获取属性
    /// </summary>
    public IntAttribute GetAttribute(eAttributeType type)
    {
        if (_attributeMap.TryGetValue(type, out IntAttribute attribute))
        {
            return attribute;
        }
        return null;
    }

    /// <summary>
    /// 设置属性基础值
    /// </summary>
    public void SetBaseValue(eAttributeType type, int value)
    {
        if (!_attributeMap.TryGetValue(type, out IntAttribute attribute))
        {
            attribute = IntAttribute.Create();
            _attributeMap.Add(type, attribute);
        }
        //基础属性没变化
        if (attribute.BaseValue == value)
        {
            return;
        }
        _ = attribute.SetBase(value);
        RefEntity.EntityEvent.EntityAttributeUpdate?.Invoke(type, attribute.Value);
    }

    /// <summary>
    /// 获取属性基础值
    /// </summary>
    public int GetBaseValue(eAttributeType type)
    {
        if (_attributeMap.TryGetValue(type, out IntAttribute attribute))
        {
            return attribute.BaseValue;
        }
        return 0;
    }

    /// <summary>
    /// 新增修改属性
    /// </summary>
    public IntAttributeModifier AddModifier(eAttributeType type, eModifierType modifierType, int value)
    {
        if (!_attributeMap.TryGetValue(type, out IntAttribute attribute))
        {
            attribute = IntAttribute.Create();
            _attributeMap.Add(type, attribute);
        }
        IntAttributeModifier modifier = attribute.AddModifier(type, modifierType, value);
        RefEntity.EntityEvent.EntityAttributeUpdate?.Invoke(type, attribute.Value);
        return modifier;
    }

    /// <summary>
    /// 删除修改属性
    /// </summary>
    public void RemoveModifier(IntAttributeModifier modifier)
    {
        if (!_attributeMap.TryGetValue(modifier.AttributeType, out IntAttribute attribute))
        {
            Log.Error($"EntityAttributeData RemoveModifier Not Find Attribute Type = {modifier.AttributeType}");
            return;
        }
        attribute.RemoveModifier(modifier);
        RefEntity.EntityEvent.EntityAttributeUpdate?.Invoke(modifier.AttributeType, attribute.Value);
    }

    private void OnDestroy()
    {
        foreach (KeyValuePair<eAttributeType, IntAttribute> item in _attributeMap)
        {
            item.Value.Dispose();
        }
        _attributeMap.Clear();
    }
}