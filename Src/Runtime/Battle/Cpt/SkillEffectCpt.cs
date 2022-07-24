/*
 * @Author: xiang huan
 * @Date: 2022-07-19 13:38:00
 * @Description: 技能效果组件
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Runtime/Battle/Cpt/SkillEffectCpt.cs
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectCpt : MonoBehaviour
{
    public List<SkillEffectBase> _skillEffects;
    // Start is called before the first frame update
    void Awake()
    {
        _skillEffects = new();
    }

    // Update is called once per frame
    void Update()
    {
        if (_skillEffects.Count <= 0)
        {
            return;
        }

        long curTimeStamp = TimeUtil.GetTimeStamp();
        for (int i = _skillEffects.Count - 1; i >= 0; i--)
        {
            SkillEffectBase effect = _skillEffects[i];
            if (effect.DestroyTimestamp > 0 && curTimeStamp >= effect.DestroyTimestamp)
            {
                effect.RemoveEffect();
                effect.Dispose();
                _skillEffects.RemoveAt(i);
            }
            else
            {
                effect.Update();
            }
        }
    }

    //应用某个效果
    public void ApplyOneEffect(SkillEffectBase effect)
    {
        if (effect == null)
        {
            return;
        }
        effect.AddEffect(gameObject);
        if (effect.Duration != 0)
        {
            effect.DestroyTimestamp = effect.Duration > 0 ? (TimeUtil.GetTimeStamp() + effect.Duration * 1000) : -1;
            AddEffectTimeList(effect);
        }
        else
        {
            effect.Start();
            effect.RemoveEffect();
            effect.Dispose();
        }
    }

    //添加到计时队列
    private void AddEffectTimeList(SkillEffectBase effect)
    {
        if (effect.IsRepeat)
        {
            effect.Start();
            _skillEffects.Add(effect);
        }
        else
        {
            SkillEffectBase oldEffect = _skillEffects.Find(value =>
            {
                return value.EffectID == effect.EffectID;
            });
            if (oldEffect != null)
            {
                oldEffect.OnRefreshRepeat(effect);
            }
            else
            {
                effect.Start();
                _skillEffects.Add(effect);
            }
        }
    }

    //取消某种效果
    public void AbolishSkillEffect(int effectID)
    {
        for (int i = _skillEffects.Count - 1; i >= 0; i--)
        {
            SkillEffectBase effect = _skillEffects[i];
            if (effect.EffectID == effectID)
            {
                effect.RemoveEffect();
                effect.Dispose();
                _skillEffects.RemoveAt(i);
            }
        }
    }

    public void ClearAllSkillEffect()
    {
        for (int i = _skillEffects.Count - 1; i >= 0; i--)
        {
            SkillEffectBase effect = _skillEffects[i];
            effect.RemoveEffect();
            effect.Dispose();
        }
        _skillEffects.Clear();
    }

    private void OnDestroy()
    {
        ClearAllSkillEffect();
    }
}
