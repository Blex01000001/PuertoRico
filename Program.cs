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
//manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5+1+2+2+1+1.5+2.5+4+2+3+3.5+1+3+2+2+3;
namespace PuertoRicoSpace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int totalScoreLimit = 130;
            int threadNum = 8;
            int ForLoops = 10;
            ConcurrentBag<double> totalThreadAveTime = new ConcurrentBag<double>();
            ConcurrentBag<double> totalAveTimePerGame = new ConcurrentBag<double>();
            List<Thread> threads = new List<Thread>();

            Console.WriteLine($"Thread Num:{threadNum}    Loop:{ForLoops}");
            Console.WriteLine("\n{0,-4}{1,-7}{2,-13}{3,-15}", "ID", "id", "Thread Time", "Ave game time");
            for (int i = 0; i < threadNum; i++) 
            { 
                Thread thread = new Thread(() =>
                {
                    Stopwatch timer = new Stopwatch();
                    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                    int currentThreadId = (int)Utilities.GetCurrentThreadId();
                    timer.Start();
                    //RunGame(doLoops, totalScoreLimit);
                    for (int j = 0; j < ForLoops; j++)
                    {
                        Option option = new Option(scoreLimit: totalScoreLimit);
                        PuertoRico game = new PuertoRico(5, option);
                    }
                    timer.Stop();
                    TimeSpan ElapsedTime = timer.Elapsed;
                    double threadTime = ElapsedTime.TotalSeconds;
                    double aveTimePerGame = ElapsedTime.TotalMilliseconds / ForLoops;
                    Console.WriteLine("{0,-4}{1,-7}{2,-13:N3}{3,-15:N1}", managedThreadId, currentThreadId, threadTime, aveTimePerGame);
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

            Console.WriteLine("\nTotal Thread Ave Time:   {0,6:N3}  s", totalThreadAveTime.Average());
            Console.WriteLine("Total Ave Time Per Game: {0,6:N1} ms ", totalAveTimePerGame.Average());
            
            
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


            Console.ReadLine();
        }
    }
}
