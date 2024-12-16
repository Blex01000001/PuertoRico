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
            //manhour = 2+1+1+2+2+1+1+2+3.5+3+2+6+0.5+2+2.5+9.5+7+0.5;
            List<PuertoRico> puertoRico = new List<PuertoRico>();
            for (int i = 0; i < 1; i++)
            {
                PuertoRico game = new PuertoRico(5);
                puertoRico.Add(game);
            }
            Console.WriteLine("A\tB\tC\tD\tE\ttotal");
            foreach (PuertoRico game in puertoRico)
            {
                int total = 0;
                foreach (int score in game.PlayerList.Select(x => x.Score))
                {
                    total += score;
                    Console.Write($"{score}\t");
                }
                Console.Write($"{total}\t");
                Console.Write("\n");
            }

            Console.ReadLine();
        }
    }
}
