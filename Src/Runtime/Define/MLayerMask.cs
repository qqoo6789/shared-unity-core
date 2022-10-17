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

    public const int NPC = 16;

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
}