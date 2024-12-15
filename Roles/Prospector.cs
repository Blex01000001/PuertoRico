using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico.Roles
{
    public class Prospector : RoleAbstract
    {
        public override string Name => "Prospector";

        public override void Action(Player player, PuertoRico game)
        {
            // 實現 Prospector 的具體行動邏輯
            Console.WriteLine($"\t{Name} Action");
        }
    }
}
