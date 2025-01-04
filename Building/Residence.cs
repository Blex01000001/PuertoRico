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
    /// 府邸，若居民區作用，最後計算點數時，若郊區空格占滿九格以下，多計四分；占滿十格，多計五分；十一格六分；十二格都占滿多計七分。
    /// </summary>
    public class Residence : BuildingAbstract
    {
        public Residence()
        {
            Level = 4;
            Name = "Residence";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
            Occupy = 2;
        }
    }
}
