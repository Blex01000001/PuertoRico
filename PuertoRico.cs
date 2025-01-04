using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PuertoRicoSpace
{
    public  class PuertoRico
    {
        public int PlayerNum { get; private set; }//玩家人數
        public Bank Bank { get; private set; }
        public List<Player> PlayerList { get; private set; }
        public List<Player> PlayerListByGovernor { get; private set; }
        public List<RoleAbstract> AvailableRoles { get; private set; }//角色List
        public List<CargoAbstract> Shop { get; private set; }//商店四格商品的空間
        public TimeSpan ElapsedTime { get; private set; }//遊戲經過時間
        public int Round { get; private set; }
        public int TotalScore { get; private set; }
        private bool EndGame = false;
        [JsonIgnore]
        public StreamWriter _writer { get; private set; }
        private string _filePath;

        public PuertoRico(int playerNum, Guid guid)
        {
            _filePath = "D:\\Code\\C#\\PuertoRicoData\\" + guid + ".txt";
            _writer = new StreamWriter(_filePath);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            this.PlayerNum = playerNum;
            Bank = new Bank();
            Shop = new List<CargoAbstract>();
            Bank.SetUp(PlayerNum, _writer);
            CreatePlayers(PlayerNum);
            CreateRoles(PlayerNum);
            GameStartSetUp(PlayerNum);
            
            while (!EndGame)
            {
                _writer.WriteLine($"==========ROUND {Round + 1}==========");
                AdjustmentPriority();
                List<RoleAbstract> priorityRoles = Utilities.RandomOrderByPriority(AvailableRoles);
                foreach (Player player in PlayerListByGovernor)
                {
                    RoleAbstract selectedRole = priorityRoles[0];
                    _writer.WriteLine($"{player.Name} select {selectedRole.Name}");
                    player.SetRole(selectedRole.Name);
                    if (selectedRole.Money > 0)//玩家所選的角色牌上如果有錢就加到玩家裡
                    {
                        player.AddMoney(selectedRole.Money);
                        _writer.WriteLine($"\t{player.Name} get {selectedRole.Money} money from Role, {player.Name} Sum Money: {player.Money}, Bank: {Bank.Money}");
                        selectedRole.ResetMoney();//角色牌所累積的錢歸零
                    }
                    selectedRole.Action(player, this);
                    priorityRoles.Remove(selectedRole);
                }

                _writer.WriteLine($"==========ROUND {Round + 1} END==========");

                foreach (RoleAbstract roles in priorityRoles)//沒有被選到的角色的錢+1
                {
                    //_writer.WriteLine($"remaining roles: {roles.Name} Money +1");
                    roles.AddMoney(Bank.GetMoney(1));
                }

                ShowAvailableRolesStatus();
                //ShowAvailableFarms();
                //ShowHideFarms();
                ShowBankStatus();
                ShowShopGoods();
                ShowCargo();
                ShowPlayerStatus();

                NextGovernor();//換下一個人當總督
                ClearPlayerRoles();//清空每個人所選的角色
                _writer.WriteLine("\n");
                Round++;
            }
            CalculateScore();

            timer.Stop();
            // Get the elapsed time as a TimeSpan value.
            ElapsedTime = timer.Elapsed;
            _writer.Flush();
            _writer.Close();
            ShowConsole();
        }
        /// <summary>
        /// 將EndGame設為TRUE，代表遊戲達到結束條件
        /// </summary>
        public void CallGame()
        {
            EndGame = true;
        }
        private void GameStartSetUp(int playerNum)
        {
            //遊戲一開始每個人分得N-1元貨幣，N為遊戲人數。這些錢就放在各自島嶼板上的空位讓大家看到
            foreach (Player player in PlayerList)
            {
                player.AddMoney(Bank.GetMoney(playerNum - 1));
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
        /// <summary>
        /// 換下一個人當總督
        /// </summary>
        private void NextGovernor()
        {
            PlayerListByGovernor.Add(PlayerListByGovernor[0]);//將第一人移至最後
            PlayerListByGovernor.RemoveAt(0);//刪除第一人
        }
        /// <summary>
        /// 清除所有PlayerList裡Player所選的角色
        /// </summary>
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
            _writer.WriteLine("--------availableRoles status--------");
            _writer.WriteLine($"Role\t\tMoney");
            foreach (RoleAbstract roles in AvailableRoles.Where(x => x.Money > 0))
            {
                _writer.WriteLine($"{roles.Name} \t  {roles.Money}");
            }
        }
        private void ShowBankStatus()
        {
            _writer.WriteLine("--------Bank status--------");
            _writer.WriteLine($"Item      \tQTY");
            _writer.WriteLine($"WorkerShip\t{Bank.WorkerShip}");
            _writer.WriteLine($"Score     \t{Bank.Score}");
            _writer.WriteLine($"Worker    \t{Bank.Worker}");
            _writer.WriteLine($"Money     \t{Bank.Money}");
            _writer.WriteLine($"Corn      \t{Bank.Cargos[0].Qty}");
            _writer.WriteLine($"Sugar     \t{Bank.Cargos[1].Qty}");
            _writer.WriteLine($"Coffee    \t{Bank.Cargos[2].Qty}");
            _writer.WriteLine($"Tobacco   \t{Bank.Cargos[3].Qty}");
            _writer.WriteLine($"Indigo    \t{Bank.Cargos[4].Qty}");
        }
        public void ShowPlayerStatus()
        {
            _writer.WriteLine("--------player status--------");
            _writer.WriteLine($"Name\tScore\tMoney\tWorker\tCorn\tSugar\tCoffee\tTobacco\tIndigo\t");
            foreach (Player player in PlayerList)
            {
                _writer.Write($"{player.Name}    \t{player.Score}   \t{player.Money}   \t{player.Worker}    \t{player.Cargos[0].Qty}   \t{player.Cargos[1].Qty}    \t{player.Cargos[2].Qty}     \t{player.Cargos[3].Qty}      \t{player.Cargos[4].Qty}\n");
            }
            _writer.Write("\n");
            _writer.Write($"Field:\n");
            foreach (Player player in PlayerList)
            {
                _writer.Write($"{player.Name}: ");
                foreach (BuildingAbstract field in player.FarmList)
                {
                    string str = field.Name + "(" + field.GetHexHash() + ")";
                    string strWorker = "(" + field.Worker + "/" + field.MaxWorker + ")";
                    _writer.Write("{0,-17}{1,6},  ", str, strWorker);
                }
                _writer.Write($"\n");
            }

            _writer.Write($"\nBuilding:\n");
            foreach (Player player in PlayerList)
            {
                _writer.Write($"{player.Name}: ");
                foreach (BuildingAbstract building in player.BuildingList)
                {
                    string str = building.Name + "(" + building.GetHexHash() + ")";
                    string strWorker = "(" + building.Worker + "/" + building.MaxWorker + ")";
                    _writer.Write("{0,-23}{1,6},  ", str, strWorker);
                }
                _writer.Write($"\n");
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
                    _writer.WriteLine($"***Ship({ship.GetHexHash()}) has been clear***");
                }
            }
            _writer.WriteLine("");
        }
        public void ShowCargo()
        {
            _writer.Write("\n");
            foreach (Ship ship in Bank.Ships)
            {
                _writer.Write($"Ship({ship.GetHexHash()})({ship.Quantity}/{ship.MaxCargoQuantity})\t");
            }
            _writer.Write("\n");
            foreach (Ship ship in Bank.Ships)
            {
                _writer.Write($"{ship.Cargo}\t\t");
            }
            _writer.Write("\n");
            foreach (Ship ship in Bank.Ships)
            {
                _writer.Write($"{ship.Quantity}\t\t");
            }
            _writer.Write("\n");
        }
        public void ShowShopGoods()
        {
            _writer.Write("\n");
            _writer.Write($"ShopGoods:");
            foreach (CargoAbstract good in Shop)
            {
                _writer.Write($" {good.Name}");
            }
            _writer.Write($"\n");
        }
        public void CheckShop()
        {
            if (Shop.Count >= 4)
            {
                foreach (CargoAbstract cargo in Shop)
                {
                    Bank.AddCargo(cargo.GetType(), 1);
                }
                Shop.Clear();
            }
        }
        private void CalculateScore()
        {
            foreach (Player player in PlayerList)
            {
                CheckBuildingScore(player);
                int buildingScore = player.GetAllBuildings().Where(x => x.Type == "Building").Sum(x => x.Score);
                player.AddScore(buildingScore);
                TotalScore += player.Score;
            }
        }
        private void CheckBuildingScore(Player player)
        {
            if (Utilities.CheckBuildingWithWorker(player, typeof(Customshouse)))//海關，若海關作用，最後計算點數時，統計（運物資上船得到的）得分方塊總分，無條件舍去每四分多計一分。
            {
                int score = (int)(player.Score / 4);
                player.AddScore(score);
            }
            if (Utilities.CheckBuildingWithWorker(player, typeof(Guildhall)))//公會廳，若商會作用，最後計算點數時，大型生產廠房多計兩分，小型生產廠房多計一分。
            {
                int smallCount = player.GetAllBuildings().Where(x => x.Scale == "Small").ToList().Count;
                player.AddScore(smallCount);
                int largeCount = player.GetAllBuildings().Where(x => x.Scale == "Large").ToList().Count;
                player.AddScore(largeCount * 2);
            }
            if (Utilities.CheckBuildingWithWorker(player, typeof(Residence)))//府邸，若居民區作用，最後計算點數時，若郊區空格占滿九格以下，多計四分；占滿十格，多計五分；十一格六分；十二格都占滿多計七分。
            {
                int FarmCount = player.GetAllBuildings().Where(x => x.Type == "Farm").ToList().Count;
                switch (FarmCount)
                {
                    case int n when (n  <= 9):
                        player.AddScore(4);
                        break;
                    case 10:
                        player.AddScore(5);
                        break;
                    case 11:
                        player.AddScore(6);
                        break;
                    case 12:
                        player.AddScore(7);
                        break;
                }
            }
            if (Utilities.CheckBuildingWithWorker(player, typeof(Fortress)))//堡壘，若要塞作用，最後計算點數時，統計遊戲盤上所有移民總數，無條件舍去每三移民多計一分。
            {
                int score = (int)(player.Worker / 3);
                player.AddScore(score);
            }
            if (Utilities.CheckBuildingWithWorker(player, typeof(Cityhall)))//市政廳，若市政廳作用，最後計算點數時，每座紫色的特殊功能建築（不論大小）多計一分。
            {
                int buildingScore = player.GetAllBuildings().Where(x => x.Type == "Building").ToList().Count;
                player.AddScore(buildingScore);
            }
        }
        private void AdjustmentPriority()
        {                
            //選角色時
            //初期：較不會選船長、交易員；多選開拓者、建築師、市長
            //中期：
            //後期：較不會選開拓者、礦工，多選工匠
            if (Round < 3)
            {
                AvailableRoles.Find(x => x.Name == "Settler   ").SetPriority(100);
                AvailableRoles.Find(x => x.Name == "Builder   ").SetPriority(100);
                AvailableRoles.Find(x => x.Name == "Mayor     ").SetPriority(70);
                AvailableRoles.Find(x => x.Name == "Craftsman ").SetPriority(50);
                AvailableRoles.Find(x => x.Name == "Prospector").SetPriority(30);
                AvailableRoles.Find(x => x.Name == "Trader    ").SetPriority(5);
                AvailableRoles.Find(x => x.Name == "Captain   ").SetPriority(5);
            }else if (Round < 12)
            {
                AvailableRoles.Find(x => x.Name == "Settler   ").SetPriority(20);
                AvailableRoles.Find(x => x.Name == "Builder   ").SetPriority(50);
                AvailableRoles.Find(x => x.Name == "Mayor     ").SetPriority(50);
                AvailableRoles.Find(x => x.Name == "Craftsman ").SetPriority(100);
                AvailableRoles.Find(x => x.Name == "Prospector").SetPriority(10);
                AvailableRoles.Find(x => x.Name == "Trader    ").SetPriority(50);
                AvailableRoles.Find(x => x.Name == "Captain   ").SetPriority(50);
            }else
            {
                AvailableRoles.Find(x => x.Name == "Settler   ").SetPriority(10);
                AvailableRoles.Find(x => x.Name == "Builder   ").SetPriority(10);
                AvailableRoles.Find(x => x.Name == "Mayor     ").SetPriority(30);
                AvailableRoles.Find(x => x.Name == "Craftsman ").SetPriority(80);
                AvailableRoles.Find(x => x.Name == "Prospector").SetPriority(10);
                AvailableRoles.Find(x => x.Name == "Trader    ").SetPriority(50);
                AvailableRoles.Find(x => x.Name == "Captain   ").SetPriority(50);
            }

        }
        private void ShowConsole()
        {
            string sec = ElapsedTime.TotalSeconds.ToString("0.000");
            //Console.WriteLine("{0,8}{1,7}", "Time", "Score");
            Console.WriteLine("{0,8}{1,7}{2,7}", sec, TotalScore, Round);

        }
    }
}
