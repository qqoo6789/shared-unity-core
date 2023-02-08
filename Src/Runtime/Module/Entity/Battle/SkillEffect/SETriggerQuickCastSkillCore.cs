/*
* @Author: xiang huan
* @Date: 2022-07-19 16:19:58
* @Description: 触发快速释放技能
 * @FilePath: /meland-scene-server/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/SkillEffect/SETriggerQuickCastSkillCore.cs
* 
*/

using UnityGameFramework.Runtime;

public class SETriggerQuickCastSkillCore : SkillEffectBase
{

    protected int TriggerSkillID;
    protected int TriggerType;
    protected int TriggerRate;
    protected int CastSkillID;
    /// <summary>
    /// 检测能否应用效果
    /// </summary>
    /// <param name="fromEntity">发送方</param>
    /// <param name="targetEntity">接受方</param>
    public override bool CheckApplyEffect(EntityBase fromEntity, EntityBase targetEntity)
    {
        //目标方已经死亡
        if (targetEntity.BattleDataCore != null)
        {
            if (!targetEntity.BattleDataCore.IsLive())
            {
                return false;
            }
        }
        return true;
    }
    public override void Start()
    {
        base.Start();
        if (EffectData == null)
        {
            return;
        }
        if (EffectCfg.Parameters == null || EffectCfg.Parameters.Length < 4)
        {
            Log.Error($"SETriggerQuickCastSkillCore Parameters Error EffectID = {EffectID}");
            return;
        }
        TriggerSkillID = EffectCfg.Parameters[0];
        TriggerType = EffectCfg.Parameters[1];
        TriggerRate = EffectCfg.Parameters[2];
        CastSkillID = EffectCfg.Parameters[3];

    }
}