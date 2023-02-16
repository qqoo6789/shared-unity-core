using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 畜牧场景管理
/// </summary>
public class HomeAnimalScene : MonoBehaviour
{
    /// <summary>
    /// 动物活动区域x z的范围
    /// </summary>
    /// <value></value>
    public Rect PlaygroundRect { get; private set; }
    /// <summary>
    /// 动物活动区域y水平面
    /// </summary>
    /// <value></value>
    public float PlaygroundY { get; private set; }
    /// <summary>
    /// 动物活动区域尺寸
    /// </summary>
    /// <value></value>
    public Vector3 PlaygroundSize { get; private set; }

    /// <summary>
    /// 所有食盆 顺序为吃的顺序
    /// </summary>
    private readonly List<AnimalBowlCore> _bowlList = new();

    private void Awake()
    {
        GameObject homeSceneConfigGo = GameObject.FindWithTag(MTag.HOME_ANIMAL_SCENE_CONFIG);
        AnimalSceneConfig sceneConfig = homeSceneConfigGo == null ? null : homeSceneConfigGo.GetComponent<AnimalSceneConfig>();
        if (sceneConfig == null)
        {
            Log.Error("场景中没有AnimalSceneConfig");
            return;
        }

        PlaygroundSize = sceneConfig.PlaygroundSize;
        Vector3 configPos = sceneConfig.transform.position;
        float x = configPos.x - (PlaygroundSize.x * 0.5f);
        float z = configPos.z - (PlaygroundSize.z * 0.5f);
        PlaygroundRect = new Rect(x, z, PlaygroundSize.x, PlaygroundSize.z);
        PlaygroundY = configPos.y;
    }

    /// <summary>
    /// 添加食盆 需要排好序 顺序为吃的顺序
    /// </summary>
    /// <param name="bowlCore"></param>
    internal void AddBowls(AnimalBowlCore bowlCore)
    {
        _bowlList.Add(bowlCore);
    }
}