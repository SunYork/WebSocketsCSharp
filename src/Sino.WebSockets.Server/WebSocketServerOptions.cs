using System;

namespace Sino.WebSockets.Server
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class WebSocketServerOptions
    {
        public WebSocketServerOptions()
        {
            KeepAliveInterval = TimeSpan.FromMinutes(2);
            ReceiveBufferSize = 4 * 1024;
        }

        /// <summary>
        /// 心跳检测时间间隔，默认2分钟
        /// </summary>
        public TimeSpan KeepAliveInterval { get; set; }

        /// <summary>
        /// 接收缓存，默认4KB
        /// </summary>
        public int ReceiveBufferSize { get; set; }
    }
}
