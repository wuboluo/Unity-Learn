using UnityEngine;

namespace Yang.CSharp.Notes.Indexer
{
    public class Notes_Indexer : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("索引器");

            // 索引器使用
            Person p = new Person("JING", 23)
            {
                [0] = new("YANG", 26)
            };
            
            Debug.Log(p[0].Name);
            Debug.Log(p["name"]);
        }
    }
}