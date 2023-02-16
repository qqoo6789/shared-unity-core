using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class AnimalInitLogicCore : MonoBehaviour
{
    /// <summary>
    /// 恢复畜牧食盆数据
    /// </summary>
    /// <param name="bowlDatas"></param>
    public void RestoreBowlData<T>(AnimalBowlSaveData[] bowlDatas) where T : AnimalBowlCore
    {
        List<T> bowlList = RestoreBowlEntityList<T>(bowlDatas);

        //按照食盆id排序 吃的顺序是最终数组顺序
        bowlList.Sort((a, b) => a.Data.SaveData.BowlId < b.Data.SaveData.BowlId ? -1 : 1);

        HomeAnimalScene homeAnimalScene = GetComponent<HomeAnimalScene>();
        for (int i = 0; i < bowlList.Count; i++)
        {
            homeAnimalScene.AddBowls(bowlList[i]);
        }
    }

    /// <summary>
    /// 获取恢复出来的食盆实体列表 不返回null
    /// </summary>
    /// <param name="bowlDatas"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static List<T> RestoreBowlEntityList<T>(AnimalBowlSaveData[] bowlDatas) where T : AnimalBowlCore
    {
        GameObject[] bowlGos = GameObject.FindGameObjectsWithTag(MTag.HOME_ANIMAL_BOWL);
        if (bowlGos == null || bowlGos.Length == 0)
        {
            return new List<T>();
        }

        Dictionary<ulong, AnimalBowlSaveData> saveDataMap = new();
        if (bowlDatas != null)
        {
            foreach (AnimalBowlSaveData saveData in bowlDatas)
            {
                saveDataMap.Add(saveData.BowlId, saveData);
            }
        }

        List<T> bowlList = new();
        HashSet<ulong> bowlIdSet = new();
        foreach (GameObject bowlGo in bowlGos)
        {
            AnimalBowlConfig bowlConfig = bowlGo.GetComponent<AnimalBowlConfig>();
            if (bowlConfig == null)
            {
                continue;
            }

            if (bowlIdSet.Contains(bowlConfig.BowlId))
            {
                Log.Error($"食盆id重复 bowlId:{bowlConfig.BowlId}");
                continue;
            }

            bowlIdSet.Add(bowlConfig.BowlId);
            T bowl = bowlGo.AddComponent<T>();
            saveDataMap.TryGetValue(bowlConfig.BowlId, out AnimalBowlSaveData saveData);
            bowl.Data.SetSaveData(saveData);
            bowlList.Add(bowl);
        }

        return bowlList;
    }
}