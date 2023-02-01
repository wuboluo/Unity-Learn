using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DataPersistence_Binary
{
    public class DataEncryption : MonoBehaviour
    {
        [Header("加/解密")] public bool isEncryption;

        // 密钥
        private const byte Key = 101;

        // 二进制文件存储路径
        private const string SavePath = "";

        private Person _person;

        private void Start()
        {
            if (isEncryption) XorEncryption(SavePath);
            else XorDecryption(SavePath);
        }

        /// <summary>
        /// 异或加密
        /// </summary>
        private void XorEncryption(string path)
        {
            _person = new Person();
            using MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, _person);
            byte[] buffers = ms.GetBuffer();

            // 异或加密
            for (int i = 0; i < buffers.Length; i++) buffers[i] ^= Key;
            File.WriteAllBytes(path, buffers);

            ms.Close();
        }

        /// <summary>
        /// 异或解密
        /// </summary>
        private void XorDecryption(string path)
        {
            byte[] buffers = File.ReadAllBytes(path);
            for (int i = 0; i < buffers.Length; i++) buffers[i] ^= Key;

            // 注意：反序列化需要将 byte数组传入
            using MemoryStream ms = new MemoryStream(buffers);
            BinaryFormatter bf = new BinaryFormatter();
            _person = bf.Deserialize(ms) as Person;

            ms.Close();
        }

        #region 何时加密？何时解密？

        // 加密：类对象 ——> 二进制数据 
        // 解密：二进制数据 ——> 类对象

        // 起到数据安全的作用

        #endregion

        #region 加密是否100%安全

        // 提高破解难度而已，不能保证完全安全
        // 通过各种尝试总会破解，时间问题

        #endregion

        #region 常用加密算法

        // MD5
        // SHA1
        // HMAC
        // AES/DES/3DES
        // ...

        #endregion
    }
}