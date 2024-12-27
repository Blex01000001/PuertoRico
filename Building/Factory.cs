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
    /// 大工廠，大工廠作用時，當工匠出現時，最後統計收成時，倘若玩家收成的種類有一種，可從銀行得0元；有兩種，可從銀行得1元；三種得2元；四種得3元；五種都有得5元。
    /// </summary>
    public class Factory : BuildingAbstract
    {
        public Factory()
        {
            Level = 3;
            Name = "Factory";
            Industry = "";
            Type = "Building";
            Score = 3;
            Cost = 7;
            MaxWorker = 1;
        }
    }
}
