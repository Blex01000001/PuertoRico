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
    /// 大蔗糖廠
    /// </summary>
    public class SugarMill_Large : BuildingAbstract
    {
        public SugarMill_Large()
        {
            Level = 2;
            Scale = "Large";
            Name = "SugarMill_Large";
            Industry = "Sugar";
            Type = "Building";
            Score = 2;
            Cost = 4;
            MaxWorker = 3;
        }
    }
}
