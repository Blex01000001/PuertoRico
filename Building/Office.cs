using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    /// <summary>
    /// 辦公室，辦公室作用時，當商人出現時，玩家可以不受商店不收不同種類商品的限制，販賣商店內已經有的商品給商店。但玩家仍然只能每次賣一個商品。
    /// </summary>
    public class Office : BuildingAbstract
    {
        public Office()
        {
            Level = 2;
            Name = "Office";
            Industry = "";
            Type = "Building";
            Score = 2;
            Cost = 5;
            MaxWorker = 1;
        }
    }
}
