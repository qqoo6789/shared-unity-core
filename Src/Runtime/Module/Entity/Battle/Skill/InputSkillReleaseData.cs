/*
 * @Author: xiang huan
 * @Date: 2023-02-09 14:54:09
 * @Description: 技能输入数据 
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/Skill/InputSkillReleaseData.cs
 * 
 */


using UnityEngine;
using UnityGameFramework.Runtime;

public class InputSkillReleaseData
{
    /// <summary>
    /// 技能位置
    /// </summary>
    public int SkillID { get; private set; }
    /// <summary>
    /// 技能配置
    /// </summary>
    public DRSkill DRSkill { get; private set; }
    /// <summary>
    /// 目标方向
    /// </summary>
    public Vector3 Dir { get; private set; }
    /// <summary>
    /// 目标列表
    /// </summary>
    public long[] Targets { get; private set; }
    /// <summary>
    /// 目标位置
    /// </summary>
    public Vector3 TargetPos { get; private set; }

    /// <summary>
    /// 技能消耗道具
    /// </summary>
    public GameMessageCore.UseSkillCostItem CostItem { get; private set; }
    /// <summary>
    /// 尝试释放
    /// </summary>
    public bool IsTry { get; private set; }
    /// <summary>
    /// 技能释放速率
    /// </summary>
    public double SkillTimeScale { get; private set; }

    /// <summary>
    /// 技能蓄力时间 s
    /// </summary>
    public float AccumulateTime { get; private set; }

    /// <summary>
    /// 技能输入数据
    /// </summary>
    /// <param name="skillID">技能ID</param>
    /// <param name="dir">技能方向</param>
    /// <param name="targets">技能目标列表</param>
    /// <param name="targetPos">技能目标位置</param>
    /// <param name="isTry">尝试释放</param>
    /// <param name="skillTimeScale">释放速率</param>
    public InputSkillReleaseData(int skillID, Vector3 dir, long[] targets, Vector3 targetPos, bool isTry = false, double skillTimeScale = 1, GameMessageCore.UseSkillCostItem costItem = null, float accumulateTime = 0)
    {
        SkillID = skillID;
        Dir = dir;
        Targets = targets;
        TargetPos = targetPos;
        IsTry = isTry;
        SkillTimeScale = skillTimeScale;
        CostItem = costItem;
        AccumulateTime = accumulateTime;
        DRSkill = GFEntryCore.DataTable.GetDataTable<DRSkill>().GetDataRow(skillID);
        if (DRSkill == null)
        {
            Log.Error("InputSkillReleaseData The skill ID was not found in the skill table:{0}", skillID);
            return;
        }
    }

    public InputSkillReleaseData Clone()
    {
        return new InputSkillReleaseData(
            SkillID,
            Dir,
            Targets,
            TargetPos,
            IsTry,
            SkillTimeScale,
            CostItem,
            AccumulateTime
        );
    }

    /// <summary>
    /// 设置目标位置
    /// </summary>
    /// <param name="targetPos"></param>
    public void SetTargetPos(Vector3 targetPos)
    {
        TargetPos = targetPos;
    }
}