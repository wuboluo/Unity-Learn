using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_SelectionSort
    {
        private static void Main(string[] args)
        {
            #region 选择排序的基本原理

            // 8 7 1 5 4 2 6 3 9
            // 新建中间商
            // 依次比较
            // 找出机值（最大或最小）
            // 放入目标位置
            // 比较m轮

            #endregion

            #region 代码实现

            int[] arr = new[] { 8, 7, 1, 5, 4, 2, 6, 3, 9 };

            // 第五步：比较m轮
            for (int m = 0; m < arr.Length; m++)
            {
                // 第一步：声明一个中间商，来记录索引
                // 每一轮开始，默认第一个都是极值
                int index = 0;

                // 第二步：依次比较
                // -m：已经确定位置的没有必要再去判断了，同冒泡-m原理
                for (int n = 0; n < arr.Length - m; n++)
                    // 第三步：找出极值（最大值）
                    if (arr[index] < arr[n])
                        index = n;

                // 第四步：放入目标位置（length-1-轮数）
                // 如果当前极值就是目标位置，那就没必要交换，所以要 '!='
                int targetIndex = arr.Length - 1 - m;
                if (index != targetIndex) (arr[index], arr[targetIndex]) = (arr[targetIndex], arr[index]);
            }

            foreach (int i in arr) Debug.Log(i);

            #endregion

            // 默写：

            // var arr2 = new[] { 4, 2, 1, 6, 8, 3, 9, 0 };
            // for (var m = 0; m < arr2.Length; m++)
            // {
            //     var index = 0;
            //     for (var n = 0; n < arr2.Length - m; n++)
            //     {
            //         if (arr2[index] < arr2[n])
            //         {
            //             index = n;
            //         }
            //     }
            //
            //     var targetIndex = arr2.Length - 1 - m;
            //     if (index != targetIndex)
            //     {
            //         (arr2[index], arr2[targetIndex]) = (arr2[targetIndex], arr2[index]);
            //     }
            // }
            //
            // foreach (var i in arr2)
            // {
            //     Debug.Log(i);
            // }
        }
    }
}