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

    /// <summary>
    /// 场景所有阻挡层mask
    /// </summary>
    /// <returns></returns>
    public const int MASK_SCENE_OBSTRUCTION = (1 << DEFAULT) | (1 << TERRAIN);
}