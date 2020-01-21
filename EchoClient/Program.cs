using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // 소켓 생성
            using (var client = new RequestSocket())
            {
                // 연결 주소 입력
                client.Connect("tcp://localhost:5555");

                // 처리 구간
                while(true)
                {
                    // 메시지 전송
                    client.SendFrame("Hello");

                    // 메시지 수신
                    //var message = client.ReceiveFrameString();
                    
                    // 메시지 출력
                    //Console.WriteLine($"Received {message}");
                    
                    // 멀티프레임 수신
                    var messagelist = client.ReceiveMultipartStrings();
                    foreach(var text in messagelist)
                        Console.WriteLine($"Received {text}");

                    // 잠시 대기
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
