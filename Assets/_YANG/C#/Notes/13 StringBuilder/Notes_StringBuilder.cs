using System.Text;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_StringBuilder
    {
        private static void Main(string[] args)
        {
            // string 是特殊的引用类型
            // 每次重新赋值或者拼接的时候会分配新的内存空间
            // 如果一个字符串经常改变会非常浪费空间


            // StringBuilder
            // C#提供的一个用于处理字符串的公共类
            // 主要解决：修改字符串而不创建新的对象，需要频繁修改和拼接的字符串可以使用它，可以提升性能
            // 使用前，需要引入命名空间


            StringBuilder strb = new StringBuilder("123");

            // 设置容量为 64
            StringBuilder strb2 = new StringBuilder("123", 64);

            // StringBuilder存在一个容量的问题，每次往里面增加时，会自动扩容
            // 不是不产生垃圾，而是相比 string来说，要少得多
            // string是在每次修改产生垃圾，StringBuilder是在扩容的时候产生垃圾

            // 获得容量
            Debug.Log(strb.Capacity); // 16
            // 获得字符长度
            Debug.Log(strb.Length); // 3


            Debug.Log("--------------------");

            // 增加
            strb.Append("4444");
            Debug.Log(strb);
            Debug.Log(strb.Length);
            Debug.Log(strb.Capacity);

            strb.AppendFormat("{0}{1}", "aaaaa", "bbbbb");
            Debug.Log(strb);
            Debug.Log(strb.Length);
            Debug.Log(strb.Capacity);

            // 插入
            strb.Insert(0, "羊");

            // 移除
            strb.Remove(0, 10);

            // 清空
            strb.Clear();

            // 查找
            Debug.Log(strb2[1]);

            // 修改
            strb2[0] = 'a';
            Debug.Log(strb2);

            // 替换
            strb2.Replace("a", "羊");
            Debug.Log(strb2);

            // 重新赋值
            strb2.Clear();
            strb2.Append("1223122");
            Debug.Log(strb2);
        }
    }
}