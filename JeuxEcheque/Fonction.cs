using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JeuxEcheque
{
    internal class Fonction
    {
        private string[] PlaceBlanc = new string[16];
        private string[] PlaceNoir = new string[16];

        public Fonction()
        {
            PlaceBlanc = MiseEnPlace("Blanc");
            PlaceNoir = MiseEnPlace("Noir");
        }

        private string[] MiseEnPlace(string couleur)
        {
            if(couleur == "Blanc")
            {
                string[] placement = { "E1", "D1", "C1", "F1", "B1", "G1", "A1", "H1", "A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2" };
                return placement;
            }
            else
            {
                string[] placement = { "E8", "D8", "C8", "F8", "B8", "G8", "A8", "H8", "A7", "B7", "C7", "D7", "E7", "F7", "G7", "H7" };
                return placement;
            }
        }

        public void AfficherEchequier()
        {
            Console.WriteLine("   A  B  C  D  E  F  G  H");
            for (int i = 1; i <= 8; i++)
            {
                ligne(i);
            }
            Console.WriteLine("   A  B  C  D  E  F  G  H");

            void ligne(int n)
            {
                Console.Write(n.ToString()+" ");
                for (int i = 0; i < 8; i++)
                {
                    if (n % 2 ==1)
                    {
                        if(i%2 == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write("   ");
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write("   ");
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }
                }
                Console.WriteLine(" "+n.ToString());
            }
        }
    }
}
