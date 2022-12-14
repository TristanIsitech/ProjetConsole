using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxEcheque
{
    internal class Pions
    {
        //Utilisation des getteur / setteur, a verifier
        /*private string namePions = string.Empty;
        private string color;
        private int positionX;
        private int positionY;
        public Pions(string snamePions, string scolor, int spositionX, int spositionY)
        {
            namePions = snamePions;
            color = scolor;
            positionX = spositionX;
            positionY = spositionY;
        }

        public string NamePions { get; set; }
        public string Color { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        */

        public string NamePions;
        public string Color;
        public int PositionX;
        public int PositionY;
        public Pions(string snamePions, string scolor, int spositionX, int spositionY)
        {
            NamePions = snamePions;
            Color = scolor;
            PositionX = spositionX;
            PositionY = spositionY;
        }
    }
}

