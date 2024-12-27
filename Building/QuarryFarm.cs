using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    public class QuarryFarm : BuildingAbstract
    {
        public QuarryFarm()
        {
            Name = "QuarryFarm";
            Industry = "Quarry";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 500;
        }
    }
}
