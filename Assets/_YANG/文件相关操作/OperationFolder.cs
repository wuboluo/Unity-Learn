using System.IO;
using UnityEngine;

namespace FileFolderOperation
{
    public class OperationFolder : MonoBehaviour
    {
        /// <summary>
        /// 创建或删除文件夹
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        private void CreateOrDeleteFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                // 删除文件夹
                // true：删除整个目录 false：仅当该目录为空时才删除
                Directory.Delete(folderPath, true);
            }
            else
            {
                // 创建文件夹
                DirectoryInfo info = Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// 获取指定路径下的所有文件夹名
        /// </summary>
        /// <param name="folderPath">指定路径</param>
        /// <returns>文件夹名列表</returns>
        private string[] GetAllFolderNameInPath(string folderPath)
        {
            string[] folderNames = Directory.GetDirectories(folderPath);
            return folderNames;
        }

        /// <summary>
        /// 得到指定路径下所有文件名
        /// </summary>
        /// <param name="folderPath">指定路径</param>
        /// <returns>文件夹列表</returns>
        private string[] GetAllFileNameInPath(string folderPath)
        {
            string[] fileNames = Directory.GetFiles(Application.dataPath);
            return fileNames;
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="sourcePath">被移动的文件夹路径</param>
        /// <param name="destPath">移动到的路径</param>
        private void MoveFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath)) Directory.Move(sourcePath, destPath);
        }

        /// <summary>
        /// 获取指定文件夹的父级文件夹信息
        /// </summary>
        /// <param name="folderPath">指定文件夹路径</param>
        /// <returns>父级文件夹信息</returns>
        private DirectoryInfo GetParentDirectory(string folderPath)
        {
            DirectoryInfo parentInfo = Directory.GetParent(folderPath);
            if (parentInfo != null)
            {
                print(parentInfo.FullName); // 完整路径
                print(parentInfo.Name); // 文件名
            }
            return parentInfo;
        }
    }
}