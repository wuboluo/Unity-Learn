using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Yang.DataPersistence.Binary
{
    // 目标：为Student类实现保存和读取二进制文件的方法
    public class CustomClassToBinaryExample : MonoBehaviour
    {
        private void Start()
        {
            Student s = new Student(18, "XY", 1, true);
            s.Save("XY");

            s = Student.Load("XY");
            print(s.Age);
            print(s.Name);
            print(s.Number);
            print(s.Sex);
        }
    }

    public class Student
    {
        public readonly int Age;
        public readonly string Name;
        public readonly int Number;
        public readonly bool Sex;

        private byte[] _buffers;

        public Student()
        {
        }

        public Student(int age, string name, int number, bool sex)
        {
            Age = age;
            Name = name;
            Number = number;
            Sex = sex;
        }

        /// <summary>
        ///     将自定义类按照自定义保存规则，以二进制文件形式保存在本地
        /// </summary>
        /// <param name="filePath">要保存到的位置（例如：folderPath\fileName.yang）</param>
        public void Save(string filePath)
        {
            // 创建指定文件夹
            string folderPath = filePath[..filePath.LastIndexOf(Path.DirectorySeparatorChar)];
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            // 创建一个指定名称的文件，并返回其文件流
            using FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

            // age
            _buffers = BitConverter.GetBytes(Age);
            fs.Write(_buffers, 0, 4);
            // name
            _buffers = Encoding.UTF8.GetBytes(Name);
            fs.Write(BitConverter.GetBytes(_buffers.Length), 0, 4);
            fs.Write(_buffers, 0, _buffers.Length);
            // number
            _buffers = BitConverter.GetBytes(Number);
            fs.Write(_buffers, 0, 4);
            // sex
            _buffers = BitConverter.GetBytes(Sex);
            fs.Write(_buffers, 0, 1);

            fs.Flush();
            fs.Close();

            // 打开文件夹
            Process.Start(folderPath);
        }

        /// <summary>
        ///     加载自定义二进制文件，根据自定义规则解析成自定义类
        /// </summary>
        /// <param name="filePath">文件路径，包含自定义后缀名（例如：folderPath\fileName.yang"）</param>
        /// <returns>自定义类对象</returns>
        public static Student Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.Log("文件不存在");
                return null;
            }

            using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // 把文件中的字节全部读取出来
            byte[] buffers = new byte[fs.Length];
            int read = fs.Read(buffers, 0, buffers.Length);
            fs.Close();

            // 依次读取内容
            int index = 0;

            // age
            int age = BitConverter.ToInt32(buffers, index);
            index += 4;
            // name
            int nameLength = BitConverter.ToInt32(buffers, index);
            index += 4;
            string name = Encoding.UTF8.GetString(buffers, index, nameLength);
            index += nameLength;
            // number
            int number = BitConverter.ToInt32(buffers, index);
            index += 4;
            // sex
            bool sex = BitConverter.ToBoolean(buffers, index);

            return new Student(age, name, number, sex);
        }
    }
}