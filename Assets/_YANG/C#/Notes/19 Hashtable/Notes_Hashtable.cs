using System.Collections;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_Hashtable
    {
        private static void Main(string[] args)
        {
            // -------------------------------------------------- Hashtable的本质
            // Hashtable 又称散列表，是基于键的哈希代码组织起来的 键值对
            // 主要作用是提高数据查询的效率
            // 使用键来访问集合中的元素


            // -------------------------------------------------- 声明，增删查改
            Hashtable hashtable = new Hashtable();

            // 增
            // 注意：不能出现相同的键
            hashtable.Add(1, "123");
            hashtable.Add("123", 2);
            hashtable.Add(true, false);


            // 删
            // 1，只能通过 key 去删除
            hashtable.Remove(1);
            // 2，删除不存在的键，没反应
            hashtable.Remove(2);
            // 3，清空
            hashtable.Clear();


            // 查
            // 1，通过 key 查看 value，找不到返回null
            Debug.Log(hashtable[1]); // 123
            Debug.Log(hashtable[4]); // null

            // 2，查看是否存在
            // 根据 key
            Debug.Log(hashtable.Contains(1));
            Debug.Log(hashtable.ContainsKey(1));
            // 根据 value
            Debug.Log(hashtable.ContainsValue(1));


            // 改
            // 只能修改 value，key需要通过add
            hashtable[1] = 100;


            // -------------------------------------------------- 遍历
            // 1，遍历所有 key
            foreach (object k in hashtable.Keys)
            {
                Debug.Log("key: " + k);
                Debug.Log("Value: " + hashtable[k]);
            }

            // 2，遍历所有 value
            foreach (object v in hashtable.Values) Debug.Log("Value: " + v);

            // 3，遍历所有 key_value
            foreach (DictionaryEntry item in hashtable)
            {
                Debug.Log("key: " + item.Key);
                Debug.Log("Value: " + item.Value);
            }

            // 4，迭代器
            IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
            bool flag = enumerator.MoveNext();
            while (flag)
            {
                Debug.Log("key: " + enumerator.Key);
                Debug.Log("Value: " + enumerator.Value);

                flag = enumerator.MoveNext();
            }
        }
        
        
        
        // 字典的原理：
        
        // 1.哈希计算：
        // 当你插入一个键值对时，字典首先使用哈希函数计算键的哈希码。这个哈希码决定了键值对在内部数组中的位置。
        
        // 2.解决冲突：
        // 如果两个键产生了相同的哈希码或者哈希函数映射到同一索引，字典使用某种冲突解决策略，如链表法（在冲突位置存储一个链表的元素）。
        
        // 3.动态扩展：
        // 随着更多元素的添加，为了保持操作的效率，字典可能需要动态扩展其内部数组的大小。这涉及到创建一个更大的数组，并重新计算每个元素的位置（重新哈希）。
        
        // 4.键查找：
        // 当检索一个值时，字典计算键的哈希码，直接访问内部数组的相应位置。如果存在哈希冲突，它会遍历链表（或使用其他冲突解决策略）以找到正确的元素。

        // 为了确保字典保持高性能，很重要的一点是保持一个低装载因子，这通常通过保证数组有足够的空间来减少冲突的可能性来实现。
        // 装载因子是当前存储的元素数与数组容量的比率。当装载因子超过某个阈值时，字典会增加内部数组的大小。
        // Dictionary<TKey, TValue> 还是类型安全的，这意味着它可以防止类型错误，并且在编译时强制类型匹配，这有助于消除许多运行时错误。
    }
}