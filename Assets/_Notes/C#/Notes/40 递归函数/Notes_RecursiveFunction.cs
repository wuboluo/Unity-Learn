using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_RecursiveFunction
    {
        #region ---------- 实例

        // 用递归函数打印 0~10
        private static void Fun(int a)
        {
            if (a > 10) return;

            Debug.Log(a);
            ++a;

            Fun(a);
        }

        #endregion

        private static void Main(string[] args)
        {
            Fun(1);
        }

        #region ---------- 基本概念

        // 函数自己调用自己
        // static void Fun()
        // {
        //     if (false) return;
        //     Fun();
        // }

        // 正确的递归函数
        // 1，必须有结束调用的条件
        // 2，用于条件判断的条件必须改变，能够达到停止的目的

        #endregion
    }
}