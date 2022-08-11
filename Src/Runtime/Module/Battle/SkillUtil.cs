
using UnityEngine;

public static partial class SkillUtil
{
    /// <summary>
    /// 点对点命中检测
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <param name="blockLayer">遮挡层</param>
    /// <returns></returns>
    public static bool CheckP2P(Vector3 origin, Vector3 target, int blockLayer)
    {
        if (origin.ApproximatelyEquals(target))
        {
            return true;
        }

        bool result = !Physics.Linecast(origin, target, blockLayer);
        return result;
    }

    /// <summary>
    /// 点对点命中检测
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <param name="hitInfo"></param>
    /// <param name="blockLayer">遮挡层</param>
    /// <returns></returns>
    public static bool CheckP2P(Vector3 origin, Vector3 target, out RaycastHit hitInfo, int blockLayer)
    {
        if (origin.ApproximatelyEquals(target))
        {
            hitInfo = new RaycastHit();
            return true;
        }

        bool result = !Physics.Linecast(origin, target, out hitInfo, blockLayer);
        return result;
    }

    public static int GetEntityTargetLayer(MelandGame3.EntityType type)
    {
        int targetLayer;
        int monsterLayer = LayerMask.GetMask("Monster");
        int playerLayer = LayerMask.GetMask("Player");
        switch (type)
        {
            case MelandGame3.EntityType.MainPlayer:
            case MelandGame3.EntityType.EntityTypePlayer:
                targetLayer = 1 << monsterLayer;
                break;
            case MelandGame3.EntityType.EntityTypeMonster:
                targetLayer = 1 << playerLayer;
                break;
            default:
                targetLayer = 0;
                break;
        }
        return targetLayer;
    }
}