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
public class DRSkillEffect : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取效果ID。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取效果类型 。*/
    /// </summary>
    public int EffectType
    {
        get;
        private set;
    }

    /// <summary>
  /**获取效果参数。*/
    /// </summary>
    public int[] Parameters
    {
        get;
        private set;
    }

    /// <summary>
  /**获取持续时间 <0为永久 =0为瞬间 >0持续时间。*/
    /// </summary>
    public int Duration
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        _id = int.Parse(columnStrings[index++]);
        EffectType = DataTableParseUtil.ParseInt(columnStrings[index++]);
        index++;
        Parameters = DataTableParseUtil.ParseArray<int>(columnStrings[index++]);
        Duration = DataTableParseUtil.ParseInt(columnStrings[index++]);
        index++;

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                _id = binaryReader.Read7BitEncodedInt32();
                EffectType = binaryReader.Read7BitEncodedInt32();
                Parameters = binaryReader.ReadArray<Int32>();
                Duration = binaryReader.Read7BitEncodedInt32();
            }
        }

        return true;
    }
}

