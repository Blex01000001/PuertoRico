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
//manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5+1+2+2+1+1.5+2.5+4+2+3+3.5+1+3+2+2;
namespace PuertoRicoSpace
{
    internal class Program
    {
        public static string filePath { get { return "D:\\Code\\C#\\PuertoRicoData\\"; } }
        static void Main(string[] args)
        {
            int totalScoreLimit = 10;
            int threadNum = 10;
            int doLoops = 200;
            List<double> totalThreadAveTime = new List<double>();
            List<double> totalAveTimePerGame = new List<double>();

            Console.WriteLine($"Thread Num:{threadNum}    Loop:{doLoops}");
            for (int i = 0; i < threadNum; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
                    int currentThreadId = (int)Utilities.GetCurrentThreadId();
                    RunGame(doLoops, totalScoreLimit);
                    timer.Stop();
                    TimeSpan ElapsedTime = timer.Elapsed;
                    double threadTime = ElapsedTime.TotalSeconds;
                    double aveTimePerGame = ElapsedTime.TotalMilliseconds / doLoops;
                    Console.WriteLine("ID:{0,3}  {1,5} time:{2,5:N3}s  Ave:{3,6:N1}ms", managedThreadId, currentThreadId, threadTime, aveTimePerGame);
                    lock (totalThreadAveTime)
                    {
                        totalThreadAveTime.Add(threadTime);
                    }
                    lock (totalAveTimePerGame)
                    {
                        totalAveTimePerGame.Add(aveTimePerGame);
                    }
                    if(totalAveTimePerGame.Count == threadNum)
                    {
                        Console.WriteLine("\nTotal Thread Ave Time:   {0,6:N3}  s", totalThreadAveTime.Average());
                        Console.WriteLine("Total Ave Time Per Game: {0,6:N1} ms ", totalAveTimePerGame.Average());
                    }
                });
                thread.Start();
            }
            //Task.Delay(10000);
            //Console.WriteLine("\nTotal Thread Ave Time:{0,3:N3}s  Total Ave Time Per Game{1,5:N1} ", totalThreadAveTime.Average(), totalAveTimePerGame.Average());

            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //Task.Delay(2000).Wait();
            //timer.Stop();
            //TimeSpan ElapsedTime = timer.Elapsed;
            //_writer.WriteLine($"thread cost {ElapsedTime.TotalMilliseconds}");
            //_writer.WriteLine("{0,15}{1,15}{2,7}{3,7}", "1234567890", "123456", "987", "6666");


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
        static private void RunGame(int doLoops, int totalScoreLimit)
        {
            List<PuertoRico> puertoRico = new List<PuertoRico>();
            do
            {
                Guid guid = Guid.NewGuid();
                string path = Program.filePath + guid + ".json";
                PuertoRico game = new PuertoRico(5, guid);
                if (game.TotalScore > totalScoreLimit)
                {
                    puertoRico.Add(game);
                }
                data _data = new data();
                _data.PlayerNum = game.PlayerNum;
                _data.ElapsedTime = game.ElapsedTime;
                _data.Round = game.Round;
                _data.TotalScore = game.TotalScore;
                _data.PlayerList = game.PlayerList;
                _data.Bank = game.Bank;
                string input = JsonSerializer.Serialize(_data);
                StreamWriter streamWriter = new StreamWriter(path);
                streamWriter.Write(input);
                streamWriter.Flush();
                streamWriter.Close();
            } while (puertoRico.Count < doLoops);
        }
    }
}
