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
    /// 大學，大學作用時，當建築師出現時，玩家可以在興建建築的同時，從銀行獲得一個移民直接作用于該建築上。倘若銀行沒有移民了，他可以直接從移民船上拿。倘若移民船上也沒了，則他就無法得到移民。不論建築物容納移民的數量為何，他都只能獲得最多一個移民。
    /// </summary>
    public class University : BuildingAbstract
    {
        public University()
        {
            Level = 3;
            Name = "University";
            Industry = "";
            Type = "Building";
            Score = 3;
            Cost = 8;
            MaxWorker = 1;
        }
    }
}
