using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    public class Craftsman : RoleAbstract//工匠
    {
        //工匠因特權多的那一個資源，是要等所有人收成完再從銀行拿?還是在工匠收成時就可以直接拿? 等所有人收成完再從銀行拿
        public override string Name => "Craftsman ";
        public override void Action(Player player, PuertoRico game)
        {
            game._writer.WriteLine($"\t{Name} Action");
            foreach (Player p1 in game.GetPlayerListFromRole(player))
            {
                List<CargoAbstract> harvestCargo = new List<CargoAbstract>();
                foreach (CargoAbstract good in game.Bank.Cargos)
                {
                    int checkedHarvest = CheckedHarvest(p1, good);
                    if (checkedHarvest <= 0)//小於0代表無法收成
                        continue;
                    int getGoodQTY = game.Bank.GetCargo(good.GetType(), checkedHarvest);
                    p1.AddCargo(good.Name, getGoodQTY);
                    harvestCargo.Add(good);
                    game._writer.WriteLine($"\t\t{p1.Name} get {getGoodQTY} {good.Name} from bank");
                }
                if (p1.Role == "Craftsman " && harvestCargo.Count != 0)
                    CraftsmanPrivilege(p1, harvestCargo, game);
                if (Utilities.CheckBuildingWithWorker(p1, typeof(Factory)))//檢查玩家是否有大工廠
                {
                    int getMoney = 0;
                    switch (harvestCargo.Count)
                    {
                        case 2:
                            getMoney = game.Bank.GetMoney(1);
                            break;
                        case 3:
                            getMoney = game.Bank.GetMoney(2);
                            break;
                        case 4:
                            getMoney = game.Bank.GetMoney(3);
                            break;
                        case 5:
                            getMoney = game.Bank.GetMoney(5);
                            break;
                    }
                    p1.AddMoney(getMoney);
                    game._writer.WriteLine($"{p1.Name} get {getMoney} from bank (Factory)");
                }
            }
            game._writer.Flush();
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
        //工匠只要有收成，在他獲得收成的同時，他可以選擇任何一個他的收成得以加一（特權）

        private void CraftsmanPrivilege(Player p2, List<CargoAbstract> harvestCargo, PuertoRico game)
        {
            harvestCargo = harvestCargo.OrderBy(x => Utilities.RndNum()).ToList();
            foreach (CargoAbstract cargo in harvestCargo)
            {
                if (game.Bank.TryGetCargo(cargo.GetType(), 1) > 0)
                {
                    p2.AddCargo(cargo.Name, 1);
                    game._writer.WriteLine($"\t\t{p2.Name} get 1 {cargo.Name} from bank(Craftsman Privilege)");
                    break;
                }
            }
        }
    }
}
