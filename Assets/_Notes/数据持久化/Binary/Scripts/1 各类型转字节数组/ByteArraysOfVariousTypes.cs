using System;
using System.Text;
using UnityEngine;

namespace Yang.DataPersistence.Binary
{
    public class ByteArraysOfVariousTypes : MonoBehaviour
    {
        private void Start()
        {
            #region 知识点一：回顾——不同的变量类型

            // 整数 有符号 sbyte short int long
            // 整数 无符号 byte ushort uint ulong

            // 浮点 float double decimal

            // 特殊 bool char string

            #endregion

            #region 知识点二：回顾——变量的本质

            // 变量的本质是二进制
            // 1 bit(位)不是 0 就是 1
            // 1 byte = 8 bit
            // 也就是一个字节等于8个不是0就是1的数字
            // 通过 sizeof 方法可以看到常用变量类型占用的字节空间长度

            print("sbyte byte:" + sizeof(sbyte) + " " + sizeof(byte)); // 1
            print("short ushort:" + sizeof(short) + " " + sizeof(ushort)); // 2
            print("int uint:" + sizeof(int) + " " + sizeof(uint)); // 4
            print("long ulong:" + sizeof(long) + " " + sizeof(ulong)); // 8

            print("float:" + sizeof(float)); // 4
            print("double:" + sizeof(double)); // 8
            print("decimal:" + sizeof(decimal)); // 16

            print("bool:" + sizeof(bool)); // 1
            print("char:" + sizeof(char)); // 2

            #endregion

            #region 知识点三：二进制文件读写的本质

            // 就是通过将【各类型变量】转换为【字节数组】，并将其直接存到文件中
            // 节约存储空间，提升效率，提升安全性
            // 在网络通信中我们直接传输的数据也是字节数据（也就是二进制数据）

            #endregion

            #region 知识点四：各类型数据和字节数据相互转换

            // C#提供了公共类帮助转化
            // 类名：BitConverter
            // 命名空间：using System                                        

            // 1，各类型转字节
            byte[] bytes = BitConverter.GetBytes(256);

            // 2，字节数组转各类型
            // startIndex：根据类型的字节长度，想要从第几个索引开始转换
            int i = BitConverter.ToInt32(bytes, 0);

            #endregion

            #region 知识点五：标准编码格式

            // 编码是用预先规定的方法将文字、数字或其它对象编成数码，或将信息、数据转换成规定的电脉冲信号
            // 为保证编码的正确性，编码要规范化、标准化，即需有标准的编码格式
            // 常见的编码格式有ASCII、ANSI、GBK、GB2312、UTF-8、GB18030和UNICODE等

            // 计算机中数据的本质就是2进制数据
            // 编码格式就是用对应的2进制数，对应不同的文字
            // 由于世界上有各种不同的语言，所有会有很多种不同的编码格式
            // 不同的编码格式，对应的规则是不同的

            // 如果在读取字符时采用了不统一的编码格式，可能会出现乱码

            // 游戏开发中常用编码格式 UTF-8
            // 中文相关编码格式 GBK
            // 英文相关编码格式 ASCII

            // 在C#中有一个专门的编码格式类 来帮助我们将字符串和字节数组进行转换

            // 类名：Encoding
            // 需要引用命名空间：using System.Text

            // 1，将字符串以指定编码格式转字节
            byte[] bytes2 = Encoding.UTF8.GetBytes("烧银行");

            // 2，字节数组以指定编码格式转字符串
            string s = Encoding.UTF8.GetString(bytes2);
            print(s);

            #endregion
        }
    }
}