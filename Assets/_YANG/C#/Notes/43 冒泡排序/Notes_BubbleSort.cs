using UnityEngine;

namespace Yang.CSharp.Notes

{
    internal class Notes_BubbleSort
    {
        private static void Main(string[] args)
        {
            #region 排序的基本概念

            // 排序是计算机内常进行的一种操作，其目的是将一组'无序'的记录序列调整为'有序'的记录序列
            // 常用的序列例子
            // 8 7 1 5 4 2 6 3 9
            // 将上面这个无序序列变为有序（升序或降序）的过程

            // 在程序中，序列一般存储在数组中。所以，排序往往是对数组进行排序
            int[] arr = { 8, 7, 1, 5, 4, 2, 6, 3, 9 };

            #endregion


            #region 冒泡排序的基本原理

            // 两两相邻
            // 不停比较
            // 不停交换
            // 交换n轮

            #endregion


            #region 代码实现

            for (int m = 0; m < arr.Length; m++)
            {
                // 优化2：
                // 当未完成所有轮循环前，就已经达到了最终的序列时，就不用继续了
                // 声明一个标识，来表示该轮是否进行了交换
                // 每一轮开始时，默认没有进行交换
                bool isSort = false;

                // 第一步：
                // 如何比较数组中两两相邻的数
                // 从头开始，第n个数和第n+1个数 比较

                // 优化1：
                // '-m':确定了一轮后，极值（最大或最小）已经放到了对应的位置（往后放）
                // 所以，每完成n轮，后面位置的数，就不用参与比较了
                for (int n = 0; n < arr.Length - 1 - m; n++)
                    // 如果 第n个数 大于 第n+1个数，那么就要交换位置
                    if (arr[n] > arr[n + 1])
                    {
                        // =true 意味着实现了一次交换
                        isSort = true;

                        // 第二步：交换位置
                        // 使用元组代替传统的 temp临时值
                        (arr[n], arr[n + 1]) = (arr[n + 1], arr[n]);
                    }

                // 当一轮结束后，如果 isSort这个标识还是 false的话，则意味着已经是最终的序列了，无需继续判断了
                if (!isSort) break;
            }

            foreach (int t in arr) Debug.Log(t);

            #endregion

            // 总结
            // 基本概念：两两相邻，不停比较，不停交换，交换m轮
            // 套路写法：两层循环（外层轮数，内层比较），两值比较，满足交换
            // 如何优化：比过不比，加入bool（完成时跳出结束）


            // 默写：

            // var numbers = new int[] { 2, 4, 3, 6, 8, 3, 7 };
            //
            // for (var m = 0; m < numbers.Length; m++)
            // {
            //     var isSort = false;
            //     for (var n = 0; n < numbers.Length - 1 - m; n++)
            //     {
            //         if (numbers[n] > numbers[n + 1])
            //         {
            //             isSort = true;
            //             (numbers[n], numbers[n + 1]) = (numbers[n + 1], numbers[n]);
            //         }
            //     }
            //
            //     if (!isSort) break;
            // }
            //
            // foreach (var number in numbers)
            // {
            //     Debug.Log(number);
            // }
        }
    }
}