using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{    //建築師可以不建嗎? 可以
    //其他玩家可以不建嗎? 可以
    public class Builder: RoleAbstract//建築師
    {
        public override string Name => "Builder   ";
        
        public Builder() { }
        public override void Action(Player player, PuertoRico game)
        {
            game._writer.WriteLine($"\t{Name} Action");
            AdjustmentPriority(game);
            List<BuildingAbstract> priorityBuildings = Utilities.RandomOrderByPriority(game.Bank.AvailableBuildings);

            foreach (Player p1 in game.GetPlayerListFromRole(player))//由建築師本身開始，可以依照上下家順序興建一座建築物。建築師可以在興建任何建築物的時候少花一元（最少可以免費；特權）。
            {
                priorityBuildings = Utilities.RandomOrderByPriority(priorityBuildings);
                for (int j = 0; j < priorityBuildings.Count; j++)//逐一檢查每個建築
                {
                    BuildingAbstract tempBuilding = priorityBuildings[j];
                    if (tempBuilding.Name == "PassBuilding")//選到PassBuilding代表該玩家pass掉買建築物
                    {
                        game._writer.WriteLine($"\t\t{p1.Name} PASS buy the building");
                        break;
                    }
                    if(tempBuilding.Occupy + p1.BuildingList.Sum(x => x.Occupy) > 12)//玩家沒空間放置該建築物
                    {
                        continue;
                    }
                    int containBuildingIndex = p1.GetAllBuildings().FindIndex(x => x.Name == tempBuilding.Name);
                    if (containBuildingIndex >= 0)//檢查玩家有沒有重複的建築
                        continue;
                    int buildingCost = CheckDiscount(p1, tempBuilding);
                    if (buildingCost < 0) //買不起就換下一個建築
                        continue;
                    //買得起就直接買
                    game._writer.WriteLine($"\t\t{p1.Name} cost {buildingCost} buy the {tempBuilding.Name}");
                    p1.BuildingList.Add(tempBuilding);
                    priorityBuildings.Remove(tempBuilding);
                    game.Bank.AvailableBuildings.Remove(tempBuilding);
                    //扣錢還給銀行
                    p1.DecreaseMoney(buildingCost);
                    game.Bank.AddMoney(buildingCost);

                    bool UniversityRule = Utilities.CheckBuildingWithWorker(p1, typeof(Hospice));//檢查玩家是否有大學
                    //大學作用時，當建築師出現時，玩家可以在興建建築的同時，從銀行獲得一個移民直接作用于該建築上。倘若銀行沒有移民了，他可以直接從移民船上拿。倘若移民船上也沒了，則他就無法得到移民。不論建築物容納移民的數量為何，他都只能獲得最多一個移民。
                    if (UniversityRule && game.Bank.TryGetWorkerFromBank(1) > 0)
                    {
                        int worker = game.Bank.GetWorkerFromBank(1);
                        tempBuilding.IncreaseWorker(worker);
                        p1.AddWorker(worker);
                        game._writer.WriteLine($"\t\t{p1.Name} get {worker} worker from bank and put it on the {tempBuilding.Name}({tempBuilding.GetHexHash()}) (University)");
                    }
                    else if (UniversityRule && game.Bank.TryGetWorkerFromWorkerShip(1) > 0)
                    {
                        int worker = game.Bank.GetWorkerFromWorkerShip(1);
                        tempBuilding.IncreaseWorker(worker);
                        p1.AddWorker(worker);
                        game._writer.WriteLine($"\t\t{p1.Name} get {worker} worker from workership and put it on the {tempBuilding.Name}({tempBuilding.GetHexHash()}) (University)");
                    }
                    break;
                }
                if(p1.BuildingList.Sum(x => x.Occupy) >= 12)//在建築師出現時，至少有一個人將12個建築空格蓋滿則發生遊戲結束事件
                {
                    game._writer.WriteLine($"\n>>>>{p1.Name}建築空格已滿，遊戲將在輪轉後結束<<<<\n");
                    game.CallGame();
                }
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
        private void AdjustmentPriority(PuertoRico game)
        {
            foreach (var item in game.Bank.AvailableBuildings.FindAll(x => x.Name == "Quarry  "))
            {
                item.SetPriority(500);
            }
            foreach (var item in game.Bank.AvailableBuildings.FindAll(x => x.Name == "Wharf"))
            {
                item.SetPriority(1000);
            }
            if(game.Bank.Worker < 10)
            {
                game.Bank.Buildings.Find(x => x.Name == "Guildhall").SetPriority(1000);
                game.Bank.Buildings.Find(x => x.Name == "Residence").SetPriority(1000);
                game.Bank.Buildings.Find(x => x.Name == "Fortress").SetPriority(1000);
                game.Bank.Buildings.Find(x => x.Name == "Customshouse").SetPriority(1000);
                game.Bank.Buildings.Find(x => x.Name == "Cityhall").SetPriority(1000);
            }
        }
    }
}
