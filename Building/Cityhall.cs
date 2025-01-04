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
    /// 市政廳，若市政廳作用，最後計算點數時，每座紫色的特殊功能建築（不論大小）多計一分。
    /// </summary>
    public class Cityhall : BuildingAbstract
    {
        public Cityhall()//市政廳
        {
            Level = 4;
            Name = "Cityhall";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
            Occupy = 2;
        }
    }
}
