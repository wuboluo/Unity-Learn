using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_InterleavedArray
    {
        private static void Main(string[] args)
        {
            #region ---------- 概念

            // 交错数组是数组的数组，每个维度的数量可以不同
            // 注意：二维数组的每行得列数相同，交错数组的每行的列数可以不同

            #endregion


            #region ---------- 声明

            // 1，变量类型[][] 交错数组名
            int[][] arr;

            // 2，变量类型[][] 交错数组名 = new 变量类型[行][]
            int[][] arr2 = new int[2][];

            // 3，变量类型[][] 交错数组名 = new 变量类型[行][]{ 一维数组1,一维数组2,...... }
            int[][] arr3 = new int[3][]
            {
                new[] { 1, 2, 3 },
                new[] { 1, 2 },
                new[] { 1 }
            };

            // 4，变量类型[][] 交错数组名 = new 变量类型[][]{ 一维数组1,一维数组2,...... }
            int[][] arr4 =
            {
                new[] { 1, 2, 3 },
                new[] { 1, 2 }
            };

            // 5，变量类型[][] 交错数组名 = { 一维数组1,一维数组2,...... }
            int[][] arr5 =
            {
                new[] { 1, 2, 3 },
                new[] { 1, 2 }
            };

            #endregion


            #region ---------- 使用

            // 1，长度
            int[][] array =
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5 }
            };

            // 行数
            Debug.Log(array.GetLength(0));
            // 列数（得到某一行的列数）
            Debug.Log(array[0].Length);

            // 2，获取元素
            Debug.Log(array[0][1]); // 2

            // 3，修改元素
            array[0][1] = 99;

            // 4，遍历
            for (int i = 0; i < array.GetLength(0); i++)
                // 交错数组中列的获取，要根据当前行，去获取此行的列数。因为不同行的列数不一定一致。
            for (int j = 0; j < array[i].Length; j++)
                Debug.Log(array[i][j] + " ");
            // 1 99 3
            // 4 5

            #endregion


            // 总结：
            // 1，概念：交错数组可以存储同一类型的m行不确定列的数据 
            // 2，增删查改
            // 3，所有的变量类型都可以声明为交错数组（任何数组都可以）
            // 4，使用不多（可用在 变长数组参数：params int[]）
        }
    }
}