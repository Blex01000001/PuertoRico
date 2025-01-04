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
    /// 小染料廠
    /// </summary>
    public class IndigoPlant_Small : BuildingAbstract
    {
        public IndigoPlant_Small()
        {
            Level = 1;
            Scale = "Small";
            Name = "IndigoPlant_Small";
            Industry = "Indigo";
            Type = "Building";
            Score = 1;
            Cost = 1;
            MaxWorker = 1;
        }
    }
}
