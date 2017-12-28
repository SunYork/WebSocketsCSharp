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
        /// 根据标识符判断WS是否存在
        /// </summary>
        /// <param name="identity">标识符</param>
        /// <param name="instanceId">实例编号</param>
        /// <returns>True表示存在，False表示不存在</returns>
        bool Exist(int identity, int instanceId = -1);

        /// <summary>
        /// 根据标识符获取WS对象
        /// </summary>
        /// <param name="identity">WS标识符</param>
        /// <returns>存在则会返回对象，否则返回Null</returns>
        WebSocketContext Get(WebSocketIdentity identity);

        /// <summary>
        /// 根据标识符获取WS对象
        /// </summary>
        /// <param name="identity">标识符</param>
        /// <param name="instalceId">实例编号</param>
        /// <returns>存在则会返回对象，否则返回Null</returns>
        WebSocketContext Get(int identity, int instanceId = -1);

        /// <summary>
        /// 查询当前保存的WS对象
        /// </summary>
        /// <param name="instanceId">实力编号，填写则表示查询指定实例上存在的数量</param>
        /// <returns>总共WS的数量</returns>
        int Count(int instanceId = -1);
    }
}
