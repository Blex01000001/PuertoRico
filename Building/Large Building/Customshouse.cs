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
    /// 海關，若海關作用，最後計算點數時，統計（運物資上船得到的）得分方塊總分，無條件舍去每四分多計一分。
    /// </summary>
    public class Customshouse : BuildingAbstract
    {
        public Customshouse()//海關
        {
            Level = 4;
            Name = "Customshouse";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
            Occupy = 2;
        }
    }
}
