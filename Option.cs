using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRico
{
    public class Option
    {
        public string DirPath { get; private set; }
        public string JsonPath { get; private set; }
        public string TxtPath { get; private set; }
        public Guid Guid { get; private set; }
        public int ScoreLimit { get; private set; }
        public Option(int scoreLimit = 0)
        {
            ScoreLimit = scoreLimit;
            DirPath = "D:\\Code\\C#\\PuertoRicoData\\";
            Guid = Guid.NewGuid();
            JsonPath = DirPath + Guid + ".json";
            TxtPath = DirPath + Guid + ".txt";

        }
    }
}
