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
    /// 港口，港口作用時，當船長出現時，每次輪到他運物資上船，只要他有物資可以運上船，他就可以多獲得一分。在同一次船長出現時，港口的作用可以進行很多次。可以和碼頭一起作用。
    /// </summary>
    public class Harbor : BuildingAbstract
    {
        public Harbor()
        {
            Level = 3;
            Name = "Harbor";
            Industry = "";
            Type = "Building";
            Score = 3;
            Cost = 8;
            MaxWorker = 1;
        }
    }
}
