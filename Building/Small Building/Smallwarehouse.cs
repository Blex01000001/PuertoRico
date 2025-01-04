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
    /// 小倉庫，小倉庫作用時，當船長出現時，最後除了每個人有一份商品不會腐敗之外，玩家可以選擇一種商品（是一種不是一份），他所持有的該種商品全部不會腐敗。可以和大倉庫一起作用（一起作用時可以選三種商品存儲）。
    /// </summary>
    public class Smallwarehouse : BuildingAbstract
    {
        public Smallwarehouse()
        {
            Level = 1;
            Name = "Smallwarehouse";
            Industry = "";
            Type = "Building";
            Score = 1;
            Cost = 3;
            MaxWorker = 1;
        }
    }
}
