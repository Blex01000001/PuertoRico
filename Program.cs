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

namespace PuertoRicoSpace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PuertoRico> puertoRico = new List<PuertoRico>();
            Guid guid = Guid.NewGuid();
            string path = "C:\\Users\\AUser\\Downloads\\" + guid + ".json";
            ////manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5+1+2+2+1+1.5+2.5+4+2+3;
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

                Console.WriteLine($"**player name: {_data.PlayerList[0].Name}");
                Console.WriteLine($"**player worker: {_data.PlayerList[0].Worker}");
                Console.WriteLine($"**player list count: {_data.PlayerList.Count}");
                string input = JsonSerializer.Serialize(_data);

                //var jsonString = JsonConvert.SerializeObject(game);
                //Console.WriteLine(jsonString);
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

            Console.WriteLine($"{da.ElapsedTime}");
            Console.WriteLine($"{da.PlayerNum}");
            Console.WriteLine($"{da.Round}"); 
            Console.WriteLine($"{da.TotalScore}");
            foreach (Player player in da.PlayerList)
            {
                Console.WriteLine($"player name: {player.Name}");
                foreach (var cargo in player.Cargos)
                {
                    Console.WriteLine($"\tcargo name: {cargo.Name}  Qty: {cargo.Qty}");
                }

            }


            Console.WriteLine($"PlayerList[0].Cargos[1].name: {da.PlayerList[1].Cargos[1].Name}");
            Console.WriteLine($"PlayerList[0].Cargos[1].Qty: {da.PlayerList[1].Cargos[1].Qty}");
            Console.WriteLine($"PlayerList[0].Cargos[2].name: {da.PlayerList[2].Cargos[2].Name}");
            Console.WriteLine($"PlayerList[0].Cargos[2].Qty: {da.PlayerList[2].Cargos[2].Qty}");
            Console.WriteLine($"PlayerList[0].Cargos[3].name: {da.PlayerList[3].Cargos[3].Name}");
            Console.WriteLine($"PlayerList[0].Cargos[3].Qty: {da.PlayerList[3].Cargos[3].Qty}");
            Console.WriteLine($"PlayerList[0].Cargos[4].name: {da.PlayerList[4].Cargos[4].Name}");
            Console.WriteLine($"PlayerList[0].Cargos[4].Qty: {da.PlayerList[4].Cargos[4].Qty}");

            Console.WriteLine($"PlayerList[0].name: {da.PlayerList[0].Name}");
            Console.WriteLine($"PlayerList[0].money: {da.PlayerList[0].Money}");
            Console.WriteLine($"PlayerList[0].Worker: {da.PlayerList[0].Worker}");
            Console.WriteLine($"PlayerList[0].Score: {da.PlayerList[0].Score}");


            Console.WriteLine($"player list count: {da.PlayerList.Count}");
            foreach (Player player in da.PlayerList)
            {
                Console.WriteLine($"player name: {player.Name}");
                Console.WriteLine($"player worker: {player.Worker}");

            }




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
