using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_OneArray
    {
        private static void Main(string[] args)
        {
            #region ---------- 概念

            // 数组是存储一组相同类型数据的集合
            // 数组分为 一维，多维，交错数组
            // 一般情况，一维数组简称为数组

            #endregion

            #region ---------- 声明

            // 1，变量类型[] 数组名
            // 只是声明了一个数组，但是并没有开房
            int[] arr1;

            // 2，变量类型[] 数组名 = new 变量类型[数组的长度]
            // 相当于开了5个房间，但是房间里的int值默认为0
            int[] arr2 = new int[5];

            // 3，变量类型[] 数组名 = new 变量类型[数组的长度]{内容1,内容2,内容3,......}
            int[] arr3 = { 1, 2, 3, 4, 5 };

            // 4，变量类型[] 数组名 = new 变量类型[]{内容1,内容2,内容3,......}
            int[] arr4 = new[] { 1, 2, 3 };

            // 5，变量类型[] 数组名 = {内容1,内容2,内容3,......}
            int[] arr5 = { 1, 2, 3, 4 };

            bool[] arr6 = new bool[5] { true, false, false, true, false };

            #endregion

            #region ---------- 试用

            int[] array = { 1, 2, 3, 4, 5 };

            // 1，长度
            int length = array.Length;

            // 2，获取元素
            // 数组中的下标或索引，是从0开始
            int firstValue = array[0];

            // 3，修改元素
            array[0] = 99;

            // 4，遍历
            for (int i = 0; i < array.Length; i++) Debug.Log(array[i]);
            foreach (int t in array) Debug.Log(t);

            // 5，增加
            // 数组初始化以后，不能直接添加新的元素
            int[] array2 = new int[6];
            for (int i = 0; i < array.Length; i++) array2[i] = array[i];
            array = array2;
            array2[5] = 999;

            // 6，删除
            int[] array3 = new int[5];
            for (int i = 0; i < array3.Length; i++) array3[i] = array[i];
            array = array3;

            // 7，查找
            // array: 99 2 3 4 5
            // 遍历，判断是否有相等项
            // 试用 Linq Find

            #endregion
        }
    }
}