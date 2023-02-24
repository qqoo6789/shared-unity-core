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
public class DRScene : DataRowBase
{
    private int _id = 0;

    /// <summary>
    /// /**获取id-int。*/
    /// </summary>
    public override int Id => _id;

    /// <summary>
  /**获取coolingTime-int。*/
    /// </summary>
    public int CoolingTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取dailyUpper-int。*/
    /// </summary>
    public int DailyUpper
    {
        get;
        private set;
    }

    /// <summary>
  /**获取defaultTemp-int。*/
    /// </summary>
    public int DefaultTemp
    {
        get;
        private set;
    }

    /// <summary>
  /**获取noOperationTime-int。*/
    /// </summary>
    public int NoOperationTime
    {
        get;
        private set;
    }

    /// <summary>
  /**获取playerTalkMan-string[]。*/
    /// </summary>
    public string[] PlayerTalkMan
    {
        get;
        private set;
    }

    /// <summary>
  /**获取playerTalkWomen-string[]。*/
    /// </summary>
    public string[] PlayerTalkWomen
    {
        get;
        private set;
    }

    /// <summary>
  /**获取scene-string。*/
    /// </summary>
    public string Scene
    {
        get;
        private set;
    }

    /// <summary>
  /**获取sceneCell-string。*/
    /// </summary>
    public string SceneCell
    {
        get;
        private set;
    }

    /// <summary>
  /**获取sceneMusic-string[]。*/
    /// </summary>
    public string[] SceneMusic
    {
        get;
        private set;
    }

    /// <summary>
  /**获取sceneSpecialEffect-int。*/
    /// </summary>
    public int SceneSpecialEffect
    {
        get;
        private set;
    }

    /// <summary>
  /**获取weatherEndRate-int。*/
    /// </summary>
    public int WeatherEndRate
    {
        get;
        private set;
    }

    /// <summary>
  /**获取weatherMusic-string。*/
    /// </summary>
    public string WeatherMusic
    {
        get;
        private set;
    }

    /// <summary>
  /**获取weatherStartRate-int。*/
    /// </summary>
    public int WeatherStartRate
    {
        get;
        private set;
    }

    /// <summary>
  /**获取weatherType-int。*/
    /// </summary>
    public int WeatherType
    {
        get;
        private set;
    }

    /// <summary>
  /**获取otherName-string。*/
    /// </summary>
    public string OtherName
    {
        get;
        private set;
    }

    /// <summary>
  /**获取remark-string。*/
    /// </summary>
    public string Remark
    {
        get;
        private set;
    }

    public override bool ParseDataRow(string dataRowString, object userData)
    {
        string[] columnStrings = CSVSerializer.ParseCSVCol(dataRowString);

        int index = 0;
        CoolingTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        DailyUpper = DataTableParseUtil.ParseInt(columnStrings[index++]);
        DefaultTemp = DataTableParseUtil.ParseInt(columnStrings[index++]);
        _id = int.Parse(columnStrings[index++]);
        NoOperationTime = DataTableParseUtil.ParseInt(columnStrings[index++]);
        PlayerTalkMan = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        PlayerTalkWomen = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        Scene = columnStrings[index++];
        SceneCell = columnStrings[index++];
        SceneMusic = DataTableParseUtil.ParseArray<string>(columnStrings[index++]);
        SceneSpecialEffect = DataTableParseUtil.ParseInt(columnStrings[index++]);
        WeatherEndRate = DataTableParseUtil.ParseInt(columnStrings[index++]);
        WeatherMusic = columnStrings[index++];
        WeatherStartRate = DataTableParseUtil.ParseInt(columnStrings[index++]);
        WeatherType = DataTableParseUtil.ParseInt(columnStrings[index++]);
        OtherName = columnStrings[index++];
        Remark = columnStrings[index++];

        return true;
    }


    public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
    {
        using (MemoryStream memoryStream = new(dataRowBytes, startIndex, length, false))
        {
            using (BinaryReader binaryReader = new(memoryStream, Encoding.UTF8))
            {
                CoolingTime = binaryReader.Read7BitEncodedInt32();
                DailyUpper = binaryReader.Read7BitEncodedInt32();
                DefaultTemp = binaryReader.Read7BitEncodedInt32();
                _id = binaryReader.Read7BitEncodedInt32();
                NoOperationTime = binaryReader.Read7BitEncodedInt32();
                PlayerTalkMan = binaryReader.ReadArray<String>();
                PlayerTalkWomen = binaryReader.ReadArray<String>();
                Scene = binaryReader.ReadString();
                SceneCell = binaryReader.ReadString();
                SceneMusic = binaryReader.ReadArray<String>();
                SceneSpecialEffect = binaryReader.Read7BitEncodedInt32();
                WeatherEndRate = binaryReader.Read7BitEncodedInt32();
                WeatherMusic = binaryReader.ReadString();
                WeatherStartRate = binaryReader.Read7BitEncodedInt32();
                WeatherType = binaryReader.Read7BitEncodedInt32();
                OtherName = binaryReader.ReadString();
                Remark = binaryReader.ReadString();
            }
        }

        return true;
    }
}

