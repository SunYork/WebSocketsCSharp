using Microsoft.Extensions.Options;
using Sino.WebSockets.Server;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebSocketServerMiddlewareExtensions
    {
        /// <summary>
        /// 启用WebSocket功能，默认已经启用官方，无需多次启动
        /// </summary>
        public static IApplicationBuilder UseWebSocketsServer(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseWebSockets();

            return app.UseMiddleware<WebSocketServerMiddleware>();
        }

        /// <summary>
        /// 启用WebSocket功能，默认已经启用官方，无需多次启动
        /// </summary>
        public static IApplicationBuilder UseWebSocketsServer(this IApplicationBuilder app, WebSocketServerOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            app.UseWebSockets();

            return app.UseMiddleware<WebSocketServerMiddleware>(Options.Create(options));
        }
    }
}
