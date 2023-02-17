using System.Collections.Generic;
using GameMessageCore;
using UnityGameFramework.Runtime;
/// <summary>
/// 角色天赋数据
/// </summary>
public class PlayerTalentTreeDataCore : EntityBaseComponent
{
    /// <summary>
    /// 天赋树等级列表
    /// </summary>
    /// <returns></returns>
    protected List<GrpcTalentLevel> TalentTreeLvList;
    /// <summary>
    /// 所有类型的天赋树列表，方便遍历
    /// </summary>
    protected Dictionary<TalentType, List<GrpcTalentNodeData>> AllTalentTreeList;
    /// <summary>
    /// 所有类型的天赋树字典，方便查找
    /// </summary>
    protected Dictionary<TalentType, Dictionary<int, GrpcTalentNodeData>> AllTalentTreeDic;
    /// <summary>
    /// 天赋树配置表引用
    /// </summary>
    private GameFramework.DataTable.IDataTable<DRTalentTree> _refTalentTreeTable;

    private void Awake()
    {
        _refTalentTreeTable = GFEntryCore.DataTable.GetDataTable<DRTalentTree>();
    }

    private void OnDestroy()
    {
        AllTalentTreeList = null;
        AllTalentTreeDic = null;
        _refTalentTreeTable = null;
    }

    /// <summary>
    /// 初始化技能树数据
    /// 后面的变动通过 UpdateNode,AddNode,RemoveNode 来更新
    /// </summary>
    /// <param name="talentData"></param>
    public void Init(GrpcTalentData talentData)
    {
        Log.Info("init talent tree");
        TalentTreeLvList = talentData.Levels == null ? new() : new(talentData.Levels);
        AllTalentTreeDic = new();
        AllTalentTreeList = new();
        int treeCount = talentData.Trees == null ? 0 : talentData.Trees.Length;
        for (int treeIndex = 0; treeIndex < treeCount; treeIndex++)
        {
            GrpcTalentTree tree = talentData.Trees[treeIndex];
            AllTalentTreeList.Add(tree.TalentType, new(tree.Nodes));
            AllTalentTreeDic.Add(tree.TalentType, new());
            int nodeCount = tree.Nodes.Length;
            for (int nodeIndex = 0; nodeIndex < nodeCount; nodeIndex++)
            {
                GrpcTalentNodeData node = tree.Nodes[nodeIndex];
                AllTalentTreeDic[tree.TalentType].Add(node.NodeId, node);
            }
        }

        RefEntity.EntityEvent.TalentSkillInited?.Invoke(GetTalentSkills());
    }

    /// <summary>
    /// 获取天赋收益
    /// </summary>
    /// <param name="gainsType">收益类型</param>
    /// <param name="talentType">天赋类型</param>
    /// <returns></returns>
    public List<int> GetTalentGains(TalentGainsType gainsType, TalentType talentType = TalentType.Unknown)
    {
        List<int> result = new();
        if (talentType == TalentType.Unknown)
        {
            foreach (TalentType type in System.Enum.GetValues(typeof(TalentType)))
            {
                if (type != TalentType.Unknown)
                {
                    result.AddRange(GetTalentGains(gainsType, type));
                }
            }

            return result;
        }

        if (!AllTalentTreeList.TryGetValue(talentType, out List<GrpcTalentNodeData> treeList))
        {
            return result;
        }

        if (treeList == null || treeList.Count == 0)
        {
            Log.Warning($"can not find talent tree, talent type: {talentType}");
            return result;
        }

        for (int i = 0; i < treeList.Count; i++)
        {
            GrpcTalentNodeData node = treeList[i];
            if (node.Level <= 0)
            {
                //虽然节点解锁了，但是等级还是0，没有收益，不必计算
                continue;
            }

            DRTalentTree nodeCfg = _refTalentTreeTable.GetDataRow(node.NodeId);
            if (nodeCfg == null)
            {
                Log.Error($"can not find talent tree config, node id: {node.NodeId}");
                continue;
            }

            if (nodeCfg.GainsType == (int)gainsType)
            {
                //1级才会解锁收益，所以这里等级需要减1才能作为收益下标
                result.AddRange(nodeCfg.GainsArgs[node.Level - 1]);
            }
        }

        return result;
    }

    public List<int> GetTalentSkills()
    {
        List<int> skills = new();
        skills.AddRange(GetTalentGains(TalentGainsType.ActiveSkill));
        skills.AddRange(GetTalentGains(TalentGainsType.PassiveSkill));
        return skills;
    }

    /// <summary>
    /// 更新天赋节点
    /// </summary>
    /// <param name="newNode"></param>
    /// <param name="talentType"></param>
    public void UpdateNode(GrpcTalentNodeData newNode, TalentType talentType)
    {
        if (!AllTalentTreeDic.TryGetValue(talentType, out Dictionary<int, GrpcTalentNodeData> talentTreeDic))
        {
            Log.Error($"update talent node failed, can not find talent tree, talent type: {talentType}");
            return;
        }

        if (!talentTreeDic.ContainsKey(newNode.NodeId))
        {
            Log.Error($"update talent node failed, can not find node, node id: {newNode.NodeId}");
            return;
        }

        Log.Info($"update talent node, node id: {newNode.NodeId}");
        GrpcTalentNodeData oldNode = talentTreeDic[newNode.NodeId];
        // talentTreeDic[newNode.NodeId] = newNode;
        // List<GrpcTalentNodeData> talentList = _allTalentTreeList[talentType];
        //后面的节点更新的概率更大，所以从后往前遍历
        // for (int i = talentList.Count - 1; i >= 0; i--)
        // {
        //     if (talentList[i].NodeId == newNode.NodeId)
        //     {
        //         talentList[i] = newNode;
        //         break;
        //     }
        // }

        OnNodeUpdated(oldNode, newNode);
        oldNode.Level = newNode.Level;
    }

