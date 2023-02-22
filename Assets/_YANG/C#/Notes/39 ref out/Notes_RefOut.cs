using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_RefOut
    {
        // 总结：
        // 1，ref和out 作用：解决值类型和引用类型在函数内部 改值或重新声明 能够影响外部传入的变量，让其也被修改
        // 2，使用上：在声明参数的时候，在类型前加上 ref和out关键字，使用时同样
        // 3，区别：
        //          ref 传入的变量必须初始化，内部可改可不改
        //          out 传入的变量可以不初始化（会默认认为没有初始化），内部必须赋值


        private static void Main(string[] args)
        {
            #region 使用

            int a = 1;
            ChangeValue(a);
            Debug.Log(a); // 1
            ChangeValueRef(ref a);
            Debug.Log(a); // 3
            ChangeValueOut(out a);
            Debug.Log(a); // 99

            Debug.Log("--------------------");

            int[] arr = { 1, 2, 3 };
            ChangeValue(arr);
            Debug.Log(arr[0]); // 99

            Debug.Log("--------------------");

            ChangeArray(arr);
            Debug.Log(arr[0]); // 99
            ChangeArrayRef(ref arr);
            Debug.Log(arr[0]); // 100
            ChangeArrayOut(out arr);
            Debug.Log(arr[0]); // 999

            #endregion

            #region 区别

            int aa;
            // ChangeValueRef(ref aa);  // 报错
            ChangeValueOut(out aa);

            #endregion
        }

        #region ---------- 原因

        // ref 和 out，可以解决在函数内部改变外部传入的内容，里面改变了，外面也要变
        private static void ChangeValue(int value)
        {
            value = 3;
        }

        private static void ChangeValue(int[] arr)
        {
            arr[0] = 99;
        }

        private static void ChangeArray(int[] array)
        {
            // 相当于重新声明了一个数组
            array = new[] { 10, 20, 30 };
        }

        #endregion

        #region ---------- 使用

        // 函数参数的修饰符
        // 当传入的值参数在内部修改时，或引用类型参数在内部重新声明时
        // 希望外部的值也发生变化

        // ref
        private static void ChangeValueRef(ref int value)
        {
            value = 3;
        }

        private static void ChangeArrayRef(ref int[] arr)
        {
            arr = new[] { 100, 200, 300 };
        }

        // out
        private static void ChangeValueOut(out int value)
        {
            value = 99;
        }

        private static void ChangeArrayOut(out int[] arr)
        {
            arr = new[] { 999, 888, 777 };
        }

        #endregion

        #region ---------- 区别

        // 1，ref 传入的变量必须初始化，out 不用
        // 2，out 传入的变量必须在内部赋值，ref 不用

        #endregion


        #region ---------- 关于重载

        // 相同参数，但是分别被ref和out 修饰后，不能成为重载
        // 但是可以和未被修饰的参数成为重载

        private static void Func(ref float f)
        {
        }

        // static void Func(out float f)
        // {
        //     f = 1f;
        // }

        private static void Func(float f)
        {
        }

        #endregion
    }
}