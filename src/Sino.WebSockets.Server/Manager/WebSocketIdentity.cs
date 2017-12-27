using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.WebSockets.Server
{
    /// <summary>
    /// WS 标识符
    /// 可利用以下信息查询或调用对应WS连接发送等操作
    /// </summary>
    public class WebSocketIdentity
    {
        /// <summary>
        /// WS 连接标识
        /// </summary>
        public int Identity { get; set; }

        /// <summary>
        /// 实例标识
        /// </summary>
        public int InstanceId { get; set; }
    }
}
