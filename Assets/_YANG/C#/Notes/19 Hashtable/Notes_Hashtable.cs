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
    }
}