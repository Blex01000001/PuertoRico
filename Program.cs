using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuertoRicoSpace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5+1+2+2+1+1.5+2.5;
            List<PuertoRico> puertoRico = new List<PuertoRico>();
            do
            {
                PuertoRico game = new PuertoRico(5);
                if(game.TotalScore > 10)
                {
                    puertoRico.Add(game);
                }
            } while (puertoRico.Count < 10);

            Console.WriteLine("A\tB\tC\tD\tE\tRound\ttotal\ttime");
            foreach (PuertoRico game in puertoRico)
            {
                int total = 0;
                foreach (int score in game.PlayerList.Select(x => x.Score))
                {
                    total += score;
                    Console.Write($"{score}\t");
                }
                Console.Write($"{game.Round}\t{game.TotalScore}\t{game.ElapsedTime.ToString("s\\.fffff")}");
                Console.Write("\n");
            }

            Console.ReadLine();
        }
    }
}
