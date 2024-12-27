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
    /// 小市場，小市場作用時，只要有賣商品進商店，就可多得一元。可以和大市場一起作用（一起作用時多得三元）。
    /// </summary>
    public class Smallmarket : BuildingAbstract
    {
        public Smallmarket()
        {
            Level = 1;
            Name = "Smallmarket";
            Industry = "";
            Type = "Building";
            Score = 1;
            Cost = 1;
            MaxWorker = 1;
        }
    }
}
