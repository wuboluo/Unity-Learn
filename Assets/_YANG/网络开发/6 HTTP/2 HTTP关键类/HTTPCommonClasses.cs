using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace Yang.Net.HTTP
{
    public class HTTPCommonClasses : MonoBehaviour
    {
        private void Start()
        {
            #region HttpWebRequest

            // 主要用于发送客户端请求的类
            // 发送HTTP客户端请求给服务器，可以进行消息通信、上传、下载等操作

            // 重要方法：
            // 1，Create 创建新的WebRequest，用于进行HTTP相关操作
            HttpWebRequest request = WebRequest.Create(new Uri("http://192.168.0.199/HTTP%20Server/")) as HttpWebRequest;
            // 2，Abort 如果正在文件传输，用此方法可以终止传输
            request?.Abort();
            // 3，GetRequestStream 获取用于上传的流
            Stream stream = request?.GetRequestStream();
            // 4，返回HTTP服务响应
            HttpWebResponse r = request?.GetResponse() as HttpWebResponse;
            // 5，Begin/EndGetRequestStream 获取用于上传的流（和Tcp原理类似）
            // 6，Begin/EndGetResponse 异步获取返回的HTTP服务器响应

            // 重要成员：
            if (request != null)
            {
                // 1，Credentials 通信凭证，设置为NetWorkCredential对象。和下面的标头配合使用
                request.Credentials = new NetworkCredential("", "");
                // 2，PreAuthenticate 是否随请求发送一个身份验证标头，一般需要进行身份验证时需要将其设置为true
                request.PreAuthenticate = true;

                // 3，Headers 构成标头的名称/值对的集合
                // 4，ContentLength 发送信息的字节数，上传信息时需要先设置该内容长度
                request.ContentLength = 100;
                // 5，ContentType 在进行POST请求时，需要对发送的内容进行内容类型的设置
                request.ContentType = "";
                // 6，Method 操作命令设置
                //      WebRequestMethods.HTTP 类中的操作命令属性
                //          Get 获取请求，一般用于获取数据
                //          Post 提交请求，一般用于上传数据，同时可以获取
                //          Head 获取和Get一致的内容，只不过只会返回消息头，不会返回具体内容
                //          Put 向指定位置上传最新内容
                //          Connect 表示与代理一起使用的 HTTP CONNECT 协议方法，该代理可以动态切换到隧道
                //          MKCol 请求在请求 URI（统一资源标识符）指定位置新建集合
                request.Method = WebRequestMethods.Http.Get;
            }

            #endregion

            #region HttpWebResponse

            // 主要用于获取服务器反馈信息的类
            // 可以通过 HttpWebRequest对象中的 GetResponse()获取
            // 使用完后，要 Close()掉

            // 重要方法：
            if (request?.GetResponse() is FtpWebResponse response)
            {
                // 1，Close 释放所有资源
                response.Close();
                // 2，GetResponseStream 返回从FTP服务器下载数据的流（这个流可以理解是正儿八经下载下来的数据）
                Stream s = response.GetResponseStream();
                
                // 重要成员：
                // 1，接收到的数据的长度
                print(response.ContentLength);
                // 2，数据的类型
                print(response.ContentType);
                // 3，HTTP服务器下发的最新状态码
                print(response.StatusCode);
                // 4，HTTP服务器下发的状态代码的文本
                print(response.StatusDescription);
                // 5，登陆前建立连接时HTTP服务器发送的消息
                print(response.BannerMessage);
                // 6，HTTP会话结束时服务器发送的消息
                print(response.ExitMessage);
                // 7，HTTP服务器上的文件上次被修改的日期和时间
                print(response.LastModified);
            }

            #endregion
        }
    }
}