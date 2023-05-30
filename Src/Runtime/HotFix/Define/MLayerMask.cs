/*
 * @Author: xiang huan
 * @Date: 2022-07-19 10:51:41
 * @Description: layer层级
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Define/MLayerMask.cs
 * 
 */

public static class MLayerMask
{
    /// <summary>
    /// 默认层级
    /// </summary>
    public const int DEFAULT = 0;
    public const int WATER = 4;
    public const int UI = 5;
    /// <summary>
    /// 地表层 能够行走的物体
    /// </summary>
    public const int TERRAIN = 6;
    public const int MONSTER = 10;
    public const int PLAYER = 11;
    public const int DISTANCE_CULL_SMALL = 12;
    public const int DISTANCE_CULL_MIDDLE = 13;
    public const int DISTANCE_CULL_BIG = 14;
    /// <summary>
    /// 各种效果触发层 主要是客户端的 比如区域声音 移动区域效果等 直接忽略碰撞
    /// </summary>
    public const int EFFECT_TRIGGER = 15;
    /// <summary>
    /// 公共触发层 npc，对话触发等  
    /// </summary>
    public const int PUBLIC_TRIGGER = 16;
    /// <summary>
    /// 格子建筑层
    /// </summary>
    public const int BUILDING = 17;
    /// <summary>
    /// 家园资源类型
    /// </summary>
    public const int HOME_RESOURCE = 18;
    /// <summary>
    /// 实体触碰层
    /// </summary>
    /// <returns></returns>
    public const int ENTITY_TRIGGER = 20;
    /// <summary>
    /// 像树叶那种遮挡人物时半透的场景物件层
    /// </summary>
    public const int OBSTRUCT_TRANSPARENT = 22;

    /// <summary>
    /// 默认的相机自动避开的遮挡层
    /// </summary>
    /// <returns></returns>
    public const int MASK_DEFAULT_CAMERA_OCCLUDE = (1 << DEFAULT) | (1 << TERRAIN) | (1 << DISTANCE_CULL_MIDDLE) | (1 << DISTANCE_CULL_BIG);
    /// <summary>
    /// 场景所有阻挡层mask
    /// </summary>
    /// <returns></returns>
    public const int MASK_SCENE_OBSTRUCTION = MASK_DEFAULT_CAMERA_OCCLUDE;
    /// <summary>
    /// 场景能被破坏的元素所在层
    /// </summary>
    /// <returns></returns>
    public const int MASK_SCENE_DESTRUCTION = (1 << DEFAULT) | (1 << DISTANCE_CULL_SMALL) | (1 << DISTANCE_CULL_MIDDLE) | (1 << DISTANCE_CULL_BIG);

}