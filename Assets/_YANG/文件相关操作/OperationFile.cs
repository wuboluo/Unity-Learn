using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FileFolderOperation
{
    public class OperationFile : MonoBehaviour
    {
        /// <summary>
        /// 创建或删除指定文件
        /// </summary>
        private void CreateOrDeleteFile(string filePath)
        {
            // 不存在就创建
            if (!File.Exists(filePath)) File.Create(filePath);
            // 存在就删除（注意：如果文件处于被打开的状态，会报错）
            else File.Delete(filePath);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="form">内容形式</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">内容</param>
        private void WriteToFile(ContentForm form, string filePath, object content)
        {
            switch (form)
            {
                // 以字符串形式写入
                case ContentForm.String:
                    File.WriteAllText(filePath, content as string);
                    break;

                // 以byte数组形式写入
                case ContentForm.ByteArray:
                    File.WriteAllBytes(filePath, content as byte[] ?? Array.Empty<byte>());
                    break;

                // 一行一行写入
                case ContentForm.StringArray:
                    File.WriteAllLines(filePath, content as string[] ?? Array.Empty<string>());
                    break;
            }
        }

        /// <summary>
        /// 以流的形式写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">内容</param>
        private void WriteToFileByStream(string filePath, byte[] content)
        {
            using FileStream fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            fs.Write(content, 0, content.Length);
            fs.Flush();
            fs.Close();
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="form">内容形式</param>
        /// <param name="filePath">文件路径</param>
        private object ReadFromFile(ContentForm form, string filePath)
        {
            switch (form)
            {
                // 读取所有文本信息
                case ContentForm.String:
                    string textString = File.ReadAllText(filePath);
                    return textString;

                // 读取字节信息
                case ContentForm.ByteArray:
                    byte[] bytes = File.ReadAllBytes(filePath);
                    return BitConverter.ToInt32(bytes, 0); // 这里以int类型举例

                // 读取所有行信息
                case ContentForm.StringArray:
                    string[] strings = File.ReadAllLines(filePath);
                    return strings;
            }

            return null;
        }

        /// <summary>
        /// 复制文件到新的位置，允许覆盖同名文件
        /// </summary>
        /// <param name="sourceFileName">被复制的文件路径</param>
        /// <param name="destFileName">被复制到的路径</param>
        /// <param name="overwrite">同名是否覆盖</param>
        private void CopyFile(string sourceFileName, string destFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>
        /// 替换文件：使用其他文件的内容替换指定文件的内容，这一过程将删除原始文件，并创建被替换文件的备份
        /// </summary>
        /// <param name="sourceFileName">用来替换的路径</param>
        /// <param name="destinationFileName">被替换的路径</param>
        /// <param name="destinationBackupFileName">将被替换的文件备份到的路径</param>
        private void ReplaceFile(string sourceFileName, string destinationFileName, string destinationBackupFileName)
        {
            File.Replace(sourceFileName, destinationFileName, destinationBackupFileName);
        }

        /// <summary>
        /// 内容形式
        /// </summary>
        private enum ContentForm
        {
            String,
            ByteArray,
            StringArray
        }
    }
}