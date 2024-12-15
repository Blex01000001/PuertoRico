using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico.Roles
{    //建築師可以不建嗎?
    //其他玩家可以不建嗎?
    public class Builder: RoleAbstract//建築師
    {
        public override string Name => "Builder   ";
        
        public Builder() { }
        public override void Action(Player player, PuertoRico game)
        {
            Console.WriteLine($"\t{Name} Action");
            foreach (Player p1 in game.GetPlayerListFromRole(player))//由建築師本身開始，可以依照上下家順序興建一座建築物。建築師可以在興建任何建築物的時候少花一元（最少可以免費；特權）。
            {
                for (int j = 0; j < game.Bank.AvailableBuildings.Count; j++)//逐一檢查每個建築
                {
                    if (game.Bank.AvailableBuildings[j].Name == "PassBuilding")//選到PassBuilding代表該玩家pass掉買建築物
                    {
                        Console.WriteLine($"\t\t{p1.Name} PASS buy the building");
                        break;
                    }
                    int containBuildingIndex = p1.GetAllBuildings().FindIndex(x => x.Name == game.Bank.AvailableBuildings[j].Name);
                    if (containBuildingIndex >= 0)//檢查玩家有沒有重複的建築
                        continue;
                    int buildingCost = CheckDiscount(p1, game.Bank.AvailableBuildings[j]);
                    if (buildingCost < 0) //買不起就換下一個建築
                        continue;
                    //買得起就直接買
                    Console.WriteLine($"\t\t{p1.Name} cost {buildingCost} buy the {game.Bank.AvailableBuildings[j].Name}");
                    p1.BuildingList.Add(game.Bank.AvailableBuildings[j]);
                    game.Bank.AvailableBuildings.Remove(game.Bank.AvailableBuildings[j]);
                    //扣錢還給銀行
                    p1.DecreaseMoney(buildingCost);
                    game.Bank.AddMoney(buildingCost);
                    break;
                }
                game.Bank.RndAvailableBuildings();
            }
        }
        private int CheckDiscount(Player player, BuildingAbstract building)
        {
            //判斷玩家是否可以買這個建築物(包含打折)，買得起就返回打折後的價格，買不起就返回-1
            if (player.FarmList.Count == 0)
                return -1;
            int playerQuarryWithWorker = player.FarmList.Where(x => x.Name == "Quarry  ").Where(x => x.Worker > 0).ToList().Count;
            int playerOreDiscount = 0;
            int playerIsBuilderDiscount = 0;
            for (int i = 1; i < building.Level; i++)//檢查玩家的礦廠可以折幾元
            {
                if (playerQuarryWithWorker == i)
                    playerOreDiscount = i;
                break;
            }
            if (player.Role == "Builder")//如果玩家是建築師可以再折扣一元
                playerIsBuilderDiscount = 1;

            int buildingCostAfterDis = (building.Cost - playerOreDiscount - playerIsBuilderDiscount) < 0 ? 0 : building.Cost - playerOreDiscount - playerIsBuilderDiscount;

            if (player.Money >= buildingCostAfterDis)
                return buildingCostAfterDis;
            return -1;
        }
    }
}
