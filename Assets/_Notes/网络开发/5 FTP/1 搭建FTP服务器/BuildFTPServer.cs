using UnityEngine;

namespace Yang.Net.FTP
{
    public class BuildFTPServer : MonoBehaviour
    {
        private void Start()
        {
            // 本机FTP账号密码：yang-1223122

            #region 搭建FTP服务器的几种方式

            // 1，使用别人做好的FTP服务器（学习阶段建议使用）
            // 2，自己编写FTP服务器应用程序，基于FTP的工作原理，用Socket中Tcp通信来进行编程（一般由后端程序来做）
            // 3，将电脑搭建为FTP文件共享服务器（一般由后端程序来做）

            // 作为前端程序，23点只做了解，一般不会由前端完成这部分工作

            #endregion

            #region 使用别人做好的FTP服务器软件来搭建FTP服务器

            // 例如：使用 Serv-U 等FTP服务器软件，在想要作为FTP服务器的电脑上运行
            // 1，创建 域，直接不停下一步即可
            // 2，使用单向加密
            // 3，创建用于上传下载的FTP 账号密码

            #endregion

            #region 总结

            // 一般前端不用去考虑服务器的搭建，只需要保证文件的安全传输就好了
            // 如果是自己开发，使用别人做好的工具足够

            #endregion
        }
    }
}