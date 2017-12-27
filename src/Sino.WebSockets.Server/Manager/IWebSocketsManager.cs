using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Sino.WebSockets.Server
{
    /// <summary>
    /// WS管理器接口
    /// </summary>
    public interface IWebSocketsManager
    {
        /// <summary>
        /// 添加WS到管理器中
        /// </summary>
        /// <param name="webSocket">WS对象</param>
        /// <returns>WS标识符</returns>
        WebSocketIdentity Add(WebSocket webSocket);
    }
}
