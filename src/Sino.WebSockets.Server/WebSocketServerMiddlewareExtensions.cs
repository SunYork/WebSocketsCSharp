using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebSocketServerMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebSocketsServer(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {

            });
        }
    }
}
