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
    /// 大染料廠
    /// </summary>
    public class IndigoPlant_Large : BuildingAbstract
    {
        public IndigoPlant_Large()
        {
            Level = 2;
            Scale = "Large";
            Name = "IndigoPlant_Large";
            Industry = "Indigo";
            Type = "Building";
            Score = 2;
            Cost = 3;
            MaxWorker = 3;
        }
    }
}
