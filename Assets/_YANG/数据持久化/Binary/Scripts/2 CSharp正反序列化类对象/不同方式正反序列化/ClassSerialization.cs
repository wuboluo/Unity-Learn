using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DataPersistence_Binary
{
    public class ClassSerialization : MonoBehaviour
    {
        public DeSerializeForm form;
        private const string FilePath = "";

        private void Start()
        {
            Person p = new Person();

            switch (form)
            {
                case DeSerializeForm.ByFileStream:
                    SerializeFileByFile(p, FilePath);
                    break;

                case DeSerializeForm.ByMemoryStream:
                    SerializeFileByMemory(p, FilePath);
                    break;
            }
        }

        /// <summary>
        /// 使用【文件流】序列化数据。主要用于存储到文件中
        /// </summary>
        /// <param name="p">被序列化的类对象</param>
        /// <param name="filePath">二进制文件保存路径</param>
        private static void SerializeFileByFile(Person p, string filePath)
        {
            // 声明一个文件流
            using FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            // 声明一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            // 序列化对象生成二进制字节数组，并写入到文件流当中，相当于直接写入文件
            bf.Serialize(fs, p);

            fs.Flush();
            fs.Close();
        }

        /// <summary>
        /// 使用【内存流】序列化数据。用于得到字节数组，可用于网络传输
        /// </summary>
        /// <param name="p">被序列化的类对象</param>
        /// <param name="filePath">二进制文件保存路径</param>
        private static void SerializeFileByMemory(Person p, string filePath)
        {
            // 1，内存流对象（System.IO - MemoryStream）
            // 2，二进制格式化对象（System.Runtime.Serialization.Formatters.Binary - BinaryFormatter）

            // 声明一个内存流
            using MemoryStream ms = new MemoryStream();
            // 声明一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            // 序列化对象生成二进制字节数组，并写入到内存流当中
            bf.Serialize(ms, p);
            // 对象的二进制字节数组
            byte[] buffers = ms.GetBuffer();
            // 存储字节
            File.WriteAllBytes(filePath, buffers);

            ms.Close();
        }
    }
}