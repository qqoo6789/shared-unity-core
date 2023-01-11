/*
 * @Author: xiang huan
 * @Date: 2022-07-19 13:38:00
 * @Description: 技能效果组件
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Battle/Cpt/SkillEffectCpt.cs
 * 
 */
using System.Collections.Generic;
public class SkillEffectCpt : EntityBaseComponent
{

    public enum eEffectType
    {
        Runtime,
        Static, //静态效果，不会进行刷新。节省性能开销
    }
    private Dictionary<eEffectType, List<SkillEffectBase>> _skillEffectMap;

    private int _immuneFlag; //免疫标识
    private void Awake()
    {
        _skillEffectMap = new()
        {
            { eEffectType.Runtime, new() },
            { eEffectType.Static, new() }
        };
    }

    private void Update()
    {
        List<SkillEffectBase> runList = _skillEffectMap[eEffectType.Runtime];
        if (runList.Count <= 0)
        {
            return;
        }

        long curTimeStamp = TimeUtil.GetTimeStamp();
        bool isUpdateImmuneFlag = false;
        for (int i = runList.Count - 1; i >= 0; i--)
        {
            SkillEffectBase effect = runList[i];
            if (effect.DestroyTimestamp > 0 && curTimeStamp >= effect.DestroyTimestamp)
            {
                effect.RemoveEffect();
                effect.Dispose();
                runList.RemoveAt(i);
                isUpdateImmuneFlag = true;
            }
            else
            {
                if (effect.IsUpdate)
                {
                    effect.Update();
                }
            }
        }
        if (isUpdateImmuneFlag)
        {
            UpdateImmuneFlag();
        }
    }

    /// <summary>
    /// 检测能否应用效果
    /// </summary>
    /// <param name="fromEntity">发送方</param>
    /// <param name="targetEntity">接受方</param>
    /// <param name="effect">效果</param>
    public bool CheckApplyEffect(EntityBase fromEntity, EntityBase targetEntity, SkillEffectBase effect)
    {
        if ((_immuneFlag & effect.EffectFlag) != 0)
        {
            return false;
        }
        return effect.CheckApplyEffect(fromEntity, targetEntity);
    }

    //应用某个效果
    public void ApplyOneEffect(SkillEffectBase effect)
    {
        if (effect == null)
        {
            return;
        }
        effect.AddEffect(RefEntity);
        if (effect.Duration != 0)
        {
            effect.DestroyTimestamp = effect.Duration > 0 ? (TimeUtil.GetTimeStamp() + effect.Duration) : -1;
            if (effect.Duration > 0 || effect.IsUpdate)
            {
                AddEffectList(effect, _skillEffectMap[eEffectType.Runtime]);
            }
            else
            {
                AddEffectList(effect, _skillEffectMap[eEffectType.Static]);
            }

        }
        else
        {
            effect.Start();
            effect.RemoveEffect();
            effect.Dispose();
        }
    }

    //添加到效果列表
    private void AddEffectList(SkillEffectBase effect, List<SkillEffectBase> effectList)
    {
        if (effect.IsRepeat)
        {
            effect.Start();
            effectList.Add(effect);
        }
        else
        {
            int oldIndex = effectList.FindIndex(value =>
            {
                return value.EffectID == effect.EffectID;
            });
            if (oldIndex >= 0)
            {
                SkillEffectBase oldEffect = effectList[oldIndex];
                effect.OnRefreshRepeat(oldEffect);
                oldEffect.RemoveEffect();
                oldEffect.Dispose();
                effectList.RemoveAt(oldIndex);
            }
            effect.Start();
            effectList.Add(effect);
        }
        UpdateImmuneFlag();
    }

    //取消某种效果
    public void AbolishSkillEffect(int effectID)
    {
        foreach (KeyValuePair<eEffectType, List<SkillEffectBase>> item in _skillEffectMap)
        {
            List<SkillEffectBase> effectList = item.Value;
            for (int i = effectList.Count - 1; i >= 0; i--)
            {
                SkillEffectBase effect = effectList[i];
                if (effect.EffectID == effectID)
                {
                    effect.RemoveEffect();
                    effect.Dispose();
                    effectList.RemoveAt(i);
                }
            }
        }
        UpdateImmuneFlag();
    }

    //取消某种效果
    public void AbolishSkillEffect(int effectID, int skillID, long fromID)
    {
        foreach (KeyValuePair<eEffectType, List<SkillEffectBase>> item in _skillEffectMap)
        {
            List<SkillEffectBase> effectList = item.Value;
            for (int i = effectList.Count - 1; i >= 0; i--)
            {
                SkillEffectBase effect = effectList[i];
                if (effect.EffectID == effectID && effect.SkillID == skillID && effect.FromID == fromID)
                {
                    effect.RemoveEffect();
                    effect.Dispose();
                    effectList.RemoveAt(i);
                }
            }
        }
        UpdateImmuneFlag();
    }
    public void ClearAllSkillEffect()
    {
        foreach (KeyValuePair<eEffectType, List<SkillEffectBase>> item in _skillEffectMap)
        {
            List<SkillEffectBase> effectList = item.Value;
            for (int i = effectList.Count - 1; i >= 0; i--)
            {
                SkillEffectBase effect = effectList[i];
                effect.RemoveEffect();
                effect.Dispose();
                effectList.RemoveAt(i);
            }
            item.Value.Clear();
        }
        UpdateImmuneFlag();
    }

    public void UpdateImmuneFlag()
    {
        _immuneFlag = 0;
        foreach (KeyValuePair<eEffectType, List<SkillEffectBase>> item in _skillEffectMap)
        {
            List<SkillEffectBase> effectList = item.Value;
            for (int i = effectList.Count - 1; i >= 0; i--)
            {
                SkillEffectBase effect = effectList[i];
                _immuneFlag |= effect.EffectImmuneFlag;
            }
        }
    }
    private void OnDestroy()
    {
        ClearAllSkillEffect();
    }
}
