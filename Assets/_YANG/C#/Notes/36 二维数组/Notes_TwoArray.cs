using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_TwoArray
    {
        private static void Main(string[] args)
        {
            #region ---------- 概念

            // 二维数组是通过两个下标（索引）来确定元素的数组
            // 两个下标可以理解为 行标和列标
            // 比如矩阵：
            //          1 2 3
            //          4 5 6
            // 可以用二维数据 int[2,3] 表示
            // 好比两行三列的数据集合

            #endregion


            #region ---------- 声明

            // 1，变量类型[,] 变量名，未初始化。数组使用前必须进行初始化
            int[,] arr;

            // 2，变量类型[,] 变量名 = new 变量类型[行,列]
            int[,] arr2 = new int[2, 3];

            // 3，变量类型[,] 变量名 = new 变量类型[行,列]{ {0行内容1，0行内容2,...}, {1行内容1，1行内容2,...} }
            int[,] arr3 = new int[3, 2]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            };

            // 4，变量类型[,] 变量名 = new 变量类型[,]{ {0行内容1，0行内容2,...}, {1行内容1，1行内容2,...} }
            int[,] arr4 =
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };

            // 5，变量类型[,] 变量名 = new { {0行内容1，0行内容2,...}, {1行内容1，1行内容2,...} }
            int[,] arr5 =
            {
                { 1, 2 },
                { 3, 4 }
            };

            #endregion


            #region ---------- 试用

            // 1，长度
            // 要获取行和列分别是多长
            int[,] array =
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };

            // 固定写法
            // 获得行数，使用GetLength(0)，得到：2
            Debug.Log(array.GetLength(0));
            // 获得列数，使用GetLength(1)，得到：3
            Debug.Log(array.GetLength(1));


            // 2，获取元素
            // 索引从0开始，最后一个元素的索引为 length-1
            // 获取n行m列：array[n,m]
            Debug.Log(array[0, 1]); // 2
            Debug.Log(array[1, 2]); // 6


            // 3，修改元素
            array[0, 0] = 99;
            Debug.Log(array[0, 0]); // 99


            // 4，遍历元素
            for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
                // i：行
                // j：列
                Debug.Log(array[i, j]); // 99 2 3 4 5 6


            // 5，增加元素
            int[,] array2 = new int[3, 3];
            for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
                array2[i, j] = array[i, j];

            array = array2;
            array[2, 0] = 7;
            array[2, 1] = 8;
            array[2, 2] = 9;
            // 99 2 3 4 5 6 7 8 9

            // 6，删除元素
            // 同添加

            // 7，查找元素
            // 遍历

            #endregion


            // 游戏中，一般用二维数组来存储：矩阵，地图格子等
        }
    }
}