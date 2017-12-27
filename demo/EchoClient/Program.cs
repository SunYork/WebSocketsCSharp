using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace EchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new ClientWebSocket();
            ws.ConnectAsync(new Uri("ws://localhost:6511"), CancellationToken.None).Wait();
            var buffer = Encoding.UTF8.GetBytes("hello");
            ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

            while (true)
            {
                var result = ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).Result;
                var msg = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(msg);

                if (Console.ReadLine() == "quit")
                {
                    break;
                }
            }

            ws.CloseAsync(WebSocketCloseStatus.Empty, "empty", CancellationToken.None).Wait();
            Console.WriteLine("Hello World!");
        }
    }
}
