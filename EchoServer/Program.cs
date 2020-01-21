using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 소켓 생성
            using (var server = new RequestSocket())
            {
                // 연결 주소 입력
                server.Bind("tcp://localhost:5555");

                // 처리 코드 수행 구간
                while (true)
                {
                    // 메시지 전송
                    server.SendMoreFrame("test");
                    server.SendFrame("Hello");

                    // 메시지 수신
                    var message = server.ReceiveFrameString();

                    // 메시지 출력
                    Console.WriteLine(message);
                }
            }
        }
    }
}
