using PuertoRicoSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace PuertoRico
{
    internal class data
    {
        public int PlayerNum { get;  set; }
        public TimeSpan ElapsedTime { get;  set; }//遊戲經過時間
        public int Round { get;  set; }
        public int TotalScore { get;  set; }
        public List<Player> PlayerList { get; set; }
        public Bank Bank { get; set; }
    }
}
