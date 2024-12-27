using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{
    public class SugarFarm : BuildingAbstract
    {
        public SugarFarm()
        {
            Name = "SugarFarm";
            Industry = "Sugar";
            Type = "Farm";
            MaxWorker = 1;
            Priority = 10;
        }
    }
}
