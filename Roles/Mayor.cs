using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{//移民不夠補充移民船時，是此次腳色輪轉後結束還是下一回合才結束?
    public class Mayor : RoleAbstract//市長
    {
        public override string Name => "Mayor     ";

        public override void Action(Player player, PuertoRico game)
        {
            Console.WriteLine($"\t{Name} Action");
            Console.WriteLine($"\t{game.Bank.WorkerShip} Worker on WorkerShip");
            Console.WriteLine($"\t{player.Name} get 1 worker from bank(Mayor)");
            player.IncreaseWorker(game.Bank.GetWorkerFromBank(1));//市長特權
            while (game.Bank.WorkerShip > 0)
            {
                foreach (Player p1 in game.GetPlayerListFromRole(player))//從市長開始每個人輪流拿工人，拿到移民船上沒有工人
                {
                    if (game.Bank.WorkerShip <= 0)
                        break;
                    p1.IncreaseWorker(game.Bank.GetWorkerFromWorkerShip(1));
                    Console.WriteLine($"\t{p1.Name} get 1 worker from WorkerShip, WorkerShip remaining: {game.Bank.WorkerShip}");
                }
            }
            int totalBuildingEmptyCircle = 0;
            foreach (Player p2 in game.GetPlayerListFromRole(player))//所有人必須將得到的移民放在地圖上任何有圈圈的地方（包括農田方塊或者建築物上），而之前部署的任何移民，可以在此時重新部署（但仍然只要有空圈圈且有空間的移民就要部署）
            {
                List<BuildingAbstract> EmptyCircleList = p2.GetEmptyCircleList();
                p2.ClearFarmWorker();
                p2.ClearFactoryWorker();
                for (int i = 0; i < p2.Worker; i++)
                {
                    if (EmptyCircleList.Count <= 0)
                        break;
                    EmptyCircleList[0].IncreaseWorker(1);
                    Console.WriteLine($"\t\t{p2.Name} put 1 worker on {EmptyCircleList[0].Name}({EmptyCircleList[0].GetHexHash()})");
                    EmptyCircleList.RemoveAt(0);
                }
                totalBuildingEmptyCircle += EmptyCircleList.Where(x => x.Type == "Building").ToList().Count;
            }
            Console.WriteLine($"\tTotal Player Empty Circle(Building): {totalBuildingEmptyCircle}");

            game.Bank.ResetWorkerShip(totalBuildingEmptyCircle > game.PlayerNum ? totalBuildingEmptyCircle : game.PlayerNum);
            game.Bank.GetWorkerFromBank(game.Bank.WorkerShip);
            if (game.Bank.Worker <= 0)//移民不夠補充移民船時，則遊戲結束事件發生
            {
                Console.WriteLine("\n>>>>移民不夠補充移民船，遊戲將在角色輪轉後結束<<<<\n");
                game.CallGame();
            }
        }
    }

}
