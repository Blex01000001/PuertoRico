using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace PuertoRico
{
    internal class data
    {
        public int PlayerNum { get;  set; }
        public TimeSpan ElapsedTime { get;  set; }//遊戲經過時間
        public int Round { get;  set; }
        public int TotalScore { get;  set; }
        public List<Player> PlayerList { get; set; }
        public Bank Bank { get; set; }

        public void ShowBankStatus()
        {
            Console.WriteLine("--------Bank status--------");
            Console.WriteLine($"Item      \tQTY");
            Console.WriteLine($"WorkerShip\t{Bank.WorkerShip}");
            Console.WriteLine($"Score     \t{Bank.Score}");
            Console.WriteLine($"Worker    \t{Bank.Worker}");
            Console.WriteLine($"Money     \t{Bank.Money}");
            Console.WriteLine($"Corn      \t{Bank.Cargos[0].Qty}");
            Console.WriteLine($"Sugar     \t{Bank.Cargos[1].Qty}");
            Console.WriteLine($"Coffee    \t{Bank.Cargos[2].Qty}");
            Console.WriteLine($"Tobacco   \t{Bank.Cargos[3].Qty}");
            Console.WriteLine($"Indigo    \t{Bank.Cargos[4].Qty}");
        }
        //public void ShowShopGoods()
        //{
        //    Console.Write("\n");
        //    Console.Write($"ShopGoods:");
        //    foreach (CargoAbstract good in Shop)
        //    {
        //        Console.Write($" {good.Name}");
        //    }
        //    Console.Write($"\n");
        //}
        public void ShowPlayerStatus()
        {
            Console.WriteLine("--------player status--------");
            Console.WriteLine($"Name\tScore\tMoney\tWorker\tCorn\tSugar\tCoffee\tTobacco\tIndigo\t");
            foreach (Player player in PlayerList)
            {
                Console.Write($"{player.Name}    \t{player.Score}   \t{player.Money}   \t{player.Worker}    \t{player.Cargos[0].Qty}   \t{player.Cargos[1].Qty}    \t{player.Cargos[2].Qty}     \t{player.Cargos[3].Qty}      \t{player.Cargos[4].Qty}\n");
            }
            Console.Write("\n");
            Console.Write($"Field:\n");
            foreach (Player player in PlayerList)
            {
                Console.Write($"{player.Name} ");
                foreach (BuildingAbstract field in player.FarmList)
                {
                    Console.Write($"{field.Name}({field.GetHexHash()})({field.Worker}/{field.MaxWorker})\t, ");
                }
                Console.Write($"\n");
            }

            Console.Write($"\nBuilding:\n");
            foreach (Player player in PlayerList)
            {
                Console.Write($"{player.Name} ");
                foreach (BuildingAbstract building in player.BuildingList)
                {
                    Console.Write($"{building.Name} ({building.GetHexHash()}) ({building.Worker}/{building.MaxWorker}), ");
                }
                Console.Write($"\n");
            }
        }

    }
}
