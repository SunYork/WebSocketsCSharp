using Sino.WebSockets.Server.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.WebSockets.Server
{
    /// <summary>
    /// WS管理器基类
    /// </summary>
    public class WebSocketsManager : IWebSocketsManager
    {
        private readonly IIdentityRepository _identityRepository;

        public WebSocketsManager(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }
    }
}
