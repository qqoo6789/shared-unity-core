/*
 * @Author: xiang huan
 * @Date: 2023-02-09 14:54:09
 * @Description: 技能输入数据 
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/Skill/InputSkillReleaseData.cs
 * 
 */


using UnityEngine;

public class InputSkillReleaseData
{
    /// <summary>
    /// 技能位置
    /// </summary>
    public int SkillID { get; private set; }
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
    /// 尝试释放
    /// </summary>
    public bool IsTry { get; private set; }
    /// <summary>
    /// 技能释放速率
    /// </summary>
    public double SkillTimeScale { get; private set; }
    /// <summary>
    /// 技能输入数据
    /// </summary>
    /// <param name="skillID">技能ID</param>
    /// <param name="dir">技能方向</param>
    /// <param name="targets">技能目标列表</param>
    /// <param name="targetPos">技能目标位置</param>
    /// <param name="isTry">尝试释放</param>
    /// <param name="skillTimeScale">释放速率</param>
    public InputSkillReleaseData(int skillID, Vector3 dir, long[] targets, Vector3 targetPos, bool isTry = false, double skillTimeScale = 1)
    {
        SkillID = skillID;
        Dir = dir;
        Targets = targets;
        TargetPos = targetPos;
        IsTry = isTry;
        SkillTimeScale = skillTimeScale;
    }
}