using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using PuertoRico;
using System.Numerics;
using System.Xml.Linq;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace PuertoRicoSpace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0,8}{1,7}{2,7}", "Time", "Score", "Round");
            for (int i = 0; i < 3; i++)
            {
                int ii = i;
                Thread thread = new Thread(() => puertoRico1(ii+1));
                
                thread.Start();
                
                
                //_writer.WriteLine($"thread {i + 1} cost {ElapsedTime}");

            }

            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //Task.Delay(987).Wait();
            //timer.Stop();
            //TimeSpan ElapsedTime = timer.Elapsed;
            //_writer.WriteLine($"thread cost {ElapsedTime.TotalMilliseconds}");
            //_writer.WriteLine("{0,15}{1,15}{2,7}{3,7}", "1234567890", "123456", "987", "6666");


            //string filePath = path;
            //string jsonInput = File.ReadAllText(filePath);

            //data da = JsonSerializer.Deserialize<data>(jsonInput);

            //da.ShowBankStatus();
            //da.ShowPlayerStatus();
            //da.ShowCargo();

            Console.ReadLine();
        }
        static private void puertoRico1(int num)
        {
            List<PuertoRico> puertoRico = new List<PuertoRico>();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ////manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5+1+2+2+1+1.5+2.5+4+2+3+3.5+1+3;
            do
            {
                Guid guid = Guid.NewGuid();
                string path = "D:\\Code\\C#\\PuertoRicoData\\" + guid + ".json";
                PuertoRico game = new PuertoRico(5, guid);
                if (game.TotalScore > 10)
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
                //var jsonString = JsonConvert.SerializeObject(game);
                //_writer.WriteLine(jsonString);
                string input = JsonSerializer.Serialize(_data);
                StreamWriter streamWriter = new StreamWriter(path);
                streamWriter.Write(input);
                streamWriter.Flush();
                streamWriter.Close();
            } while (puertoRico.Count < 1);
            timer.Stop();
            TimeSpan ElapsedTime = timer.Elapsed;
            //string name = "thread " + (num) + " cost " + ElapsedTime.TotalMilliseconds;
            //string path1 = "C:\\Users\\AUser\\Downloads\\" + name + ".json";
            //StreamWriter streamWriter1 = new StreamWriter(path1);
            //streamWriter1.Write("name");
            //streamWriter1.Flush();
            //streamWriter1.Close();
        }
    }
}
