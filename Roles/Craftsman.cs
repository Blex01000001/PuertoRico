using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico.Roles
{
    public class Craftsman : RoleAbstract//工匠
    {
        public override string Name => "Craftsman ";

        public override void Action(Player player, PuertoRico game)
        {
            Console.WriteLine($"\t{Name} Action");
            foreach (Player player1 in game.GetPlayerListFromRole(player))
            {
                foreach (CargoAbstract good in game.Bank.Cargos)
                {
                    int checkedHarvest = CheckedHarvest(player1, good);
                    if (checkedHarvest <= 0)//小於0代表無法收成
                        continue;
                    int getGoodQTY = game.Bank.GetCargo(good.GetType(), checkedHarvest);
                    player1.IncreaseCargo(good.Name, getGoodQTY);
                    Console.WriteLine($"\t\t{player1.Name} get {getGoodQTY} {good.Name} from bank");
                }
            }
        }
        private int CheckedHarvest(Player player, CargoAbstract good)//小於0代表無法收成
        {
            if (good.GetType() == typeof(Corn))
            {
                return player.GetFarmWorker("Corn");
            }else{
                int FarmWorker = player.GetFarmWorker(good.GetType().Name);
                if (FarmWorker <= 0)
                    return -1;
                int BuildingWorker = player.GetBuildingWorker(good.GetType().Name);
                return Math.Min(BuildingWorker, FarmWorker);
            }
        }

    }
}
