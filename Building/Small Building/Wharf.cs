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
    /// 碼頭，碼頭作用時，當船長出現時，輪到該玩家運物資上船時，他可以選擇將物資運到一艘私人的，虛擬的，沒有容量上限的船上，且該虛擬的船運輸的種類可以與船長的三艘船上的種類重複，但該虛擬的船在同一個船長出現的時候仍然只能運送一種物資。該虛擬的船最後一定會開船，也就是船上的物資必定會繳回銀行。可以和港口一起作用。
    /// </summary>
    public class Wharf : BuildingAbstract
    {
        public Wharf()
        {
            Level = 3;
            Name = "Wharf";
            Industry = "";
            Type = "Building";
            Score = 3;
            Cost = 9;
            MaxWorker = 1;
        }
    }
}
