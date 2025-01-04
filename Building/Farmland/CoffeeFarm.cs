using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    public class CoffeeFarm : BuildingAbstract
    {
        public CoffeeFarm()
        {
            Name = "CoffeeFarm";
            Industry = "Coffee";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 10;
        }
    }
}
