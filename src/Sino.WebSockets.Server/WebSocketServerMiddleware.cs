using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.WebSockets.Server
{
    public class WebSocketServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketServerOptions _options;

        public WebSocketServerMiddleware(RequestDelegate next, IOptions<WebSocketServerOptions> options)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _next = next;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return _next(context);


        }
    }
}
