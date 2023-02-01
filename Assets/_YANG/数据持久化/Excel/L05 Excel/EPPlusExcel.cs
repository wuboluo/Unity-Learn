using System.IO;
using OfficeOpenXml;
using UnityEditor;
using UnityEngine;

namespace DataPersistence_Binary
{
    public class EPPlusExcel : MonoBehaviour
    {
        private void Start()
        {
            // ReadExcel();

            GenerateExcel();
        }

        private void ReadExcel()
        {
            string filePath = @$"{Application.dataPath}\Data\ExampleExcel.xlsx";

            // 获取Excel文件信息
            FileInfo fileInfo = new FileInfo(filePath);

            // 通过Excel表格的文件信息，打开Excel表格
            using ExcelPackage excelPackage = new ExcelPackage(fileInfo);

            // 获取第一张表（EPPlus的索引从 1 开始）
            ExcelWorksheet firstSheet = excelPackage.Workbook.Worksheets[1];

            // 获得某一个单元格的信息（索引从 1 开始）
            string firstCell = firstSheet.Cells[1, 1].Value.ToString();
            print(firstCell);

            // 将所有单元格信息保存下来。因为另存到了一个新的二维数组，所以索引从 0 开始
            if (firstSheet.Cells.Value is object[,] cells)
            {
                var rowCount = firstSheet.Dimension.End.Row;
                var columnCount = firstSheet.Dimension.End.Column;

                print(cells[0, 0]);
                print(cells[rowCount - 1, columnCount - 1]);
            }
        }

        private void GenerateExcel()
        {
            string newExcelFilePath = @$"{Application.dataPath}\Data\NewExcel.xlsx";

            // 得到文件信息。但是如果这个Excel文件不存在，不会报错，只是不包括Excel文件的信息而已
            FileInfo fileInfo = new FileInfo(newExcelFilePath);

            using ExcelPackage excelPackage = new ExcelPackage(fileInfo);

            // 为Excel文件添加一张表。但是如果这个Excel文件不存在，会先创建一个Excel文件出来，再添加
            excelPackage.Workbook.Worksheets.Add("New Sheet");
            excelPackage.Workbook.Worksheets.Add("XY");

            // 删除一张表
            excelPackage.Workbook.Worksheets.Delete("XY");


            excelPackage.Save();
            AssetDatabase.Refresh();
        }
    }
}