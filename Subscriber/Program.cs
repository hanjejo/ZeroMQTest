using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber
{
    class Program
    {
        public static IList<string> allowableCommandLineArgs = new[] { "TopicA", "TopicB", "All" };

        static void Main(string[] args)
        {
            // 구독 주제 설정
            if (args.Length != 1 || !allowableCommandLineArgs.Contains(args[0]))
            {
                Console.WriteLine("Expected one argument, either " + "'TopicA', 'TopicB' or 'All'");
                Environment.Exit(-1);
            }

            // 구독 주제 입력
            string topic = args[0] == "All" ? "" : args[0];
            Console.WriteLine($"Subscriber started for Topic : {topic}");

            // 소켓 오픈
            using (var subSocket = new SubscriberSocket())
            {
                // 미해결 메시지 제한
                subSocket.Options.ReceiveHighWatermark = 1000;
                
                // 소켓 오픈
                subSocket.Connect("tcp://localhost:12345");
                
                // 주제 구독
                subSocket.Subscribe(topic);

                // 메시지 처리
                Console.WriteLine("Subscriber socket connecting...");
                while (true)
                {
                    string messageTopicReceived = subSocket.ReceiveFrameString();
                    string messageReceived = subSocket.ReceiveFrameString();
                    Console.WriteLine(messageReceived);
                }
            }
        }
    }
}
