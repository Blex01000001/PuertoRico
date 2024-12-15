using PuertoRico.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico
{
    public  class PuertoRico
    {
        public int PlayerNum { get; private set; }
        public Bank Bank { get; private set; }
        public List<Player> PlayerList { get; private set; }
        public List<Player> PlayerListByGovernor { get; private set; }
        public List<RoleAbstract> AvailableRoles { get; private set; }//角色List
        public List<RoleAbstract> SelectedRoles { get; private set; }

        private bool EndGame = false;
        private int Round = 0;

        public PuertoRico(int playerNum)
        {
            this.PlayerNum = playerNum;
            Bank = new Bank();
            Bank.SetUp(PlayerNum);
            CreatePlayers(PlayerNum);
            CreateRoles(PlayerNum);
            GameStartSetUp(PlayerNum);

            while (!EndGame)
            {
                Console.WriteLine($"==========ROUND {Round + 1}==========");

                AvailableRoles = AvailableRoles.OrderBy(x => Utilities.RndNum()).ToList();
                foreach (Player player in PlayerListByGovernor)
                {
                    Console.WriteLine($"{player.Name} select {AvailableRoles[0].Name}");
                    player.SetRole(AvailableRoles[0].Name);
                    if (AvailableRoles[0].Money > 0)//玩家所選的角色牌上如果有錢就加到玩家裡
                    {
                        player.IncreaseMoney(AvailableRoles[0].Money);
                        Console.WriteLine($"\t{player.Name} get {AvailableRoles[0].Money} money from Role, {player.Name} Sum Money: {player.Money}, Bank: {Bank.Money}");
                        AvailableRoles[0].ResetMoney();//角色牌所累積的錢歸零
                    }
                    AvailableRoles[0].Action(player, this);
                    SelectedRoles.Add(AvailableRoles[0]);
                    AvailableRoles.Remove(AvailableRoles[0]);
                }

                Console.WriteLine($"==========ROUND {Round + 1} END==========");

                foreach (RoleAbstract roles in AvailableRoles)//沒有被選到的角色的錢+1
                {
                    //Console.WriteLine($"remaining roles: {roles.Name} Money +1");
                    roles.AddMoney(Bank.GetMoney(1));
                }

                AvailableRoles.AddRange(SelectedRoles);//將被選過的角色加回去AvailableRoles
                SelectedRoles.RemoveAll(x => true);

                ShowAvailableRolesStatus();
                //ShowAvailableFarms();
                //ShowHideFarms();
                ShowBankStatus();
                
                //ShowShopGoods();
                ShowCargo();
                ShowPlayerStatus();

                NextGovernor();//換下一個人當總督
                ClearPlayerRoles();//清空每個人所選的角色
                Console.WriteLine("\n");

                Round++;
            }



        }
        public void CallGame()
        {
            EndGame = true;
        }
        private void GameStartSetUp(int playerNum)
        {
            //遊戲一開始每個人分得N-1元貨幣，N為遊戲人數。這些錢就放在各自島嶼板上的空位讓大家看到
            foreach (Player player in PlayerList)
            {
                player.IncreaseMoney(Bank.GetMoney(playerNum - 1));
            }
            //根據參加人數不同，每個人得到的第一個農田方塊不同：
            //3個人遊玩：第1、2家為染料田，第3家為玉米田。
            //4個人遊玩：第1、2家為染料田，第3、4家為玉米田。
            //5個人遊玩：第1、2、3家為染料田，第4、5家為玉米田。
            Utilities.shift(Bank.HideFarms.Find(x => x.GetType() == typeof(IndigoFarm)), PlayerList[0].FarmList, Bank.HideFarms);
            Utilities.shift(Bank.HideFarms.Find(x => x.GetType() == typeof(IndigoFarm)), PlayerList[1].FarmList, Bank.HideFarms);
            switch (playerNum)
            {
                case 3:
                    Utilities.shift(Bank.HideFarms.Find(x => x.GetType() == typeof(CornFarm)), PlayerList[2].FarmList, Bank.HideFarms);
                    break;
                case 4:
                    Utilities.shift(Bank.HideFarms.Find(x => x.GetType() == typeof(CornFarm)), PlayerList[2].FarmList, Bank.HideFarms);
                    Utilities.shift(Bank.HideFarms.Find(x => x.GetType() == typeof(CornFarm)), PlayerList[3].FarmList, Bank.HideFarms);
                    break;
                case 5:
                    Utilities.shift(Bank.HideFarms.Find(x => x.GetType() == typeof(IndigoFarm)), PlayerList[2].FarmList, Bank.HideFarms);
                    Utilities.shift(Bank.HideFarms.Find(x => x.GetType() == typeof(CornFarm)), PlayerList[3].FarmList, Bank.HideFarms);
                    Utilities.shift(Bank.HideFarms.Find(x => x.GetType() == typeof(CornFarm)), PlayerList[4].FarmList, Bank.HideFarms);
                    break;
            }
            Bank.HideFarms = Bank.HideFarms.OrderBy(x => Utilities.RndNum()).ToList();
            Bank.AvailableFarms = Bank.HideFarms.Take(playerNum + 1).OrderBy(x => Utilities.RndNum()).ToList();
            foreach (var item in Bank.AvailableFarms)
            {
                Bank.HideFarms.Remove(item);
            }

        }
        private void NextGovernor()
        {
            PlayerListByGovernor.Add(PlayerListByGovernor[0]);//將第一人移至最後
            PlayerListByGovernor.RemoveAt(0);//刪除第一人
        }
        private void ClearPlayerRoles()
        {
            foreach (Player player in PlayerList)
            {
                player.RemoveRole();
            }
        }

        private void CreatePlayers(int playerNum)
        {
            PlayerList = new List<Player>();
            PlayerListByGovernor = new List<Player>();
            for (int i = 0; i < playerNum; i++)
            {
                Player player = new Player(Convert.ToChar(65 + i).ToString());
                PlayerList.Add(player);
                PlayerListByGovernor.Add(player);
            }
        }
        private void CreateRoles(int playerNum)
        {
            AvailableRoles = new List<RoleAbstract>();
            SelectedRoles = new List<RoleAbstract>();

            RoleAbstract Settler = new Settler();//開拓者
            AvailableRoles.Add(Settler);

            RoleAbstract Mayor = new Mayor();//市長
            AvailableRoles.Add(Mayor);

            RoleAbstract Builder = new Builder();//建築師
            AvailableRoles.Add(Builder);

            RoleAbstract Craftsman = new Craftsman();//工匠
            AvailableRoles.Add(Craftsman);

            RoleAbstract Trader = new Trader();//交易商
            AvailableRoles.Add(Trader);

            RoleAbstract Captain = new Captain();//船長
            AvailableRoles.Add(Captain);

            switch (playerNum)//探勘者
            {
                case 4:
                    RoleAbstract Prospector41 = new Prospector();
                    AvailableRoles.Add(Prospector41);
                    break;
                case 5:
                    RoleAbstract Prospector51 = new Prospector();
                    AvailableRoles.Add(Prospector51);
                    RoleAbstract Prospector52 = new Prospector();
                    AvailableRoles.Add(Prospector52);
                    break;
            }
        }

        public List<Player> GetPlayerListFromRole(Player player)
        {
            List<Player> templist = new List<Player>();
            int playerIndex = PlayerList.FindIndex(x => x == player);
            for (int i = 0; i < PlayerList.Count; i++)
            {
                int ii = (i % PlayerList.Count) + playerIndex > (PlayerList.Count - 1) ? (i % PlayerList.Count) + playerIndex - PlayerList.Count : (i % PlayerList.Count) + playerIndex;
                templist.Add(PlayerList[ii]);
            }
            return templist;
        }
        private void ShowAvailableRolesStatus()
        {
            Console.WriteLine("--------availableRoles status--------");
            Console.WriteLine($"Role\t\tMoney");
            foreach (RoleAbstract roles in AvailableRoles.Where(x => x.Money > 0))
            {
                Console.WriteLine($"{roles.Name} \t  {roles.Money}");
            }
        }
        private void ShowBankStatus()
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
        public void ShowPlayerStatus()
        {
            Console.WriteLine("--------player status--------");
            Console.WriteLine($"Name\tScore\tMoney\tWorker\tCorn\tSugar\tCoffee\tTobacco\tIndigo\t");
            foreach (Player player in PlayerList)
            {
                Console.Write($"{player.Name}    \t{player.Score}   \t{player.Money}   \t{player.Worker}    \t{player.Cargos[0].Qty}   \t{player.Cargos[1].Qty}    \t{player.Cargos[2].Qty}     \t{player.Cargos[3].Qty}      \t{player.Cargos[4].Qty}\n");
            }
            Console.Write("\n");
            Console.Write($"field:\n");
            foreach (Player player in PlayerList)
            {
                Console.Write($"{player.Name} ");
                foreach (BuildingAbstract field in player.FarmList)
                {
                    Console.Write($"{field.Name}({field.GetHashCode()})({field.Worker}/{field.MaxWorker})\t, ");
                }
                Console.Write($"\n");
            }

            Console.Write($"\nbuilding:\n");
            foreach (Player player in PlayerList)
            {
                Console.Write($"{player.Name} ");
                foreach (BuildingAbstract building in player.BuildingList)
                {
                    Console.Write($"{building.Name} ({building.GetHashCode()}) ({building.Worker}/{building.MaxWorker}), ");
                }
                Console.Write($"\n");
            }
        }

        internal void ArrangeFarms()
        {
            for (int i = 0; i < Bank.AvailableFarms.Count; i++)
            {
                Bank.TrushFarms.Add(Bank.AvailableFarms[i]);
                Bank.TrushFarms.OrderBy(x => Utilities.RndNum());
            }
            Bank.AvailableFarms.Clear();

            while (Bank.HideFarms.Count < PlayerNum + 1)
            {
                if (Bank.TrushFarms.Count <= 0)
                    break;
                foreach (BuildingAbstract farm in Bank.TrushFarms)
                {
                    Bank.HideFarms.Add(farm);
                }
                Bank.TrushFarms.Clear();
            }

            if (Bank.HideFarms.Count >= PlayerNum + 1)
            {
                Bank.AvailableFarms = Bank.HideFarms.Take(PlayerNum + 1).ToList();
            }
            else
            {
                Bank.AvailableFarms = Bank.HideFarms.Take(Bank.HideFarms.Count).ToList();
            }


            for (int i = 0; i < Bank.AvailableFarms.Count; i++)
            {
                Bank.HideFarms.Remove(Bank.AvailableFarms[i]);
            }


        }
        public void CheckCargo()
        {
            foreach (Ship ship in Bank.Ships)
            {
                if (ship.Quantity >= ship.MaxCargoQuantity)
                {
                    ship.Reset();
                    Console.WriteLine($"***Ship({ship.GetHashCode()}) has been clear***");
                }
            }
            Console.WriteLine("");
        }
        public void ShowCargo()
        {
            foreach (Ship ship in Bank.Ships)
            {
                Console.Write($"Ship({ship.GetHashCode()})\t");
            }
            Console.Write("\n");
            foreach (Ship ship in Bank.Ships)
            {
                Console.Write($"{ship.Cargo}\t\t\t");
            }
            Console.Write("\n");
            foreach (Ship ship in Bank.Ships)
            {
                Console.Write($"{ship.Quantity}\t\t");
            }
            Console.Write("\n");
        }


    }
}
