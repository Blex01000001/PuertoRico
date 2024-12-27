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
    /// 大市場，大市場作用時，只要有賣商品進商店，就可多得二元。可以和小市場一起作用（一起作用時多得三元）。
    /// </summary>
    public class Largemarket : BuildingAbstract
    {
        public Largemarket()
        {
            Level = 2;
            Name = "Largemarket";
            Industry = "";
            Type = "Building";
            Score = 2;
            Cost = 5;
            MaxWorker = 1;
        }
    }
}
