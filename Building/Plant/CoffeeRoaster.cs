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
    /// 咖啡廠，只有大型
    /// </summary>
    public class CoffeeRoaster : BuildingAbstract
    {
        public CoffeeRoaster()
        {
            Level = 3;
            Scale = "Large";
            Name = "CoffeeRoaster";
            Industry = "Coffee";
            Type = "Building";
            Score = 3;
            Cost = 6;
            MaxWorker = 3;
        }
    }
}
