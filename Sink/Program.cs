using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sink
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task Sink
            // Bindd PULL socket to tcp://localhost:5558
            // Collects results from workers via that socket
            Console.WriteLine("====== SINK ======");

            //socket to receive messages on
            using (var receiver = new PullSocket("@tcp://localhost:5558"))
            {
                //wait for start of batch (see Ventilator.csproj Program.cs)
                var startOfBatchTrigger = receiver.ReceiveFrameString();
                Console.WriteLine("Seen start of batch");

                //Start our clock now
                var watch = Stopwatch.StartNew();

                for (int taskNumber = 0; taskNumber < 100; taskNumber++)
                {
                    var workerDoneTrigger = receiver.ReceiveFrameString();
                    if (taskNumber % 10 == 0)
                    {
                        Console.Write(":");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                watch.Stop();
                //Calculate and report duration of batch
                Console.WriteLine();
                Console.WriteLine("Total elapsed time {0} msec", watch.ElapsedMilliseconds);
                Console.ReadLine();
            }
        }
    }
}
