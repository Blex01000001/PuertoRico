using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    public abstract class RoleAbstract
    {
        public abstract string Name { get; }
        public int Money { get; protected set; }
        public abstract void Action(Player player, PuertoRico game);
        public void ResetMoney()
        {
            Money = 0;
        }
        public void AddMoney(int money)
        {
            Money += money;
        }
        protected RoleAbstract()
        {
            Money = 0;
        }
    }
}
