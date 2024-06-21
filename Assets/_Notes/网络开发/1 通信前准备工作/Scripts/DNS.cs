using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace Yang.Net.NDS
{
    public class DNS : MonoBehaviour
    {
        private void Start()
        {
            #region 什么是域名解析？

            // 域名解析又名 域名指向、服务器设置、域名配置、反向IP登记...等
            // 将好记的域名转换为IP
            // IP就是网络上标记站点的数字地址，但是IP地址相对来说记忆困难
            // 例如：www.baidu.com 就是一个域名

            // 域名解析就是域名到IP地址的转换过程，域名的解析过程由 DNS 服务器完成
            // DNS是互联网的一项服务，它作为将域名和IP地址相互映射的一个分布式数据库，能够使人更方便的访问互联网
            // 是因特网上解决网上机器命名的一种系统，因为IP地址不好记，就采用了域名系统来管理名字和IP的对应关系

            #endregion

            #region IPHostEntry

            // 作用：域名解析后的返回值，可以通过该对象获取IP地址，主机名等信息
            // 该类不会自己声明，都是作为某些方法的返回值返回信息

            // 获取关联IP：AddressList
            // 获取主机别名列表：Aliases
            // 获取 DNS名称：HostName

            #endregion

            #region Dns

            // 作用：Dns是一个静态类，可以使用它来根据域名获取IP地址

            // 1，获取本地系统的主机名
            string nativeName = Dns.GetHostName();
            print("本机名：" + nativeName);

            // 2，获取指定域名的IP信息
            print("---------- 同步获取 ----------");
            // 同步获取：
            IPHostEntry entry = Dns.GetHostEntry("www.baidu.com");
            foreach (IPAddress a in entry.AddressList) print("baidu的IP地址：" + a);
            foreach (string a in entry.Aliases) print("baidu的主机别名：" + a);
            print("baidu的DNS服务器名称：" + entry.HostName);

            print("---------- 异步获取 ----------");
            GetHostEntryAsync();

            #endregion
        }

        private static async void GetHostEntryAsync()
        {
            Task<IPHostEntry> task = Dns.GetHostEntryAsync("www.baidu.com");
            await task;

            foreach (IPAddress a in task.Result.AddressList) print("baidu的IP地址：" + a);
            foreach (string a in task.Result.Aliases) print("baidu的主机别名：" + a);
            print("baidu的DNS服务器名称：" + task.Result.HostName);
        }
    }
}