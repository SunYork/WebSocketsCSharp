using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Sino.WebSockets
{
    /// <summary>
    /// 代表WS上下文对象基类
    /// </summary>
    public abstract class WebSocketBaseContext
    {
        public WebSocket WebSocket { get; set; }

        public WebSocketBaseContext(WebSocket webSocket)
        {
            WebSocket = webSocket;
        }
    }
}
