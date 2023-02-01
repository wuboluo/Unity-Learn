using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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

        // 用于存储所有Excel表数据的容器
        private readonly Dictionary<string, object> _tableDic = new();

        private BinaryDataManager()
        {
        }

        public void InitData()
        {
            LoadTable<PlayerDataContainer, PlayerData>();
            LoadTable<ItemDataContainer, ItemData>();
        }

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

        /// <summary>
        /// 加载Excel表的二进制数据到内存中
        /// </summary>
        /// <typeparam name="TC">容器类名（xxContainer）</typeparam>
        /// <typeparam name="TD">数据结构体类名（xxData）</typeparam>
        private void LoadTable<TC, TD>()
        {
            // ------------------------------ 读取Excel表对应的二进制文件，进行解析
            string binaryFilePath = $"{DataBinaryPath}{typeof(TD).Name}.xy";
            using FileStream fs = File.Open(binaryFilePath, FileMode.Open, FileAccess.Read);

            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();

            // ------------------------------ 记录读取过多少字节
            int index = 0;

            // ------------------------------ 根据存储时的顺序
            // 1，多少行数据
            int rowCount = BitConverter.ToInt32(bytes, index);
            index += 4;

            // 2，key的变量名
            int keyNameLength = BitConverter.ToInt32(bytes, index);
            index += 4;
            string keyName = Encoding.UTF8.GetString(bytes, index, keyNameLength);
            index += keyNameLength;

            // 3，每行的信息
            // 先创建容器类对象，利用反射
            Type containerType = typeof(TC);
            object containerObj = Activator.CreateInstance(containerType);
            Type dataClassType = typeof(TD);

            // 通过反射得到数据结构类所有字段的信息
            FieldInfo[] infos = dataClassType.GetFields();

            for (int i = 0; i < rowCount; i++)
            {
                // 实例化一个数据结构类对象
                object dataClassObj = Activator.CreateInstance(dataClassType);

                // 根据字段类型分别读取
                foreach (FieldInfo info in infos)
                {
                    if (info.FieldType == typeof(int))
                    {
                        // 相当于把二进制数据转为int，然后赋值给对应的字段
                        object value = BitConverter.ToInt32(bytes, index);
                        info.SetValue(dataClassObj, value);
                        index += 4;
                    }
                    else if (info.FieldType == typeof(string))
                    {
                        // 读取字符串字节数组的长度
                        int length = BitConverter.ToInt32(bytes, index);
                        index += 4;

                        // 反射赋值
                        object value = Encoding.UTF8.GetString(bytes, index, length);
                        info.SetValue(dataClassObj, value);
                        index += length;
                    }
                }

                #region 每读取完一行数据，使用反射把这行的数据存入容器的字典中

                // 先得到 containerType 类型中名叫 Dic 的字段信息，再通过 GetValue，获取被Get的对象中 Dic 的引用
                object dicObj = containerType.GetField("Dic").GetValue(containerObj);
                // 得到 Add 方法
                MethodInfo methodInfo = dicObj.GetType().GetMethod("Add");
                // 得到数据结构类对象中 key 的引用/值
                object keyObj = dataClassType.GetField(keyName).GetValue(dataClassObj);
                // 调用方法
                methodInfo?.Invoke(dicObj, new[] { keyObj, dataClassObj });

                #endregion
            }

            // 把读取完的表记录下来
            _tableDic.Add(typeof(TC).Name, containerObj);
        }

        public T GetTable<T>() where T : class
        {
            string tableName = typeof(T).Name;
            if (!_tableDic.ContainsKey(tableName)) return null;

            return _tableDic[tableName] as T;
        }
    }
}