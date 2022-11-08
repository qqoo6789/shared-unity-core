//------------------------------------------------------------
// 此文件由工具自动生成
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;


/// <summary>
/** __DATA_TABLE_COMMENT__*/
/// </summary>
public class DRBuilding : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取骨架资源。*/
    /// </summary>
    public string ArmatureRes
    {
        get;
        private set;
    }

    /// <summary>
  /**获取每小时消耗电量。*/
    /// </summary>
    public int PowerCostPerHour
    {
        get;
        private set;
    }

    /// <summary>
  /**获取随机奖励list。*/
    /// </summary>
    public int[][] RewardList
    {
        get;
        private set;
    }

    /// <summary>
  /**获取最大产出。*/
    /// </summary>
    public int MaxCanHarvest
    {
        get;
        private set;
    }

    /// <summary>
  /**获取最大可偷取。*/
    /// </summary>
    public int MaxCanCollect
    {
        get;
        private set;
    }

    /// <summary>
  /**获取每次偷取比例。*/
    /// </summary>
    public string Stolenpercentage
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        ArmatureRes = columnStrings[index++];
        PowerCostPerHour = DataTableParseUtil.ParseInt(columnStrings[index++]);
        RewardList = DataTableParseUtil.ParseArrayList<int>(columnStrings[index++]);
        MaxCanHarvest = DataTableParseUtil.ParseInt(columnStrings[index++]);
        MaxCanCollect = DataTableParseUtil.ParseInt(columnStrings[index++]);
        Stolenpercentage = columnStrings[index++];

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                ArmatureRes = binaryReader.ReadString();
                PowerCostPerHour = binaryReader.Read7BitEncodedInt32();
                RewardList = binaryReader.ReadArrayList<Int32>();
                MaxCanHarvest = binaryReader.Read7BitEncodedInt32();
                MaxCanCollect = binaryReader.Read7BitEncodedInt32();
                Stolenpercentage = binaryReader.ReadString();
            }
        }

        return true;
    }
}

