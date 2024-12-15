using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico.Roles
{
    public class Prospector : RoleAbstract//礦工
    {
        public override string Name => "Prospector";

        public override void Action(Player player, PuertoRico game)
        {
            Console.WriteLine($"\t{Name} Action");
            player.IncreaseMoney(game.Bank.GetMoney(1));
        }
    }
}
