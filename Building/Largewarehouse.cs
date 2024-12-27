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
    /// 大倉庫，大倉庫作用時，當船長出現時，最後除了每個人有一份商品不會腐敗之外，玩家可以選擇二種商品（是二種不是二份），他所持有的該二種商品全部不會腐敗。可以和小倉庫一起作用（一起作用時可以選三種）。
    /// </summary>
    public class Largewarehouse : BuildingAbstract
    {
        public Largewarehouse()
        {
            Level = 2;
            Name = "Largewarehouse";
            Industry = "";
            Type = "Building";
            Score = 2;
            Cost = 6;
            MaxWorker = 1;
        }
    }
}
