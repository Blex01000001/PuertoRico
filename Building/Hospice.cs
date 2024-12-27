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
    /// 民宿，民宿作用時，當開拓者出現時，玩家可以在選擇正面朝上的農田方塊（或者採礦場--不論是開拓者本人或者有建築舍）的同時，從銀行獲得一個移民直接作用于該方塊上。倘若銀行沒有移民了，他可以直接從移民船上拿。倘若移民船上也沒了，則他就無法得到移民。
    /// </summary>
    public class Hospice : BuildingAbstract
    {
        public Hospice()
        {
            Level = 2;
            Name = "Hospice";
            Industry = "";
            Type = "Building";
            Score = 2;
            Cost = 4;
            MaxWorker = 1;
        }
    }
}
