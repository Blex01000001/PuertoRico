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
    /// 小蔗糖廠
    /// </summary>
    public class SugarMill_Small : BuildingAbstract
    {
        public SugarMill_Small()
        {
            Level = 1;
            Scale = "Small";
            Name = "SugarMill_Small";
            Industry = "Sugar";
            Type = "Building";
            Score = 1;
            Cost = 2;
            MaxWorker = 1;
        }
    }
}
