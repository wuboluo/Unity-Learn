using System;
using System.Collections;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Yang.FileFolderOperation
{
    public class OperationFileStream : MonoBehaviour
    {
        // 什么是文件流？

        // File只能整体读写文件
        // FileStream可以以读写字节的形式处理文件

        // 文件里面存储的数据就像是一条数据流（数组或列表）
        // 可以通过FileStream以流的形式逐个读写
        // 例如：先存一个int(4个字节)，再存一个bool(1个字节)，再存一个string(n个字节)

        
        
        /// <summary>
        /// 打开或创建指定文件——new FileStream
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private void OpenOrCreateFile_New(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            // arg1：路径
            // arg2：打开模式
            //      CreateNew：创建新文件。文件存在，报错
            //      Create：创建文件。文件存在，覆盖
            //      Open：打开文件。文件不存在，报错
            //      OpenOrCreate：存在打开，不存在创建
            //      Append：文件存在，打开文件并查找文件尾。或创建一个新文件
            //      Truncate：打开并清空文件内容
            // arg3：访问模式
            //      Read：只读
            //      Write：只写
            //      ReadWrite：可读可写
            // arg4：共享权限
            //      None：谢绝共享
            //      Read：允许其它程序读取该文件
            //      Write：允许其它程序写入该文件
            //      ReadWrite：允许其它程序读写该文件
        }

        /// <summary>
        /// 打开或创建指定文件——File.Create
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private void OpenOrCreateFile_Create(string filePath)
        {
            FileStream fs = File.Create(filePath, 2048, FileOptions.None);
            // arg1：路径
            // arg2：缓存
            // arg3：描述如何创建或覆盖该文件（不常用）
            //      Asynchronous：可用于异步读写
            //      DeleteOnClose：不再使用时自动删除
            //      Encrypted：加密
            //      None：不应用其他选项
            //      RandomAccess：随机访问文件
            //      SequentialScan：从头到尾顺序访问文件
            //      WriteThrough：通过中间缓存直接写入磁盘
        }

        /// <summary>
        /// 打开或创建指定文件——File.Open
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private void OpenOrCreateFile_Open(string filePath)
        {
            FileStream fs = File.Open(filePath, FileMode.OpenOrCreate);
        }

        /// <summary>
        /// 写入文件（举例：依次写入一个int和一个string）
        /// </summary>
        /// <param name="filePath">文件位置</param>
        private void Write(string filePath)
        {
            const int content1 = 100;
            const string content2 = "ABC";

            using FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] bytes = BitConverter.GetBytes(content1);

            // int直接写入
            fs.Write(bytes, 0, bytes.Length);

            // string写入时，先写入长度，再写入具体内容
            byte[] bytes2 = Encoding.UTF8.GetBytes(content2);
            fs.Write(BitConverter.GetBytes(bytes2.Length), 0, 4);
            fs.Write(bytes2, 0, bytes2.Length);

            // 将字节写入文件后务必要执行，防止文件内容丢失（内容先保存至内存，执行后才真正全部保存至磁盘）
            fs.Flush();
            // 关闭流
            fs.Close();
            // 缓存资源销毁回收
            fs.Dispose();
        }

        /// <summary>
        /// 依次读取（举例：依次读取一个int和一个string）
        /// </summary>
        /// <param name="filePath"></param>
        private void SequenceRead(string filePath)
        {
            using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // 1，读取一个整形
            byte[] bytes = new byte[4];

            // indexMoveCount：当前流索引前进了几个位置
            int indexMoveCount = fs.Read(bytes, 0, 4);
            int i = BitConverter.ToInt32(bytes, 0);
            print($"读取出来的第一个int：{i}");
            print($"读取完int，索引向前移动 {indexMoveCount} 个位置");

            // 2，读取一个字符串
            indexMoveCount = fs.Read(bytes, 0, 4);
            print($"读取string的长度，索引向前移动 {indexMoveCount} 个位置");
            int length = BitConverter.ToInt32(bytes, 0);
            bytes = new byte[length];
            indexMoveCount = fs.Read(bytes, 0, length);
            print($"读取string本身，索引向前移动 {indexMoveCount} 个位置");
            string resultStr = Encoding.UTF8.GetString(bytes);

            print(resultStr);
        }

        /// <summary>
        /// 一次性读取再挨个读取
        /// </summary>
        /// <param name="filePath"></param>
        private void GlobalRead(string filePath)
        {
            using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // 一开始就声明一个和文件字节数总长度的byte数组
            byte[] bytes4 = new byte[fs.Length];
            int readBytesNumber = fs.Read(bytes4, 0, (int)fs.Length);

            // 读取int
            print(BitConverter.ToInt32(bytes4, 0));

            // 读取string
            int stringLength = BitConverter.ToInt32(bytes4, 4);
            print(Encoding.UTF8.GetString(bytes4, 8, stringLength));
        }
    }
}