using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    public class Prospector : RoleAbstract//礦工
    {
        public override string Name => "Prospector";

        public override void Action(Player player, PuertoRico game)
        {
            game._writer.WriteLine($"\t{Name} Action");
            player.AddMoney(game.Bank.GetMoney(1));
            game._writer.Flush();
        }
    }
}
