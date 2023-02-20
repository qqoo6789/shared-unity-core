using System.Collections.Generic;
//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System;

namespace Meland.Editor.DataTableTools
{
    public sealed class DataTableGeneratorUtil
    {
        public static string ATTRIBUTE_TYPE_CSV_FILE_NAME = "Assets/Plugins/SharedCore/Res/DataTable/Csv/EntityAttribute.csv";
        public static string ATTRIBUTE_TYPE_FILE_TEMPLATE_NAME = "Assets/Plugins/SharedCore/Src/Editor/DataTableGenerator/Template/AttributeTypeTemplate.txt";
        public static string ATTRIBUTE_TYPE_FILE_NAME = "Assets/Plugins/SharedCore/Src/Runtime/Module/Entity/Attribute/AttributeType.cs";
        public static string SVNCsvPath { get; private set; }
        public static string SVNConfigPath { get; private set; }

        public static void SetPath(string configPath)
        {
            if (configPath is null)
            {
                return;
            }
            SVNConfigPath = configPath;
            SVNCsvPath = configPath + "/csv";
        }

        public static void UpdateCsv()
        {
            string csvPath = Utility.Path.GetRegularPath(DataTableGenerator.DATA_TABLE_CSV_PATH);
            if (Directory.Exists(csvPath))
            {
                Directory.Delete(csvPath, true);
            }
            _ = Directory.CreateDirectory(csvPath);
            DirectoryInfo direction = new(SVNCsvPath);
            FileInfo[] files = direction.GetFiles("*.csv", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string dest = Path.Combine(csvPath, files[i].Name);
                File.Copy(files[i].FullName, dest, true);
            }
        }
        public static void GenerateDataTables()
        {
            //清空bytes目录
            if (Directory.Exists(DataTableGenerator.DATA_TABLE_PATH))
            {
                Directory.Delete(DataTableGenerator.DATA_TABLE_PATH, true);
            }
            _ = Directory.CreateDirectory(DataTableGenerator.DATA_TABLE_PATH);

            string csvPath = Utility.Path.GetRegularPath(DataTableGenerator.DATA_TABLE_CSV_PATH);
            DirectoryInfo direction = new(csvPath);
            FileInfo[] files = direction.GetFiles("*.csv", SearchOption.AllDirectories);
            List<string> tableNames = new();
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i].Name;
                string extension = files[i].Extension;
                string fullName = files[i].FullName;
                string dataTableName = fileName[..^extension.Length];
                tableNames.Add(dataTableName);
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(fullName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
            }
        }

        public static void GenerateDataTableCodes()
        {
            //清空脚本目录
            if (Directory.Exists(DataTableGenerator.CSHARP_CODE_PATH))
            {
                Directory.Delete(DataTableGenerator.CSHARP_CODE_PATH, true);
            }
            _ = Directory.CreateDirectory(DataTableGenerator.CSHARP_CODE_PATH);

            string csvPath = Utility.Path.GetRegularPath(DataTableGenerator.DATA_TABLE_CSV_PATH);
            DirectoryInfo direction = new(csvPath);
            FileInfo[] files = direction.GetFiles("*.csv", SearchOption.AllDirectories);
            List<string> tableNames = new();
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i].Name;
                string extension = files[i].Extension;
                string fullName = files[i].FullName;
                string dataTableName = fileName[..^extension.Length];
                tableNames.Add(dataTableName);
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(fullName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }
            DataTableGenerator.GenerateConfigFile(tableNames.ToArray());
        }

        public static void GenerateAttributeTypeFile()
        {
            if (!File.Exists(ATTRIBUTE_TYPE_CSV_FILE_NAME))
            {
                return;
            }
            try
            {
                string template = File.ReadAllText(ATTRIBUTE_TYPE_FILE_TEMPLATE_NAME, Encoding.UTF8);
                StringBuilder stringBuilder = new(template);
                _ = stringBuilder.Replace("__DATA_TABLE_NAMES__", GeneratorAttributeType());

                using (FileStream fileStream = new(ATTRIBUTE_TYPE_FILE_NAME, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter stream = new(fileStream, Encoding.UTF8))
                    {
                        stream.Write(stringBuilder.ToString());
                    }
                }

                Debug.Log(Utility.Text.Format("Generate AttributeType file '{0}' success.", ATTRIBUTE_TYPE_FILE_NAME));
                return;
            }
            catch (Exception exception)
            {
                Debug.LogError(Utility.Text.Format("Generate AttributeType file '{0}' failure, exception is '{1}'.", ATTRIBUTE_TYPE_FILE_NAME, exception));
                return;
            }
        }
        public static string GeneratorAttributeType()
        {
            string tableText = File.ReadAllText(ATTRIBUTE_TYPE_CSV_FILE_NAME, Encoding.GetEncoding(TableDefine.DATA_TABLE_ENCODING));
            List<string[]> rawValues = CSVSerializer.ParseCSV(tableText);

            StringBuilder stringBuilder = new();

            for (int i = TableDefine.DATA_TABLE_START_ROW; i < rawValues.Count; i++)
            {
                string desc = rawValues[i][^1];
                _ = stringBuilder
                    .AppendLine("    /// <summary>")
                    .AppendLine($"    /** {desc}*/")
                    .AppendLine("    /// <summary>")
                    .AppendLine($"    {rawValues[i][1]} = {rawValues[i][0]},");
            }

            return stringBuilder.ToString();
        }

    }
}
