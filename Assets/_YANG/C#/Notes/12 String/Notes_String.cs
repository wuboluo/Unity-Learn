using System;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_String
    {
        private static void Main(string[] args)
        {
            // -------------------------------------------------- 字符串指定位置获取
            // 字符串本质是 char数组
            string str = "Yang";
            Debug.Log(str[0]); // 索引器

            // 转为 char数组
            char[] arr = str.ToCharArray();

            // -------------------------------------------------- 正向查找字符位置
            str = "XY";
            int index = str.IndexOf("X", StringComparison.Ordinal);
            Debug.Log(index); // 0

            // -------------------------------------------------- 反向查找字符位置
            str = "XYXY";
            index = str.LastIndexOf("XY", StringComparison.Ordinal);
            Debug.Log(index); // 2

            // -------------------------------------------------- 移除指定位置后的字符
            str = "123456";
            string res = str.Remove(1, 1);
            string res2 = str.Remove(1);
            Debug.Log(res); // 13456
            Debug.Log(res2); // 1

            // -------------------------------------------------- 字符串截取
            str = "123456";
            string subRes = str.Substring(1);
            string subRes2 = str.Substring(1, 1);
            Debug.Log(subRes); // 23456
            Debug.Log(subRes2); // 2
        }
    }
}