using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Yang.DataPersistence.Binary
{
    // 通过断点快速查看反序列化结果
    public class ClassDeserialization : MonoBehaviour
    {
        public DeSerializeForm form;
        private const string FilePath = "";

        private void Start()
        {
            switch (form)
            {
                case DeSerializeForm.ByFileStream:
                    DeserializeFileByFile(FilePath);
                    break;
                
                case DeSerializeForm.ByMemoryStream:
                    DeserializeFileByMemory(FilePath);
                    break;
            }
        }

        /// <summary>
        /// 使用【文件流】反序列化数据
        /// </summary>
        /// <param name="path">数据文件路径</param>
        /// <returns>自定义类对象</returns>
        private void DeserializeFileByFile(string path)
        {
            using FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            // 反序列化
            Person p = bf.Deserialize(fs) as Person;

            fs.Close();
        }

        /// <summary>
        /// 使用【内存流】反序列化数据
        /// </summary>
        /// <param name="path">数据文件路径</param>
        /// <returns>自定义类对象</returns>
        private void DeserializeFileByMemory(string path)
        {
            byte[] buffers = File.ReadAllBytes(path);

            // 声明内存流对象，一开始就把字节数组传进去
            using MemoryStream ms = new MemoryStream(buffers);
            BinaryFormatter bf = new BinaryFormatter();
            // 反序列化
            Person p = bf.Deserialize(ms) as Person;

            ms.Close();
        }
    }
}