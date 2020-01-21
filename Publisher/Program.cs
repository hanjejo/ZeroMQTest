using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pubSocket = new PublisherSocket())
            {
                // 미해결 메시지 제한
                pubSocket.Options.SendHighWatermark = 1000;
                
                // 바인딩
                pubSocket.Bind("tcp://*:12345");

                for (var i = 0; i < 100; i++)
                {
                    if (i % 2 == 0)
                    {
                        var msg = "TopicA msg-" + i + " 노석수 바보";
                        Console.WriteLine($"Sending message : {msg}");
                        pubSocket.SendMoreFrame("TopicA").SendFrame(msg);
                    }
                    else
                    {
                        var msg = "TopicB msg-" + i + " 노석수 천재";
                        Console.WriteLine($"Sending message : {msg}");
                        pubSocket.SendMoreFrame("TopicB").SendFrame(msg);
                    }

                    Thread.Sleep(1000);
                }
            }
        }
    }
}
