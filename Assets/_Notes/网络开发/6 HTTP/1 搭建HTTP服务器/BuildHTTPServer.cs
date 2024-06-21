using UnityEngine;

namespace Yang.Net.HTTP
{
    public class BuildHTTPServer : MonoBehaviour
    {
        private void Start()
        {
            #region 搭建HTTP服务器的几种方式

            // 1，使用别人做好的HTTP服务器（学习阶段建议使用）
            // 2，自己编写HTTP服务器应用程序，一般作为【Web服务器】或者【短链接游戏服务器】时使用该方式（一般由后端程序来做）

            // 一般不会由前端完成这部分工作

            #endregion

            #region 使用别人做好的HTTP服务器软件来搭建HTTP服务器

            // 例如：使用在Addressable里用到过的 hfs服务器软件

            #endregion

            #region 使用别人做好的Web服务器进行测试

            // 可以直接在别人做好的Web服务器上获取信息和资源，比如可以下载任意网站上可被下载的图片

            #endregion

            #region 总结

            // 在实际的商业项目开发中
            // HTTP资源服务器，可以自己写也可以用别人做好的软件
            // HTTP网站服务器或游戏服务器，需要自己根据需求进行实现
            // 一般都是由后端或者运维程序员进行制作

            // 在游戏开发时，更多时候需要的HTTP资源服务器
            // 除非要做短连接游戏，那么后端程序可以以HTTP协议为基础来开发服务端应用程序

            #endregion
        }
    }
}