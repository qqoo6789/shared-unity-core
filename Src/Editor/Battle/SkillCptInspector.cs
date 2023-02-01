/*
 * @Author: xiang huan
 * @Date: 2023-01-16 09:44:22
 * @Description: 
 * @FilePath: /meland-unity/Assets/Plugins/SharedCore/Src/Editor/Battle/SkillCptInspector.cs
 * 
 */
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace SharedCore.Editor
{
    [CustomEditor(typeof(SkillCpt))]
    internal sealed class SkillCptInspector : UnityEditor.Editor
    {
        private string _skillID;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("仅运行时可用", MessageType.Info);
                return;
            }

            SkillCpt skillCpt = (SkillCpt)target;
            _ = EditorGUILayout.BeginHorizontal("box");
            {
                _skillID = EditorGUILayout.TextField("技能ID", _skillID);

                if (GUILayout.Button("添加"))
                {
                    if (int.TryParse(_skillID, out int value))
                    {
                        _ = skillCpt.AddSkill(value);
                    }

                }
                if (GUILayout.Button("删除"))
                {
                    if (int.TryParse(_skillID, out int value))
                    {
                        skillCpt.RemoveSkill(value);
                    }
                }
                if (GUILayout.Button("释放"))
                {
                    if (int.TryParse(_skillID, out int value))
                    {
                        Vector3 dir = skillCpt.RefEntity.Forward;
                        if (!skillCpt.CanUseSkill(value, dir, null))
                        {
                            return;
                        }
                        skillCpt.RefEntity.EntityEvent.InputSkillRelease?.Invoke(value, dir, null, true);
                    }
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("当前技能数量", skillCpt.SkillMap.Count.ToString());

            foreach (KeyValuePair<int, SkillBase> item in skillCpt.SkillMap)
            {
                _ = EditorGUILayout.BeginVertical("box");
                {
                    EditorGUILayout.LabelField(item.Key.ToString(), item.Value.DRSkill.SkillName);
                }
                EditorGUILayout.EndVertical();
            }

            Repaint();
        }

    }
}
