
using GameMessageCore;


public class UseSkillParam
{
    public int SkillID;
    public int SeqId;
    public UnityEngine.Vector3 Pos;
    public UnityEngine.Vector3 SkillDir;
    public long[] Targets;
    public UseCollectResourceSkillInfo HomeSkillInfo;
    public UseSkillCostItem CostItem;
    public float AccumulateTime;
}