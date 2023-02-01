using System;
using System.Data;
using System.IO;
using System.Text;
using Excel;
using UnityEditor;
using UnityEngine;

namespace DataPersistence_Binary
{
    // 需访问Editor文件夹下得dll，需要将此脚本也放入Editor文件夹中
    public static class ExcelReader
    {
        // 1，FileStream：读取文件流
        // 2，IExcelDataReader：从流中读取Excel数据
        // 3，DataSet：数据集合类，将Excel数据转存进其中方便读取
        // 4，DataTable：数据表类，标识Excel文件中的一个表
        // 5，DataRow：数据行类，表示某张表中的一行数据

        private static readonly string excelFilePath = @$"{Application.dataPath}\Data\Data.xlsx";

        private static readonly string dataClassPath = @$"{Application.dataPath}\Scripts\ExcelData\Data\";
        private static readonly string dataContainerPath = @$"{Application.dataPath}\Scripts\ExcelData\Container\";

        // 具体的内容开始的行号
        private const int realContentRowIndex = 5;

        [MenuItem("XY/Read Excel")]
        private static void ReadExcel()
        {
            using FileStream fs = File.Open(excelFilePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);

            // 文件中所有表的信息
            DataTableCollection tableCollection = excelReader.AsDataSet().Tables;

            foreach (DataTable t in tableCollection)
            {
                Debug.Log(t.TableName);

                // 生成数据结构类
                GenerateExcelDataClass(t);
                // 生成容器类
                GenerateExcelContainer(t);
                // 生成二进制数据
                GenerateExcelBinary(t);
            }

            // 刷新Project窗口
            AssetDatabase.Refresh();

            fs.Close();
        }

        // 生成对应数据类
        private static void GenerateExcelDataClass(DataTable table)
        {
            DataRow nameRow = GetVariableNameRow(table);
            DataRow typeRow = GetVariableTypeRow(table);

            if (!Directory.Exists(dataClassPath)) Directory.CreateDirectory(dataClassPath);

            // 编辑内容
            string content = "public class " + table.TableName + "\n{\n";
            for (var i = 0; i < table.Columns.Count; i++) content += $"    public {typeRow[i]} {nameRow[i]};\n";
            content += "}";

            // 写入文件
            File.WriteAllText($"{dataClassPath}{table.TableName}.cs", content);
        }

        // 生成对应数据容器类
        private static void GenerateExcelContainer(DataTable table)
        {
            if (!Directory.Exists(dataContainerPath)) Directory.CreateDirectory(dataContainerPath);

            // 编辑内容（key使用第一列的内容）
            string content = "using System.Collections.Generic;\n\n";
            content += "public class " + table.TableName + "Container" + "\n{\n";
            content += $"    public Dictionary<{table.Rows[1][0]}, {table.TableName}> dic = new();";
            content += "\n}";

            // 写入文件
            File.WriteAllText($"{dataContainerPath}{table.TableName}Container.cs", content);
        }

        // 生成Excel二进制数据
        private static void GenerateExcelBinary(DataTable table)
        {
            if (!Directory.Exists(BinaryDataManager.DataBinaryPath)) Directory.CreateDirectory(BinaryDataManager.DataBinaryPath);

            // 创建二进制文件写入
            using FileStream fs = new FileStream($"{BinaryDataManager.DataBinaryPath}{table.TableName}.xy", FileMode.OpenOrCreate, FileAccess.Write);

            // 1，需要保存多少行数据
            fs.Write(BitConverter.GetBytes(table.Rows.Count - 4), 0, 4); // -4：前4行不算数据内容，不进行保存

            // 2，存入key的变量名（默认使用第一行第一列的内容，key/name）
            byte[] bytes = Encoding.UTF8.GetBytes(GetVariableNameRow(table)[0].ToString());
            fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
            fs.Write(bytes, 0, bytes.Length);

            // 3，遍历所有内容的行，进行二进制的写入
            DataRow rowType = GetVariableTypeRow(table);

            for (var i = realContentRowIndex - 1; i < table.Rows.Count; i++)
            {
                for (var k = 0; k < table.Columns.Count; k++)
                {
                    // 每个格子的内容（实际数据范围的格子）
                    string gridData = table.Rows[i][k].ToString();

                    // 根据该列的数据类型，进行不同存储
                    switch (rowType[k].ToString())
                    {
                        case "int":
                            fs.Write(BitConverter.GetBytes(int.Parse(gridData)), 0, 4);
                            break;

                        case "string":
                            bytes = Encoding.UTF8.GetBytes(gridData);
                            fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                    }
                }
            }

            fs.Flush();
            fs.Close();
        }

        // 获得变量名所在行（第一行）
        private static DataRow GetVariableNameRow(DataTable table) => table.Rows[0];

        // 获得变量类型所在行（第二行）
        private static DataRow GetVariableTypeRow(DataTable table) => table.Rows[1];
    }
}