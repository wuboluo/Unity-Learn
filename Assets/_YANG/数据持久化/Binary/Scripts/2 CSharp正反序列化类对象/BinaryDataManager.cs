using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DataPersistence_Binary
{
    // 目标：实现一个管理类，用于快捷读写自定义类对象数据
    public class BinaryDataManager
    {
        // 数据存储路径
        private static readonly string SavePath = @$"{Application.persistentDataPath}\Data\";
        public static readonly string DataBinaryPath = @$"{Application.streamingAssetsPath}\Binary_配置\";

        public static BinaryDataManager Instance { get; } = new();

        /// 存类对象数据
        public static void Save(object obj, string path)
        {
            if (!Directory.Exists(SavePath)) Directory.CreateDirectory(SavePath);

            using FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, obj);

            fs.Flush();
            fs.Close();

            Process.Start(SavePath);
        }

        /// 获取二进制数据，转换为类对象
        public static T Read<T>(string path) where T : class
        {
            // 判断文件是否存在
            if (!File.Exists(path))
            {
                // 不存在就返回这个泛型对象的默认值，class=null
                return default;
            }

            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            // 文件流形式不需要传入一个 byte数组
            BinaryFormatter bf = new BinaryFormatter();
            T result = bf.Deserialize(fs) as T;
            return result;
        }
    }
}