using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace Yang.Net.FTP
{
    public class FtpDownloadFile : MonoBehaviour
    {
        private void Start()
        {
            #region 使用FTP下载文件关键点

            // 1，通信凭证，进行Ftp连接操作时需要的账号密码
            // 2，操作命令 WebRequestMethods.Ftp，设置想要进行的Ftp操作
            // 3，文件流相关 FileStream Stream，上传和下载时都会使用的文件流
            // 4，保证Ftp服务器已经开启，并且能够正常访问

            #endregion

            #region FTP下载

            try
            {
                // 1，创建一个Ftp连接（下载的文件名，一定是FTP上存在的）
                if (WebRequest.Create(new Uri("ftp://127.0.0.1/pic.png")) is not FtpWebRequest request) return;

                // 2，将代理相关信息置空，避免服务器同时有 http相关服务，造成冲突
                request.Proxy = null;

                // 3，设置通信凭证（如果不支持匿名，就必须设置这个）
                NetworkCredential credential = new NetworkCredential("yang", "1223122");
                request.Credentials = credential;

                // 4，设置是否关闭控制连接在请求完毕后，如果想要关闭，设置为false
                request.KeepAlive = false;

                // 5，设置操作命令（设置命令为下载文件）
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // 6，设置传输类型（二进制）
                request.UseBinary = true;

                // 7，得到用于下载的流对象
                // 相当于把请求发送给FTP服务器，返回值就会携带想要的信息
                if (request.GetResponse() is FtpWebResponse response)
                {
                    // 用于下载的流
                    Stream downloadStream = response.GetResponseStream();

                    // 开始下载
                    print(Application.persistentDataPath);
                    using FileStream fs = File.Create(Application.persistentDataPath + "/YYY.png");
                    byte[] bytes = new byte[1024];

                    if (downloadStream != null)
                    {
                        // 读取下载下来的流数据
                        int length = downloadStream.Read(bytes, 0, bytes.Length);
                        print(length);

                        // 一点一点下载到本地流中
                        while (length != 0)
                        {
                            fs.Write(bytes, 0, length);
                            length = downloadStream.Read(bytes, 0, bytes.Length);
                        }

                        // 关闭流
                        downloadStream.Close();
                    }

                    fs.Close();

                    print("下载完成");
                }
            }
            catch (Exception e)
            {
                print($"下载失败：{e.Message}");
            }

            #endregion
        }
    }
}