using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico.Roles
{
    public class Mayor : RoleAbstract
    {
        public override string Name => "Mayor     ";

        public override void Action(Player player, PuertoRico game)
        {
            // 實現 Mayor 的具體行動邏輯
            Console.WriteLine($"\t{Name} Action");
        }
    }
}
