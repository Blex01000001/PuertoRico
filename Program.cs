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

namespace PuertoRicoSpace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PuertoRico> puertoRico = new List<PuertoRico>();
            Guid guid = Guid.NewGuid();
            string path = "C:\\Users\\AUser\\Downloads\\" + guid + ".json";
            ////manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5+1+2+2+1+1.5+2.5+4+2+3+3.5+1;
            do
            {
                PuertoRico game = new PuertoRico(5);
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
                //Console.WriteLine(jsonString);
                string input = JsonSerializer.Serialize(_data);
                StreamWriter streamWriter = new StreamWriter(path);
                streamWriter.Write(input);
                streamWriter.Flush();
                streamWriter.Close();
            } while (puertoRico.Count < 1);

            //var settings = new JsonSerializerSettings
            //{
            //    Converters = { new PuertoRicoJsonConverter() },
            //    Formatting = Formatting.Indented
            //};

            string filePath = path;
            string jsonInput = File.ReadAllText(filePath);


            //PuertoRico game1 = JsonConvert.DeserializeObject<PuertoRico>(jsonInput);

            //PuertoRico game1 = JsonSerializer.Deserialize<PuertoRico>(jsonInput);
            data da = JsonSerializer.Deserialize<data>(jsonInput);

            string typeName = "QuarryFarm";
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetType($"PuertoRicoSpace.{typeName}");
            BuildingAbstract build = (BuildingAbstract)Activator.CreateInstance(type);

            Console.WriteLine($"**********");
            Console.WriteLine($"**********{build.Name}");
            Console.WriteLine($"**********");
            //D:\Code\C#\PuertoRico\Building\
            //Console.WriteLine($"{da.ElapsedTime}");
            //Console.WriteLine($"{da.PlayerNum}");
            //Console.WriteLine($"{da.Round}"); 
            //Console.WriteLine($"{da.TotalScore}");
            //foreach (Player player in da.PlayerList)
            //{
            //    Console.WriteLine($"player name: {player.Name} money: {player.Money} score: {player.Score} worker: {player.Worker} ");
            //    Console.Write("cargo:");
            //    foreach (var cargo in player.Cargos)
            //    {
            //        Console.Write($"{cargo.Name}  Qty: {cargo.Qty} ");
            //    }
            //    Console.Write("\n");
            //    Console.Write("farm list: ");
            //    foreach (var farm in player.FarmList)
            //    {
            //        Console.Write($"{farm.Name}({farm.Worker}/{farm.MaxWorker}) ");
            //    }
            //    Console.Write("\n");
            //    Console.Write("building list: ");
            //    foreach (var building in player.BuildingList)
            //    {
            //        Console.Write($"{building.Name}({building.Worker}/{building.MaxWorker}) ");
            //    }
            //    Console.Write("\n");
            //}

            da.ShowBankStatus();
            da.ShowPlayerStatus();
            da.ShowCargo();

            //puertoRico.Add(game1);

            //PuertoRico game1 = JsonConvert.DeserializeObject<PuertoRico>(json);
            //puertoRico.Add(game1);

            //Console.WriteLine("A\tB\tC\tD\tE\tRound\ttotal\ttime");
            //Console.WriteLine("No.1\tNo.2\tNo.3\tNo.4\tNo.5\tRound\ttotal\ttime");
            //foreach (PuertoRico game in puertoRico)
            //{
            //    int total = 0;
            //    List<Player> list = game.PlayerList.OrderByDescending(x => x.Score).ToList();
            //    foreach (Player player in list)
            //    {
            //        total += player.Score;
            //        Console.Write($"{player.Score}");
            //        if (player.BuildingList.Where(x => x.Name == "Wharf").ToList().Count > 0)
            //        {
            //            Console.Write($"W");
            //        }
            //        Console.Write($"\t");
            //    }
            //    Console.Write($"{game.Round}\t{game.TotalScore}\t{game.ElapsedTime.ToString("s\\.fffff")}");
            //    Console.Write("\n");
            //}

            Console.ReadLine();
        }
    }
}
