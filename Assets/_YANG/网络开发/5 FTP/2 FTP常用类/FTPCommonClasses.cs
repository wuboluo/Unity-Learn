using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace Yang.Net.FTP
{
    public class FTPCommonClasses : MonoBehaviour
    {
        private void Start()
        {
            #region NetworkCredential 类

            // 通信凭证类对象
            // 用于在Ftp文件传输时，设置账号密码
            NetworkCredential credential = new NetworkCredential("yang", "1223122");

            #endregion

            #region FtpWebRequest 类

            // Ftp文件传输协议客户端操作类
            // 主要用于：上传、下载、删除服务器上的文件

            // 重要方法：
            // 1，Create 创建新的WebRequest，用于进行Ftp相关操作
            FtpWebRequest request = WebRequest.Create(new Uri("ftp://127.0.0.1/Test.txt")) as FtpWebRequest;
            // 2，Abort 如果正在文件传输，用此方法可以终止传输
            request?.Abort();
            // 3，GetRequestStream 获取用于上传的流
            Stream stream = request?.GetRequestStream();
            // 4，返回FTP服务响应
            FtpWebResponse r = request?.GetResponse() as FtpWebResponse;

            // 重要成员：
            if (request != null)
            {
                // 1，Credentials 通信凭证，设置为NetWorkCredential对象
                request.Credentials = credential;
                // 2，KeepAlive bool值，当完成请求时是否关闭到FTP服务器的控制连接（默认为true，不关闭）
                request.KeepAlive = false;
                // 3，Method 操作命令设置
                //      WebRequestMethods.Ftp 类中的操作命令属性
                //          DeleteFile 删除文件
                //          DownloadFile 下载文件
                //          ListDirectory 获取文件简短列表
                //          ListDirectoryDetails 获取文件详细列表
                //          MakeDirectory 创建目录
                //          RemoveDirectory 删除目录
                //          UploadFile 上传文件
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                // 4，是否使用二进制传输，false则使用ASCII码格式
                request.UseBinary = true;
                // 5，重命名
                request.RenameTo = "MyTest.txt";
            }

            #endregion

            #region FtpWebResponse 类

            // 用于封装FTP服务器对请求的响应
            // 提供操作状态以及从服务器下载数据
            // 可以通过 FtpWebRequest对象中的 GetResponse()获取
            // 使用完后，要 Close()掉

            // 通过它真正的从服务器上获取内容
            if (request?.GetResponse() is FtpWebResponse response)
            {
                // 重要方法：
                // 1，Close 释放所有资源
                response.Close();
                // 2，GetResponseStream 返回从FTP服务器下载数据的流（这个流可以理解是正儿八经下载下来的数据）
                Stream s = response.GetResponseStream();

                // 重要成员：
                // 1，接收到的数据的长度
                print(response.ContentLength);
                // 2，数据的类型
                print(response.ContentType);
                // 3，FTP服务器下发的最新状态码
                print(response.StatusCode);
                // 4，FTP服务器下发的状态代码的文本
                print(response.StatusDescription);
                // 5，登陆前建立连接时FTP服务器发送的消息
                print(response.BannerMessage);
                // 6，FTP会话结束时服务器发送的消息
                print(response.ExitMessage);
                // 7，FTP服务器上的文件上次被修改的日期和时间
                print(response.LastModified);
            }

            #endregion
        }
    }
}