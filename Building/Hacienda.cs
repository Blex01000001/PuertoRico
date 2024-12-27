using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PuertoRicoSpace
{    /// <summary>
     /// 農莊，農莊作用時，當開拓者出現時，玩家可以（也可以不要）從背面朝上的農田方塊中抽取一張，放在自己的郊區空格上。然後才從正面朝上的農天方塊中選擇方塊（當自己是開拓者的時候，可以選擇採礦場）。
     /// </summary>
    public class Hacienda : BuildingAbstract
    {
        public Hacienda()
        {
            Level = 1;
            Name = "Hacienda";
            Industry = "";
            Type = "Building";
            Score = 1;
            Cost = 2;
            MaxWorker = 1;
        }
    }
}
