using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace Yang.Net.FTP
{
    public class FtpUploadFile : MonoBehaviour
    {
        private void Start()
        {
            #region 使用FTP上传文件关键点

            // 1，通信凭证，进行Ftp连接操作时需要的账号密码
            // 2，操作命令 WebRequestMethods.Ftp，设置想要进行的Ftp操作
            // 3，文件流相关 FileStream Stream，上传和下载时都会使用的文件流
            // 4，保证Ftp服务器已经开启，并且能够正常访问

            #endregion

            #region FTP上传

            try
            {
                // 1，创建一个Ftp连接（想要上传一个文件，取名叫 pic.png）
                if (WebRequest.Create(new Uri("ftp://127.0.0.1/pic.png")) is not FtpWebRequest request) return;
                
                // 2，将代理相关信息置空，避免服务器同时有 http相关服务，造成冲突
                request.Proxy = null;

                // 3，设置通信凭证（如果不支持匿名，就必须设置这个）
                NetworkCredential credential = new NetworkCredential("yang", "1223122");
                request.Credentials = credential;

                // 4，设置是否关闭控制连接在请求完毕后，如果想要关闭，设置为false
                request.KeepAlive = false;

                // 5，设置操作命令（设置命令为上传文件）
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // 6，设置传输类型（二进制）
                request.UseBinary = true;

                // 7，得到用于上传的流对象
                Stream upLoadStream = request.GetRequestStream();

                // 8，开始上传
                using FileStream fs = File.OpenRead(Application.streamingAssetsPath + "/1.png");
                // 一点一点的把这个文件中的字节数据读取出来，存于上传流中
                byte[] bytes = new byte[1024];
                // 返回值：真正读取了多少字节
                int length = fs.Read(bytes, 0, bytes.Length);
                // 不停的读取文件，直到全部读取完成
                while (length != 0)
                {
                    // 写入上传流
                    upLoadStream.Write(bytes, 0, length);
                    // 继续读
                    length = fs.Read(bytes, 0, bytes.Length);
                }

                // 写完之后，关闭流
                fs.Close();
                upLoadStream.Close();

                print("上传结束");
            }
            catch (Exception e)
            {
                print($"上传失败：{e.Message}");
            }

            #endregion
        }
    }
}