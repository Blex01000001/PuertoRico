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
    /// 商會，若商會作用，最後計算點數時，大型生產廠房多計兩分，小型生產廠房多計一分。
    /// </summary>
    public class Guildhall : BuildingAbstract
    {
        public Guildhall()
        {
            Level = 4;
            Name = "Guildhall";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
            Occupy = 2;
        }
    }
}