    /// <summary>
    /// 新增天赋节点
    /// </summary>
    /// <param name="node"></param>
    /// <param name="talentType"></param>
    public void AddTalentNode(GrpcTalentNodeData node, TalentType talentType)
    {
        if (!AllTalentTreeDic.TryGetValue(talentType, out Dictionary<int, GrpcTalentNodeData> talentTreeDic))
        {
            talentTreeDic = new();
            AllTalentTreeList.Add(talentType, new());
            AllTalentTreeDic.Add(talentType, talentTreeDic);
        }

        if (talentTreeDic.ContainsKey(node.NodeId))
        {
            Log.Error($"add talent node failed, node id: {node.NodeId} already exist");
            return;
        }

        Log.Info($"add talent node, node id: {node.NodeId}");
        talentTreeDic.Add(node.NodeId, node);
        AllTalentTreeList[talentType].Add(node);

        OnNodeAdded(node);
    }

    /// <summary>
    /// 移除天赋节点
    /// </summary>
    /// <param name="nodeID"></param>
    /// <param name="talentType"></param>
    public void RemoveTalentNode(int nodeID, TalentType talentType)
    {
        if (!AllTalentTreeDic.TryGetValue(talentType, out Dictionary<int, GrpcTalentNodeData> talentTreeDic))
        {
            Log.Error($"update talent node failed, can not find talent tree, talent type: {talentType}");
            return;
        }

        Log.Info($"remove talent node, node id: {nodeID}");
        if (!talentTreeDic.Remove(nodeID, out GrpcTalentNodeData removedNode))
        {
            Log.Error("remove talent node failed, can not find node, node id: {nodeID}");
            return;
        }

        List<GrpcTalentNodeData> talentList = AllTalentTreeList[talentType];
        //删除节点从后往前找查询命中率会高一点，因为一般情况下，删除的节点都是最后一个
        for (int i = talentList.Count - 1; i >= 0; i--)
        {
            if (talentList[i].NodeId == nodeID)
            {
                talentList.RemoveAt(i);
                break;
            }
        }

        OnNodeRemoved(removedNode);
    }

    /// <summary>
    /// 当天赋节点更新
    /// </summary>
    /// <param name="oldNode"></param>
    /// <param name="newNode"></param>
    private void OnNodeUpdated(GrpcTalentNodeData oldNode, GrpcTalentNodeData newNode)
    {
        DRTalentTree nodeCfg = _refTalentTreeTable.GetDataRow(newNode.NodeId);
        if (nodeCfg == null)
        {
            Log.Error($"can not find talent tree config, node id: {newNode.NodeId}");
            return;
        }

        if (IsSkillTypeGains(nodeCfg.GainsType))
        {
            int[] addedSkillIDArr = new int[0];
            int[] removedSkillIDArr = new int[0];
            if (newNode.Level > 0)
            {
                addedSkillIDArr = nodeCfg.GainsArgs[newNode.Level - 1];
            }
            if (oldNode.Level > 0)
            {
                removedSkillIDArr = nodeCfg.GainsArgs[oldNode.Level - 1];
            }

            if (removedSkillIDArr != null || addedSkillIDArr != null)
            {
                //处理天赋技能数据变更
                RefEntity.EntityEvent.TalentSkillUpdated?.Invoke(addedSkillIDArr, removedSkillIDArr);
            }
        }
    }

    /// <summary>
    /// 当新增天赋节点
    /// </summary>
    /// <param name="node"></param>
    private void OnNodeAdded(GrpcTalentNodeData node)
    {
        DRTalentTree nodeCfg = _refTalentTreeTable.GetDataRow(node.NodeId);
        if (nodeCfg == null)
        {
            Log.Error($"can not find talent tree config, node id: {node.NodeId}");
            return;
        }

        if (IsSkillTypeGains(nodeCfg.GainsType) && node.Level > 0)
        {
            //处理天赋技能数据变更
            int[] addedSkillIDArr = nodeCfg.GainsArgs[node.Level - 1];
            RefEntity.EntityEvent.TalentSkillUpdated?.Invoke(addedSkillIDArr, new int[0]);
        }
    }

    /// <summary>
    /// 当天赋节点被移除时
    /// </summary>
    /// <param name="node"></param>
    private void OnNodeRemoved(GrpcTalentNodeData node)
    {
        DRTalentTree nodeCfg = _refTalentTreeTable.GetDataRow(node.NodeId);
        if (nodeCfg == null)
        {
            Log.Error($"can not find talent tree config, node id: {node.NodeId}");
            return;
        }

        if (IsSkillTypeGains(nodeCfg.GainsType) && node.Level > 0)
        {
            //处理天赋技能数据变更
            int[] removedSkillIDArr = nodeCfg.GainsArgs[node.Level - 1];
            RefEntity.EntityEvent.TalentSkillUpdated?.Invoke(new int[0], removedSkillIDArr);
        }
    }

    /// <summary>
    /// 判断是否技能类型的收益
    /// </summary>
    /// <param name="gainsType"></param>
    /// <returns></returns>
    private bool IsSkillTypeGains(int gainsType)
    {
        return gainsType is ((int)TalentGainsType.ActiveSkill) or ((int)TalentGainsType.PassiveSkill);
    }
}