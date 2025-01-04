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
    /// 建築舍，建築舍作用時，當開拓者出現時，玩家猶如開拓者一般地可以選擇拿採礦場（當然要採礦場還有剩的時候）或者普通的農田空格。
    /// </summary>
    public class Constructionhut : BuildingAbstract
    {
        public Constructionhut()
        {
            Level = 1;
            Name = "Constructionhut";
            Industry = "";
            Type = "Building";
            Score = 1;
            Cost = 2;
            MaxWorker = 1;
        }
    }
}
