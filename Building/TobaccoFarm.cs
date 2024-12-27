using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    public class TobaccoFarm : BuildingAbstract
    {
        public TobaccoFarm()
        {
            Name = "TobaccoFarm";
            Industry = "Tobacco";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 10;
        }
    }
}
