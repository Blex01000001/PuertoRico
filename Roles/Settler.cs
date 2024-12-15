using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico.Roles
{    //選擇開拓者的玩家可以不拿郊區方塊嗎?
    //其他玩家可以不拿郊區方塊嗎?

    public class Settler : RoleAbstract //開拓者
    {
        public override string Name => "Settler   ";

        public override void Action(Player player, PuertoRico game)
        {
            //開拓者選擇一個打開的郊區方塊，或者拿一個採礦場（特權），放在自己的郊區空格上；接著，下家可以從剩下打開的郊區方塊中選擇一個，放在自己的郊區空格上；依此類推。
            Console.WriteLine($"\t{Name} Action");
            foreach (Player p1 in game.GetPlayerListFromRole(player)) //從開拓者開始選農田，接著下家，依此類推
            {
                bool ConstructionhutRules = Utilities.CheckBuildingWithWorker(p1, typeof(Constructionhut));
                if (game.Bank.AvailableFarms.Count <= 0)
                {
                    Console.WriteLine("***No available Farms***");
                    continue;
                }
                else if (p1.FarmList.Count >= 12)
                {
                    Console.WriteLine($"***{p1.Name} field is full***");
                    continue;
                }
                //開拓者可以選擇一個打開的郊區方塊，或者拿一個採礦場（特權）
                else if (p1.Role == "Settler" && game.Bank.QuarryFields.Count > 0 && player.FarmList.Count < 12)
                {
                    GetQuarry(p1, game);
                    continue;
                }
                //建築舍作用時，當開拓者出現時，玩家猶如開拓者一般地可以選擇拿採礦場（當然要採礦場還有剩的時候）或者普通的農田空格。
                else if (ConstructionhutRules && game.Bank.QuarryFields.Count > 0 && player.FarmList.Count < 12)
                {
                    GetQuarry(p1, game);
                    continue;
                }
                GetField(p1, game);
                game.ArrangeFarms();
            }

        }
        private void GetQuarry(Player p1, PuertoRico game)
        {
            Console.WriteLine($"\t{p1.Name} select the {game.Bank.QuarryFields[0].Name}({game.Bank.QuarryFields[0].GetHashCode()}");
            p1.FarmList.Add(game.Bank.QuarryFields[0]);
            game.Bank.QuarryFields.Remove(game.Bank.QuarryFields[0]);
        }
        private void GetField(Player p1, PuertoRico game)
        {
            Console.WriteLine($"\t{p1.Name} select the {game.Bank.AvailableFarms[0].Name}({game.Bank.AvailableFarms[0].GetHashCode()})");
            p1.FarmList.Add(game.Bank.AvailableFarms[0]);
            game.Bank.AvailableFarms.Remove(game.Bank.AvailableFarms[0]);
        }

    }
}
