using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    public class Captain : RoleAbstract//船長
    {
        public override string Name => "Captain   ";
        private bool Privilege = true;
        public override void Action(Player player, PuertoRico game)
        {
            //從船長開始，依照上下家次序，將各項物資運上貨船，以運回舊大陸。任何人依據規則，只要有物資可以運上船，就必須要將物資運上船。物資運輸的輪替會一直重複，直到所有人都無法將物資運上船為止。
            Console.WriteLine($"\t{Name} Action");
            List<Player> playerListFromRole = game.GetPlayerListFromRole(player);
            List<bool> checkAllHasStrategy = new List<bool>();

            do
            {
                checkAllHasStrategy.Clear();
                foreach (Player p1 in playerListFromRole)
                {
                    List<TransportStrategy> Strategies = new List<TransportStrategy>();
                    foreach (CargoAbstract good in p1.Cargos)
                    {
                        if (good.Qty <= 0)//貨物為0不用算策略
                            continue;
                        if (Utilities.CheckBuildingWithWorker(p1, typeof(Wharf)) && p1.UsedStealthShip)//隱形船
                        {
                            Ship stealthShip = new Ship(99, "Stealth");
                            TransportStrategy strategy = CheckStrategy(stealthShip, good);
                            if (strategy != null)
                                Strategies.Add(strategy);
                            continue;
                        }
                        foreach (Ship ship in game.Bank.Ships)
                        {
                            TransportStrategy strategy = CheckStrategy(ship, good);
                            if (strategy != null)
                                Strategies.Add(strategy);
                        }
                    }
                    if (Strategies.Count == 0)
                    {
                        checkAllHasStrategy.Add(false);
                        Console.WriteLine($"\t\t{p1.Name} No Strategies");
                        continue;
                    }

                    int topScore = Strategies.Max(y => y.Score);//找出得分最高的策略分數
                    Strategies = Strategies.OrderBy(x => Utilities.RndNum()).ToList();//打亂策略順序
                    TransportStrategy executionStrategy = Strategies.First(x => x.Score == topScore);
                    executionStrategy.Transport(p1, game);//執行分數最高的第一個策略
                    checkAllHasStrategy.Add(true);
                    //船長能夠將物資運上船，則他在整段運輸物資的過程可以多得一分（特權）
                    CaptainPrivilege(p1,game, executionStrategy);
                }

            } while (checkAllHasStrategy.Exists(x => x == true));

            game.Bank.CheckCargoShip();//檢查船是否裝滿貨物

            //從船長開始，每個玩家必須選擇手中餘下的商品中的一份留下（並非一種，而是只能留有一份(個/箱)），其餘歸還銀行
            Console.WriteLine($"\tCheck Cargo Spoilage:");
            CheckCargoSpoilage(player, game);


        }
        private TransportStrategy CheckStrategy(Ship ship, CargoAbstract good)
        {
            if (ship.Cargo != good.Name && ship.Cargo != null)//貨物跟船上的貨物不同，無法運上船
                return null;
            return new TransportStrategy(ship, good);
        }
        private void CaptainPrivilege(Player p1, PuertoRico game, TransportStrategy executionStrategy)
        {
            if (Privilege && p1.Role == "Captain   ")
            {
                Console.WriteLine($"\t\t{p1.Name} get 1 Score(Captain Privilege)");
                p1.IncreaseScore(game.Bank.GetScore(1));
                Privilege = false;
            }
            if (executionStrategy.IsStealth)
                p1.UseStealthShip();
        }
        private void CheckCargoSpoilage(Player player, PuertoRico game)
        {
            foreach (Player p1 in game.GetPlayerListFromRole(player))
            {
                Console.WriteLine("");
                List<CargoAbstract> cargos = p1.Cargos.Where(x => x.Qty > 0).OrderByDescending(x => x.Qty).ToList();
                bool hasSmallwarehouse = Utilities.CheckBuildingWithWorker(p1, typeof(Smallwarehouse));
                bool hasLargewarehouse = Utilities.CheckBuildingWithWorker(p1, typeof(Largewarehouse));
                int retainLimit = 0;//計算可以持有幾種商品可以不腐敗
                if (hasSmallwarehouse) retainLimit += 1;
                if (hasLargewarehouse) retainLimit += 2;
                //Console.WriteLine($"\t\t{p1.Name} cargos.Count: {cargos.Count}");
                p1.ShowCargo();
                Console.WriteLine($"\t\t{p1.Name} retainLimit: {retainLimit}");


                if(cargos.Count == 0 || cargos == null)//貨品皆為0，沒有貨品可以腐爛
                {
                    Console.WriteLine($"\t\t{p1.Name} No Cargo Spoilage");
                    continue;
                }

                for (int i = 0; i < retainLimit; i++)//有倉庫就保留
                {
                    if (cargos.Count == 0) continue;
                    Console.WriteLine($"\t\t{p1.Name} can keep {cargos[0].Name} {cargos[0].Qty} (Warehouse)");
                    cargos.RemoveAt(0);
                }

                if (cargos.Count == 0) continue;

                if(cargos[0].Qty == 1)
                {
                    Console.WriteLine($"\t\t{p1.Name} can keep {cargos[0].Name} {cargos[0].Qty} (Spoilage Rule)");
                }
                else if (cargos[0].Qty > 1)//貨物多餘1的就腐爛
                {
                    Console.WriteLine($"\t\t{p1.Name} can keep {cargos[0].Name} 1 (Spoilage Rule)");
                    int decreaseCargo = p1.DecreaseCargo(cargos[0].Name, cargos[0].Qty - 1);
                    //Console.WriteLine($"\t\t{p1.Name} decrease {cargos[0].Name} {decreaseCargo}, bank{game.Bank.GetCargoQty(cargos[0].GetType())}");
                    game.Bank.AddCargo(cargos[0].GetType(), decreaseCargo);
                    Console.WriteLine($"\t\t{p1.Name} {cargos[0].Name} Spoilage {decreaseCargo}");
                    //Console.WriteLine($"\t\tbank{game.Bank.GetCargoQty(cargos[0].GetType())}");
                    //} else {
                }
                cargos.RemoveAt(0);

                if (cargos.Count == 0) continue;
                //Console.WriteLine($"\t\t\t{p1.Name} cargos.Count: {cargos.Count}");

                foreach (CargoAbstract cargo in cargos)//剩下的全部都繳回
                {
                    //Console.WriteLine($"\t\t***");
                    //Console.WriteLine($"\t\tbank {game.Bank.GetCargoQty(cargo.GetType())}");
                    string cargoName = cargo.Name;
                    int cargoQty = cargo.Qty;
                    game.Bank.AddCargo(cargo.GetType(), p1.DecreaseCargo(cargoName, cargoQty));
                    Console.WriteLine($"\t\t{p1.Name} {cargo.Name} Spoilage {cargoQty}");
                   // Console.WriteLine($"\t\tbank {game.Bank.GetCargoQty(cargo.GetType())}");
                }

                p1.ShowCargo();
            }
        }
    }
    internal class TransportStrategy
    {
        public int Score { get; private set; }
        public bool IsStealth { get; private set; }
        private CargoAbstract _good;
        private Ship _ship;
        private bool _isStealth = false;
        public TransportStrategy(Ship ship, CargoAbstract good)
        {
            this._good = good;
            this._ship = ship;
            if (ship.Type == "Stealth")
                _isStealth = true;
            if (ship.Cargo == null)//船如果是空的，分數就是貨物的數量
            {
                ship.SetCargo(good.Name);
                Score = good.Qty > ship.MaxCargoQuantity ? ship.MaxCargoQuantity : good.Qty;
            }
            else
            {
                int shipEmpty = ship.MaxCargoQuantity - ship.Quantity;
                Score = good.Qty > shipEmpty ? shipEmpty : good.Qty;
            }
        }
        public void Transport(Player player, PuertoRico game)
        {
            Console.WriteLine($"\t\t{player.Name} Transport {Score} {_good.Name} to the ship({_ship.Type})");
            player.DecreaseCargo(_good.Name, Score);
            player.IncreaseScore(Score);
            _ship.AddQuantity(Score);
            game.Bank.GetScore(Score);
        }
    }

}
