/*
 * @Author: xiang huan
 * @Date: 2023-01-11 14:44:39
 * @Description: 属性定义
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Attribute/AttributeDefine.cs
 * 
 */

/// <summary>
/// 属性修改器类型
/// </summary>
public enum eModifierType : int
{
    Add = 1,  //增加
    PctAdd,   //百分比增加
    FinalAdd,  //最终增加
    FinalPctAdd,  //最终百分比增加
}

/// <summary>
/// 属性类型
/// </summary>
public enum eAttributeType : int
{
    Lv = 100,
    Exp = 200,
    Att = 300,
    AttSpeed = 400,
    Def = 500,
    CritRate = 600,
    CritDamage = 700,
    HitRate = 800,
    MissRate = 900,
    MoveSpeed = 1000,
    Hp = 1100,
    HpMax = 1200,
    HpRecovery = 1300,
}