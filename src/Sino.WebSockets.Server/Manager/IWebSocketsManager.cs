using System;
using System.Collections.Generic;
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
        /// <param name="webSocket">WS上下文对象</param>
        /// <returns>WS标识符</returns>
        WebSocketIdentity Add(WebSocketContext webSocket);

        /// <summary>
        /// 根据标识符判断WS是否存在
        /// </summary>
        /// <param name="identity">WS标识符</param>
        /// <returns>True表示存在，False表示不存在</returns>
        bool Exist(WebSocketIdentity identity);

        /// <summary>
        /// 根据标识符获取WS对象
        /// </summary>
        /// <param name="identity">WS标识符</param>
        /// <returns></returns>
        WebSocketContext Get(WebSocketIdentity identity);
    }
}
