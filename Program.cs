using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PuertoRicoSpace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PuertoRico> puertoRico = new List<PuertoRico>();
            //string path = "C:\\Users\\AUser\\Downloads\\" + Guid.NewGuid() + ".json";
            ////manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5+1+2+2+1+1.5+2.5+4;
            //do
            //{
            //    PuertoRico game = new PuertoRico(5);
            //    if(game.TotalScore > 10)
            //    {
            //        puertoRico.Add(game);
            //    }
            //    var jsonString = JsonConvert.SerializeObject(game);
            //    Console.WriteLine(jsonString);
            //    StreamWriter streamWriter = new StreamWriter(path);
            //    streamWriter.Write(jsonString);
            //    streamWriter.Flush();
            //    streamWriter.Close();
            //} while (puertoRico.Count < 1);


            string filePath = "C:\\Users\\AUser\\Downloads\\65c009fb-08a9-4e56-85a2-4f20d957622f.json";
            string json = File.ReadAllText(filePath);
            PuertoRico game1 = JsonConvert.DeserializeObject<PuertoRico>(json);
            puertoRico.Add(game1);

            //Console.WriteLine("A\tB\tC\tD\tE\tRound\ttotal\ttime");
            Console.WriteLine("No.1\tNo.2\tNo.3\tNo.4\tNo.5\tRound\ttotal\ttime");
            foreach (PuertoRico game in puertoRico)
            {
                int total = 0;
                List<Player> list = game.PlayerList.OrderByDescending(x => x.Score).ToList();
                foreach (Player player in list)
                {
                    total += player.Score;
                    Console.Write($"{player.Score}");
                    if(player.BuildingList.Where(x => x.Name == "Wharf").ToList().Count > 0)
                    {
                        Console.Write($"W");
                    }
                    Console.Write($"\t");
                }
                Console.Write($"{game.Round}\t{game.TotalScore}\t{game.ElapsedTime.ToString("s\\.fffff")}");
                Console.Write("\n");
            }

            Console.ReadLine();
        }
    }
}
