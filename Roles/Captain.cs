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
            List<int> playerStrategies = new List<int>();
            do
            {
                foreach (Player p1 in game.GetPlayerListFromRole(player))
                {
                    List<TransportStrategy> Strategies = new List<TransportStrategy>();
                    foreach (CargoAbstract good in p1.Cargos)
                    {
                        if (good.Qty <= 0)
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
                        playerStrategies.Add(0);
                        Console.WriteLine($"\t\t{p1.Name} No Strategies");
                        continue;
                    }
                    int topScore = Strategies.Max(y => y.Score);//找出得分最高的策略分數
                    Strategies = Strategies.OrderBy(x => Utilities.RndNum()).ToList();//打亂策略順序
                    TransportStrategy executionStrategy = Strategies.First(x => x.Score == topScore);
                    executionStrategy.Transport(p1, game);//執行分數最高的第一個策略
                    if (Privilege && p1.Role == "Captain   ")
                    {
                        Console.WriteLine($"\t\t{p1.Name} get 1 Score(Captain Privilege)");
                        p1.IncreaseScore(game.Bank.GetScore(1));
                        Privilege = false;
                    }
                    if (executionStrategy.IsStealth)
                        p1.UseStealthShip();
                }
            } while (playerStrategies.Where(x => x == 0).ToList().Count == game.PlayerNum);
            game.Bank.CheckCargoShip();

        }
        private TransportStrategy CheckStrategy(Ship ship, CargoAbstract good)
        {
            if (ship.Cargo != good.Name && ship.Cargo != null)//貨物跟船上的貨物不同，無法運上船
                return null;
            return new TransportStrategy(ship, good);
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
