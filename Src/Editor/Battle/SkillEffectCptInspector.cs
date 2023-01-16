/*
 * @Author: xiang huan
 * @Date: 2023-01-16 09:44:22
 * @Description: 
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Editor/Battle/SkillEffectCptInspector.cs
 * 
 */
using System.Collections.Generic;

using UnityEditor;

namespace SharedCore.Editor
{
    [CustomEditor(typeof(SkillEffectCpt))]
    internal sealed class SkillEffectCptInspector : UnityEditor.Editor
    {
        private readonly HashSet<int> _openedItems = new();
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("仅运行时可用", MessageType.Info);
                return;
            }

            SkillEffectCpt skillEffectCpt = (SkillEffectCpt)target;
            foreach (KeyValuePair<SkillEffectCpt.eEffectType, List<SkillEffectBase>> item in skillEffectCpt.SkillEffectMap)
            {
                EditorGUILayout.LabelField(item.Key.ToString(), item.Value.Count.ToString());
                _ = EditorGUILayout.BeginVertical("box");
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        SkillEffectBase skillEffectBase = item.Value[i];
                        DrawSkillEffect(skillEffectBase);
                    }

                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.Separator();
            }

            Repaint();
        }
        private void DrawSkillEffect(SkillEffectBase skillEffectBase)
        {
            bool lastState = _openedItems.Contains(skillEffectBase.EffectID);
            bool currentState = EditorGUILayout.Foldout(lastState, skillEffectBase.EffectID.ToString());
            if (currentState != lastState)
            {
                if (currentState)
                {
                    _ = _openedItems.Add(skillEffectBase.EffectID);
                }
                else
                {
                    _ = _openedItems.Remove(skillEffectBase.EffectID);
                }
            }

            if (currentState)
            {
                long curTimeStamp = TimeUtil.GetTimeStamp();
                _ = EditorGUILayout.BeginVertical("box");
                {
                    EditorGUILayout.LabelField("效果ID", skillEffectBase.EffectID.ToString());
                    EditorGUILayout.LabelField("当前层数", skillEffectBase.CurLayer.ToString());
                    EditorGUILayout.LabelField("来源技能ID", skillEffectBase.SkillID.ToString());
                    EditorGUILayout.LabelField("来源实体ID", skillEffectBase.FromID.ToString());
                    EditorGUILayout.LabelField("剩余时间", ((skillEffectBase.DestroyTimestamp - curTimeStamp) / TimeUtil.MS2S).ToString());

                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Separator();
            }

        }

    }
}
