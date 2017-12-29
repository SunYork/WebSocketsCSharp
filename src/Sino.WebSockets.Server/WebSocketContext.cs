using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Sino.WebSockets.Server
{
    /// <summary>
    /// 代表上下文对象
    /// </summary>
    public abstract class WebSocketContext : WebSocketBaseContext
    {
        private WebSocketIdentity _identity;

        public WebSocketContext(WebSocket webSocket, WebSocketIdentity identity)
            : base(webSocket)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        public int Identity
        {
            get
            {
                return _identity.Identity;
            }
        }

        public int InstanceId
        {
            get
            {
                return _identity.InstanceId;
            }
        }

        public object LastReceiveMsg { get; set; }

        public abstract Task SendAsync<T>(T msg);
    }
}
