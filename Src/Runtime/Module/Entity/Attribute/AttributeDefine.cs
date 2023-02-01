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