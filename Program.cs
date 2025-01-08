using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using PuertoRico;
using System.Numerics;
using System.Xml.Linq;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Collections.Concurrent;
//manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5+1+2+2+1+1.5+2.5+4+2+3+3.5+1+3+2+2+3+2;
namespace PuertoRicoSpace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int.TryParse(args[0], out int threadNum);
            int.TryParse(args[1], out int forLoops);
            if (args.Length > 2)
            {
                int.TryParse(args[2], out int totalScoreLimit);
                RubGame(threadNum, forLoops, totalScoreLimit);
            }
            else
            {
                RubGame(threadNum, forLoops);
            }

            //string filePath = "D:\\Code\\C#\\PuertoRicoData\\" + guid + ".json";
            //string jsonInput = File.ReadAllText(filePath);
            //var options = new JsonSerializerOptions
            //{
            //    IncludeFields = false // 禁止欄位參與序列化和反序列化
            //};
            //data da = JsonSerializer.Deserialize<data>(jsonInput);
            //Console.WriteLine(da.PlayerNum);
            //Console.WriteLine(da.ElapsedTime);
            //Console.WriteLine(da.Round);
            //Console.WriteLine(da.TotalScore);
            //da.ShowBankStatus();
            //da.ShowPlayerStatus();
            //da.ShowCargo();


            //Console.ReadLine();
        }

        public static void RubGame(int threadNum = 1, int forLoops = 1, int totalScoreLimit = 130)
        {
            //int totalScoreLimit = 130;
            //int threadNum = 20;
            //int ForLoops = 200;
            ConcurrentBag<double> totalThreadAveTime = new ConcurrentBag<double>();
            ConcurrentBag<double> totalAveTimePerGame = new ConcurrentBag<double>();
            List<Thread> threads = new List<Thread>();
            ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);
            ThreadPool.GetMinThreads(out int workerThreads1, out int completionPortThreads1);
            //Console.WriteLine($"默認最大執行緒數量：{workerThreads}，I/O 執行緒：{completionPortThreads}");
            //Console.WriteLine($"默認最小執行緒數量：{workerThreads1}，I/O 執行緒：{completionPortThreads1}");
            Console.WriteLine($"Thread Num:{threadNum}    Loop:{forLoops}");
            Console.WriteLine("\n{0,-5}{1,-9}{2,11}{3,16}", "ID", "id", "Thread t", "Ave game time");
            Stopwatch mainTimer = new Stopwatch();
            mainTimer.Start();
            for (int i = 0; i < threadNum; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Stopwatch subTimer = new Stopwatch();
                    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                    int currentThreadId = (int)Utilities.GetCurrentThreadId();
                    subTimer.Start();
                    //RunGame(doLoops, totalScoreLimit);
                    for (int j = 0; j < forLoops; j++)
                    {
                        Option option = new Option(scoreLimit: totalScoreLimit);
                        PuertoRico game = new PuertoRico(5, option);
                    }
                    subTimer.Stop();
                    TimeSpan ElapsedTime = subTimer.Elapsed;
                    double threadTime = ElapsedTime.TotalSeconds;
                    double aveTimePerGame = ElapsedTime.TotalMilliseconds / (forLoops);
                    Console.WriteLine("{0,-5}{1,-9}{2,10:N2}s{3,15:N1}ms", managedThreadId, currentThreadId, threadTime, aveTimePerGame);
                    totalThreadAveTime.Add(threadTime);
                    totalAveTimePerGame.Add(aveTimePerGame);
                });
                threads.Add(thread);
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            mainTimer.Stop();
            TimeSpan mainTime = mainTimer.Elapsed;
            double mainThreadTime = mainTime.TotalSeconds;
            double mainAveTimePerGame = mainTime.TotalMilliseconds / (threadNum * forLoops);

            Console.WriteLine("{0,-5}{1,-9}{2,10:N2}s{3,15:N1}ms", "Ave", "", totalThreadAveTime.Average(), totalAveTimePerGame.Average());
            Console.WriteLine("{0,-5}{1,-9}{2,10:N2}s{3,15:N1}ms", "Total", "", mainThreadTime, mainAveTimePerGame);

        }
    }
}
