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
    /// 雪茄廠，只有大型
    /// </summary>
    public class TobaccoStorage : BuildingAbstract
    {
        public TobaccoStorage()
        {
            Level = 3;
            Scale = "Large";
            Name = "TobaccoStorage";
            Industry = "Tobacco";
            Type = "Building";
            Score = 3;
            Cost = 5;
            MaxWorker = 3;
        }
    }
}
