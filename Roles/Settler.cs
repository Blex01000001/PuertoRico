using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{   //選擇開拓者的玩家可以不拿郊區方塊嗎?
    //其他玩家可以不拿郊區方塊嗎?

    public class Settler : RoleAbstract //開拓者
    {
        public override string Name => "Settler   ";

        public override void Action(Player player, PuertoRico game)
        {
            //開拓者選擇一個打開的郊區方塊，或者拿一個採礦場（特權），放在自己的郊區空格上；接著，下家可以從剩下打開的郊區方塊中選擇一個，放在自己的郊區空格上；依此類推。
            game._writer.WriteLine($"\t{Name} Action");
            foreach (Player p1 in game.GetPlayerListFromRole(player)) //從開拓者開始選農田，接著下家，依此類推
            {
                BuildingAbstract selectedBuilding = null;
                bool ConstructionhutRules = Utilities.CheckBuildingWithWorker(p1, typeof(Constructionhut));//檢查玩家是否有建築舍
                bool HaciendaRules = Utilities.CheckBuildingWithWorker(p1, typeof(Hacienda));//檢查玩家是否有農莊
                //農莊作用時，當開拓者出現時，玩家可以（也可以不要）從背面朝上的農田方塊中抽取一張，放在自己的郊區空格上。然後才從正面朝上的農天方塊中選擇方塊（當自己是開拓者的時候，可以選擇採礦場）。
                //當背面朝上的農田方塊已經沒了，就不可以抽取。
                //不管農田方塊是不是玩家要的都必須接受
                if (p1.FarmList.Count < 12 && game.Bank.HideFarms.Count > 0 && HaciendaRules)
                {
                    GetHideField(p1, game);
                }

                if (game.Bank.AvailableFarms.Count <= 0)//正面朝上的農天方塊為0
                    {
                    game._writer.WriteLine("***No available Farms***");
                    continue;
                }
                else if (p1.FarmList.Count >= 12)//玩家農田已滿
                {
                    game._writer.WriteLine($"***{p1.Name} field is full***");
                    continue;
                }
                //開拓者可以選擇一個打開的郊區方塊，或者拿一個採礦場（特權）
                else if (p1.Role == "Settler" && game.Bank.QuarryFields.Count > 0 && player.FarmList.Count < 12)
                {
                    selectedBuilding = GetQuarry(p1, game);
                }
                //建築舍作用時，當開拓者出現時，玩家猶如開拓者一般地可以選擇拿採礦場（當然要採礦場還有剩的時候）或者普通的農田空格。
                else if (ConstructionhutRules && game.Bank.QuarryFields.Count > 0 && player.FarmList.Count < 12)
                {
                    selectedBuilding = GetQuarry(p1, game);

                }else{
                    selectedBuilding = GetField(p1, game);
                }

                //民宿作用時，當開拓者出現時，玩家可以在選擇正面朝上的農田方塊（或者採礦場--不論是開拓者本人或者有建築舍）的同時，從銀行獲得一個移民直接作用于該方塊上。倘若銀行沒有移民了，他可以直接從移民船上拿。倘若移民船上也沒了，則他就無法得到移民。
                if (Utilities.CheckBuildingWithWorker(p1, typeof(Hospice)) && game.Bank.TryGetWorkerFromBank(1) > 0)
                {
                    int worker = game.Bank.GetWorkerFromBank(1);
                    selectedBuilding.IncreaseWorker(worker);
                    p1.AddWorker(worker);
                    game._writer.WriteLine($"\t\t{p1.Name} get {worker} worker from bank and put it on the {selectedBuilding.Name}({selectedBuilding.GetHexHash()}) (Hospice)");
                }
                else if (Utilities.CheckBuildingWithWorker(p1, typeof(Hospice)) && game.Bank.TryGetWorkerFromWorkerShip(1) > 0)
                {
                    int worker = game.Bank.GetWorkerFromWorkerShip(1);
                    selectedBuilding.IncreaseWorker(worker);
                    p1.AddWorker(worker);
                    game._writer.WriteLine($"\t\t{p1.Name} get {worker} worker from workership and put it on the {selectedBuilding.Name}({selectedBuilding.GetHexHash()}) (Hospice)");
                }
                game.ArrangeFarms();
            }
            game._writer.Flush();
        }
        private BuildingAbstract GetQuarry(Player p1, PuertoRico game)
        {
            BuildingAbstract quarry = game.Bank.QuarryFields[0];
            p1.FarmList.Add(quarry);
            game.Bank.QuarryFields.Remove(quarry);
            game._writer.WriteLine($"\t{p1.Name} select the {quarry.Name}({quarry.GetHexHash()}");
            return quarry;
        }
        private BuildingAbstract GetField(Player p1, PuertoRico game)
        {
            BuildingAbstract farm = game.Bank.AvailableFarms[0];
            p1.FarmList.Add(farm);
            game.Bank.AvailableFarms.Remove(farm);
            game._writer.WriteLine($"\t{p1.Name} select the {farm.Name}({farm.GetHexHash()})");
            return farm;
        }
        private BuildingAbstract GetHideField(Player p1, PuertoRico game)
        {
            BuildingAbstract farm = game.Bank.HideFarms[0];
            p1.FarmList.Add(farm);
            game.Bank.HideFarms.Remove(farm);
            game._writer.WriteLine($"\t{p1.Name} get the {farm.Name}({farm.GetHexHash()})(Hacienda)");
            return farm;
        }
    }
}
