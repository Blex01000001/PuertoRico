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
    /// 堡壘，若堡壘作用，最後計算點數時，統計遊戲盤上所有移民總數，無條件舍去每三移民多計一分。
    /// </summary>
    public class Fortress : BuildingAbstract
    {
        public Fortress()//堡壘
        {
            Level = 4;
            Name = "Fortress";
            Industry = "";
            Type = "Building";
            Score = 4;
            Cost = 10;
            MaxWorker = 1;
        }
    }
}
