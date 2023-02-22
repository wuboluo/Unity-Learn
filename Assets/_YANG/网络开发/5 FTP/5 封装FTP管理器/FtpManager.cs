using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Yang.Net.FTP
{
    public static class FtpManager
    {
        private const string ftpPath = "ftp://127.0.0.1/";
        private const string userName = "yang";
        private const string password = "1223122";

        /// <summary>
        ///     上传文件到FTP服务器
        /// </summary>
        /// <param name="fileName">FTP上的文件名</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="action">上传完成后执行的操作</param>
        public static async void UploadFile(string fileName, string localPath, UnityAction action = null)
        {
            // 通过一个线程执行这里面的逻辑，不会影响主线程
            await Task.Run(() =>
            {
                try
                {
                    // 1，创建Ftp连接
                    if (WebRequest.Create(ftpPath + fileName) is FtpWebRequest request)
                    {
                        FtpWebRequestSettings(request);
                        request.Method = WebRequestMethods.Ftp.UploadFile; // 模式为上传

                        // 3，上传
                        Stream uploadStream = request.GetRequestStream();
                        using FileStream fs = File.OpenRead(localPath);
                        byte[] bytes = new byte[1024];
                        int length = fs.Read(bytes, 0, bytes.Length);
                        // 有数据就一直上传
                        while (length != 0)
                        {
                            // 读了多少就上传多少
                            uploadStream.Write(bytes, 0, length);
                            // 继续读
                            length = fs.Read(bytes, 0, bytes.Length);
                        }

                        // 4，关闭连接
                        fs.Close();
                        uploadStream.Close();

                        Debug.Log("上传成功");
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"上传失败：{e.Message}");
                }
            });

            // 上传完成后，执行的操作
            action?.Invoke();
        }

        /// <summary>
        ///     从FTP服务器下载文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="localPath">下载到的本地路径</param>
        /// <param name="action">下载完成后执行的操作</param>
        public static async void DownloadFile(string fileName, string localPath, UnityAction action = null)
        {
            // 通过一个线程执行这里面的逻辑，不会影响主线程
            await Task.Run(() =>
            {
                try
                {
                    // 1，创建Ftp连接
                    if (WebRequest.Create(ftpPath + fileName) is FtpWebRequest request)
                    {
                        FtpWebRequestSettings(request);
                        request.Method = WebRequestMethods.Ftp.DownloadFile; // 模式为下载

                        // 3，下载
                        if (request.GetResponse() is FtpWebResponse response)
                        {
                            Stream downloadStream = response.GetResponseStream();
                            // 写入到本地文件
                            using FileStream fs = File.Create(localPath);
                            byte[] bytes = new byte[1024];
                            if (downloadStream != null)
                            {
                                int length = downloadStream.Read(bytes, 0, bytes.Length);
                                while (length != 0)
                                {
                                    fs.Write(bytes, 0, length);
                                    length = downloadStream.Read(bytes, 0, bytes.Length);
                                }

                                // 关闭流
                                downloadStream.Close();
                            }

                            fs.Close();
                            response.Close();
                        }


                        Debug.Log("下载成功");
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"下载失败：{e.Message}");
                }
            });

            // 下载完成后，执行的操作
            action?.Invoke();
        }

        /// <summary>
        ///     移除FTP上的指定文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="action">移除完成后执行的操作</param>
        public static async void DeleteFile(string fileName, UnityAction<bool> action = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    // 1，创建Ftp连接
                    if (WebRequest.Create(ftpPath + fileName) is FtpWebRequest request)
                    {
                        // 2，设置
                        FtpWebRequestSettings(request);
                        request.Method = WebRequestMethods.Ftp.DeleteFile; // 模式为删除

                        // 3，删除
                        if (request.GetResponse() is FtpWebResponse response)
                        {
                            response.Close();

                            action?.Invoke(true);
                            Debug.Log("移除成功");
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"移除失败：{e.Message}");
                    action?.Invoke(false);
                }
            });
        }

        /// <summary>
        ///     获取FTP上某个文件大小
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="action">获取完成后执行的操作</param>
        public static async void GetFileSize(string fileName, UnityAction<long> action = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    // 1，创建Ftp连接
                    if (WebRequest.Create(ftpPath + fileName) is FtpWebRequest request)
                    {
                        // 2，设置
                        FtpWebRequestSettings(request);
                        request.Method = WebRequestMethods.Ftp.GetFileSize; // 模式为获取文件大小

                        // 3，真正的获取
                        if (request.GetResponse() is FtpWebResponse response)
                        {
                            // 把文件大小传递给外部
                            action?.Invoke(response.ContentLength);
                            response.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"获取文件大小失败失败：{e.Message}");
                    action?.Invoke(0);
                }
            });
        }

        /// <summary>
        ///     在FTP上创建文件夹
        /// </summary>
        /// <param name="directoryName">文件夹名</param>
        /// <param name="action">创建完成后执行的操作</param>
        public static async void CreateDirectory(string directoryName, UnityAction<bool> action = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    // 1，创建Ftp连接
                    if (WebRequest.Create(ftpPath + directoryName) is FtpWebRequest request)
                    {
                        // 2，设置
                        FtpWebRequestSettings(request);
                        request.Method = WebRequestMethods.Ftp.MakeDirectory; // 模式为创建文件夹

                        // 3，真正的创建
                        if (request.GetResponse() is FtpWebResponse response)
                        {
                            response.Close();
                            action?.Invoke(true);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"创建文件夹失败：{e.Message}");
                    action?.Invoke(false);
                }
            });
        }

        /// <summary>
        ///     获取所有文件名
        /// </summary>
        /// <param name="directoryName">文件夹路径</param>
        /// <param name="action">返回给外部的文件名列表</param>
        public static async void GetFileList(string directoryName, UnityAction<List<string>> action = null)
        {
            await Task.Run(() =>
            {
                try
                {
                    // 1，创建Ftp连接
                    if (WebRequest.Create(ftpPath + directoryName) is FtpWebRequest request)
                    {
                        // 2，设置
                        FtpWebRequestSettings(request);
                        request.Method = WebRequestMethods.Ftp.ListDirectory; // 模式为遍历文件名

                        // 3，真正的遍历
                        if (request.GetResponse() is FtpWebResponse response)
                        {
                            // 把下载的信息流转换成 StreamReader对象，方便一行一行的读取信息
                            StreamReader sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException());
                            string line = sr.ReadLine();
                            List<string> fileNames = new List<string>();

                            // 不断地添加进列表
                            while (line != null)
                            {
                                fileNames.Add(line);
                                line = sr.ReadLine();
                            }

                            response.Close();
                            action?.Invoke(fileNames);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"获取文件列表失败：{e.Message}");
                    action?.Invoke(null);
                }
            });
        }

        // 通用设置
        private static void FtpWebRequestSettings(FtpWebRequest request)
        {
            request.Proxy = null; // 代理
            request.Credentials = new NetworkCredential(userName, password); // 凭证
            request.KeepAlive = false; // 传输完成后关闭连接
            request.UseBinary = true; // 使用二进制
        }

        #region 总结

        // FTP的作用：
        // 1，上传和下载功能（游戏进度数据等）
        // 2，原生的AB包上传和下载
        // 3，上传下载一些语言内容

        #endregion
    }
}