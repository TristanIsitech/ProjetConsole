using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

enum Pieces {
    R, D, F1, F2, C1, C2, T1, T2, P1, P2, P3, P4, P5, P6, P7, P8
}

namespace JeuxEcheque
{
    internal class Fonction
    {
        // List des pions
        private Pions[] ListPions = new Pions[32];
        // Variable bool de fin du jeu
        public static bool EndGame = false;
        // Variable des couleurs de joueur
        public static string[] color = { "White", "Black" };
        // Variable qui définit quel joueur joue
        public static int player = 0;

        // Constructeur
        public Fonction()
        {
            ListPions = Establishment();
        }

        // Fonctoin de déplacement d'un poin apres selection
        public bool Moove(int oldX, int oldY)
        {
            Pions Pion = FindPion(oldX, oldY);
            // Test si il y a un pion sur cette case
            if(Pion != null)
            {
                // Test si ce pion est bien celui du joueur
                if (Pion.Color == color[player])
                {
                    // List des mouvements possible
                    List<int[]> possibleMoove = PossibleMoove(Pion);
                    // Test si le pion peut bouger
                    if(possibleMoove.Count != 0)
                    {
                        // Variable local
                        bool checksCase = true;
                        bool checksMoovecase = false;
                        int[] selectedMoove = new int[2];
                        string selectPion;

                        // Verification de déplacement du pion
                        do
                        {
                            // Verification de l'entrer de la case
                            do
                            {
                                // Affichage du plateau avec les coups possible 
                                this.ShowChessboard(possibleMoove);
                                Console.WriteLine("Pièce sélectionnée : " + Pion.NamePions.Substring(0, 1));
                                // Affichage des coups possible
                                Console.WriteLine("Liste des coups possible :");

                                foreach (int[] moove in possibleMoove)
                                {
                                    Console.Write(ConvertCase(moove[0]) + "" + moove[1] + " / ");
                                }
                                Console.WriteLine("");

                                // Selection du deplacement du pion
                                Console.WriteLine("Où souhaitez vous déplacer cette pièce ? ");
                                selectPion = Console.ReadLine();
                                checksCase = ChecksCase(selectPion);
                            } while (!checksCase);

                            // Verification de déplacement du pion
                            selectedMoove[0] = ConvertCase(selectPion.Substring(0, 1));
                            selectedMoove[1] = Convert.ToInt32(selectPion.Substring(1, 1));
                            foreach (int[] moove in possibleMoove)
                            {
                                // Verification si le pion peut se déplacer sur cette case
                                if (moove[0] == selectedMoove[0] && moove[1] == selectedMoove[1])
                                {
                                    checksMoovecase = true;
                                    break;
                                }
                            }
                        } while (!checksMoovecase);

                        // Verification si un enemis est sur cette case
                        Pions enemis = FindPion(selectedMoove[0], selectedMoove[1]);
                        if (enemis != null)
                        {
                            // Supprime l'enemis (Case 0, 0)
                            enemis.PositionX = 0;
                            enemis.PositionY = 0;
                            // Verifie que ce n'est pas le Roi
                            if (enemis.NamePions == "R")
                            {
                                // Si c'est le Roi, termine le jeu
                                EndGame = true;
                            }
                        }
                        // Deplace le pion
                        Pion.PositionX = selectedMoove[0];
                        Pion.PositionY = selectedMoove[1];
                        return true;
                        
                    }
                    else
                    {
                        Console.WriteLine("Cette pièce ne peut pas bouger !");
                        Console.ReadKey();
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Cette pièce ne vous appartient pas !");
                    Console.ReadKey();
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Aucune pièce sur cette case !");
                Console.ReadKey();
                return false;
            }
        }

        // Fonction de list des coups possible pour un pion
        private List<int[]> PossibleMoove(Pions Pion)
        {
            List<int[]> possibleMoove = new List<int[]>();
            switch (Pion.NamePions.Substring(0, 1))
            {
                case ("R"):
                    for(sbyte x = -1; x <= 1; x++)
                    {
                        for (sbyte y = -1; y <= 1; y++)
                        {
                            if(x!=0 || y != 0) 
                            {
                                if (!(Pion.PositionX + x >= 9 || Pion.PositionX + x <= 0 || Pion.PositionY + y >= 9 || Pion.PositionY + y <= 0) && (FindPion(Pion.PositionX + x, Pion.PositionY + y) == null))
                                {
                                    Pions possibleEnemy = FindPion(Pion.PositionX + x, Pion.PositionY + y);
                                    if(possibleEnemy == null)
                                    {
                                        possibleMoove.Add(new int[2] { Pion.PositionX + x, Pion.PositionY + y });
                                    }
                                    else if (possibleEnemy.Color != Pion.Color)
                                    {
                                        possibleMoove.Add(new int[2] { Pion.PositionX + x, Pion.PositionY + y });
                                    }
                                }
                            }
                        }
                    }
                    break;

                case ("D"):
                    for (sbyte x = 1; x <= 7; x++) // + X
                    {
                        if (Pion.PositionX + x != 9)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX + x, Pion.PositionY);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + x, Pion.PositionY });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + x, Pion.PositionY });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte x = 1; x <= 7; x++) // - X
                    {
                        if (Pion.PositionX - x != 0)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX - x, Pion.PositionY);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - x, Pion.PositionY });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - x, Pion.PositionY });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte y = 1; y <= 7; y++) // + Y
                    {
                        if (Pion.PositionY + y != 9)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX, Pion.PositionY + y);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY + y });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY + y });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte y = 1; y <= 7; y++) // - Y
                    {
                        if (Pion.PositionY - y != 0)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX, Pion.PositionY - y);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY - y });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY - y });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte i = 1; i <= 7; i++) // + X + Y
                    {
                        if (Pion.PositionX + i != 9 && Pion.PositionY + i != 9)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX + i, Pion.PositionY + i);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + i, Pion.PositionY + i });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + i, Pion.PositionY + i });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte i = 1; i <= 7; i++) // - X + Y
                    {
                        if (Pion.PositionX - i != 0 && Pion.PositionY + i != 9)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX - i, Pion.PositionY + i);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - i, Pion.PositionY + i });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - i, Pion.PositionY + i });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte i = 1; i <= 7; i++) // + X - Y
                    {
                        if (Pion.PositionX + i != 9 && Pion.PositionY - i != 0)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX + i, Pion.PositionY - i);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + i, Pion.PositionY - i });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + i, Pion.PositionY - i });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte i = 1; i <= 7; i++) // - X - Y
                    {
                        if (Pion.PositionX - i != 0 && Pion.PositionY - i != 0)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX - i, Pion.PositionY - i);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - i, Pion.PositionY - i });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - i, Pion.PositionY - i });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;

                case ("T"):
                    for (sbyte x = 1; x <= 7; x++) // + X
                    {
                        if (Pion.PositionX + x != 9)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX + x, Pion.PositionY);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + x, Pion.PositionY });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + x, Pion.PositionY });
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte x = 1; x <= 7; x++) // - X
                    {
                        if (Pion.PositionX - x != 0)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX - x, Pion.PositionY);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - x, Pion.PositionY });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - x, Pion.PositionY });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte y = 1; y <= 7; y++) // + Y
                    {
                        if (Pion.PositionY + y != 9)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX, Pion.PositionY + y);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY + y });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY + y });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte y = 1; y <= 7; y++) // - Y
                    {
                        if (Pion.PositionY - y != 0)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX, Pion.PositionY - y);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY - y });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY - y });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;

                case ("F"):
                    for (sbyte i = 1; i <= 7; i++) // + X + Y
                    {
                        if (Pion.PositionX + i != 9 && Pion.PositionY + i != 9)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX + i, Pion.PositionY + i);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + i, Pion.PositionY + i });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + i, Pion.PositionY + i });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte i = 1; i <= 7; i++) // - X + Y
                    {
                        if (Pion.PositionX - i != 0 && Pion.PositionY + i != 9)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX - i, Pion.PositionY + i);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - i, Pion.PositionY + i });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - i, Pion.PositionY + i });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte i = 1; i <= 7; i++) // + X - Y
                    {
                        if (Pion.PositionX + i != 9 && Pion.PositionY - i != 0)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX + i, Pion.PositionY - i);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + i, Pion.PositionY - i });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + i, Pion.PositionY - i });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (sbyte i = 1; i <= 7; i++) // - X - Y
                    {
                        if (Pion.PositionX - i != 0 && Pion.PositionY - i != 0)
                        {
                            Pions possibleEnemy = FindPion(Pion.PositionX - i, Pion.PositionY - i);
                            if (possibleEnemy == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - i, Pion.PositionY - i });
                            }
                            else if (possibleEnemy.Color != Pion.Color)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - i, Pion.PositionY - i });
                                
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;

                case ("C"):
                    for(short x = -2; x <= 2; x++)
                    {
                        for(short y = -2; y <= 2; y++)
                        {
                            if (!(System.Math.Abs(x) == 2 && System.Math.Abs(y) == 2) && !(System.Math.Abs(x) == 1 && System.Math.Abs(y) == 1) && x != 0 && y != 0)
                            {
                                if(Pion.PositionX + x !> 0 && Pion.PositionY + y !> 0 && Pion.PositionX + x !< 9 && Pion.PositionY + y !< 9)
                                {
                                    Pions possibleEnemy = FindPion(Pion.PositionX + x, Pion.PositionY + y);
                                    if (possibleEnemy == null)
                                    {
                                        possibleMoove.Add(new int[2] { Pion.PositionX + x, Pion.PositionY + y });
                                    }
                                    else if(possibleEnemy.Color != Pion.Color)
                                    {
                                        possibleMoove.Add(new int[2] { Pion.PositionX + x, Pion.PositionY + y });
                                    }
                                }
                            }
                        }
                    }
                    break;

                case ("¤"):
                    if (Pion.Color == "White")
                    {
                        // Si personne devant
                        if(FindPion(Pion.PositionX, Pion.PositionY + 1) == null)
                        {
                            possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY + 1 });

                            // Si jamais bouger (toujours sur la ligne 2)
                            if (Pion.PositionY == 2 && FindPion(Pion.PositionX, Pion.PositionY + 2) == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY + 2 });
                            }
                        }
                        // Si pion enemis dans la digonale bas-droite
                        Pions possibleEnemy = FindPion(Pion.PositionX + 1, Pion.PositionY + 1);
                        if (possibleEnemy != null)
                        {
                            if(possibleEnemy.Color == "Black")
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + 1, Pion.PositionY + 1 });
                            }
                        }
                        // Si pion enemis dans la digonale bas-gauche
                        possibleEnemy = FindPion(Pion.PositionX - 1, Pion.PositionY + 1);
                        if (possibleEnemy != null)
                        {
                            if (possibleEnemy.Color == "Black")
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - 1, Pion.PositionY + 1 });
                            }
                        }
                    }
                    else
                    {
                        // Si personne devant
                        if (FindPion(Pion.PositionX, Pion.PositionY - 1) == null)
                        {
                            possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY - 1 });
                            if (Pion.PositionY == 7 && FindPion(Pion.PositionX, Pion.PositionY - 2) == null)
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX, Pion.PositionY - 2 });
                            }
                        }
                        // Si pion enemis dans la digonale haut-droite
                        Pions possibleEnemy = FindPion(Pion.PositionX + 1, Pion.PositionY - 1);
                        if (possibleEnemy != null)
                        {
                            if (possibleEnemy.Color == "White")
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX + 1, Pion.PositionY - 1 });
                            }
                        }
                        // Si pion enemis dans la digonale haut-gauche
                        possibleEnemy = FindPion(Pion.PositionX - 1, Pion.PositionY - 1);
                        if (possibleEnemy != null)
                        {
                            if (possibleEnemy.Color == "White")
                            {
                                possibleMoove.Add(new int[2] { Pion.PositionX - 1, Pion.PositionY - 1 });
                            }
                        }
                    }
                    break;
            }
            return possibleMoove;
        }

        // Convertion des caractères de case en chiffre et surcharge de sens inverses
        private static string ConvertCase(int x)
        {     
            string s = Encoding.ASCII.GetString(new byte[] {  Convert.ToByte(x + 64) });
            return s;
        }
        public static int ConvertCase(string x)
        {
            int s = Convert.ToInt32(Encoding.ASCII.GetBytes(new char[] { Convert.ToChar(x) })[0]) - 64;
            return s;
        }

        // Convertion des noms des joueurs en couleur de plateau
        public static string ConvertPlayer(string player)
        {
            if(player == "White")
            {
                return "Rouge";
            }
            else
            {
                return "Vert";
            }
        }

        // Mise en place de la list des pions du plateau
        private Pions[] Establishment()
        {
            //Utilisation de enum Pions (pas pratique)
            /*Pions[] placement = new Pions[32];
            string[] couleurs = { "White", "Black" };
            byte iter = 0;
            byte[] ligne = new byte[2];
            foreach (string couleur in couleurs)
            {
                if (couleur == "White") { ligne[0] = 1; ligne[1] = 2; }
                else { ligne[0] = 8; ligne[1] = 7; }

                foreach (Pieces piece in Pieces.GetValues(typeof(Pieces)))
                {
                    switch (piece)
                    {
                        case (Pieces.R):
                            placement[iter] = new Pions(piece, couleur, 5, ligne[0]);
                            break;
                        case (Pieces.D):
                            placement[iter] = new Pions(piece, couleur, 4, ligne[0]);
                            break;
                        case (Pieces.F1):
                    }

                    Console.WriteLine(placement[iter].NamePions + " " + iter);
                    iter++;
                }
            }

            Console.WriteLine(placement[iter].NamePions + " " + iter);*/

            Pions[] placement =
                   {
                       new Pions("R", "White", 5, 1),
                       new Pions("D", "White",4, 1),
                       new Pions("F1", "White",3, 1),
                       new Pions("F2", "White", 6, 1),
                       new Pions("C1", "White", 2, 1),
                       new Pions("C2", "White", 7, 1),
                       new Pions("T1", "White", 1, 1),
                       new Pions("T2", "White", 8, 1),
                       new Pions("¤1", "White",1, 2),
                       new Pions("¤2", "White",2, 2),
                       new Pions("¤3", "White",3, 2),
                       new Pions("¤4", "White",4, 2),
                       new Pions("¤5", "White",5, 2),
                       new Pions("¤6", "White",6, 2),
                       new Pions("¤7", "White",7, 2),
                       new Pions("¤8", "White",8, 2),
                       new Pions("R", "Black", 5, 8),
                       new Pions("D", "Black",4, 8),
                       new Pions("F1", "Black",3, 8),
                       new Pions("F2", "Black",6, 8),
                       new Pions("C1","Black", 2, 8),
                       new Pions("C2", "Black",7, 8),
                       new Pions("T1", "Black",1, 8),
                       new Pions("T2", "Black",8, 8),
                       new Pions("¤1", "Black",1, 7),
                       new Pions("¤2", "Black",2, 7),
                       new Pions("¤3", "Black",3, 7),
                       new Pions("¤4","Black", 4, 7),
                       new Pions("¤5", "Black",5, 7),
                       new Pions("¤6", "Black",6, 7),
                       new Pions("¤7", "Black",7, 7),
                       new Pions("¤8", "Black",8, 7)
                   };
            return placement;
        }

        // Fonction de création des cases du plateau
        private void BoxChessboard(int x, int y)
        {
            if (y % 2 == 1)
            {
                if (x % 2 == 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (x % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
            }

            Pions Pion = FindPion(x, y);
            if (Pion != null) 
            {
                if (Pion.Color == "White")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write(" " + Pion.NamePions.Substring(0,1) + " ") ;
                Console.ForegroundColor = ConsoleColor.White;
            }else
            {
                Console.Write("   ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        // Surcharge de la creation des cases du plateau pour afficher la list des coups possibles
        private void BoxChessboard(int x, int y, List<int[]> possibleMoove)
        {
            if (y % 2 == 1)
            {
                if (x % 2 == 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
            }
            else
            {
                if (x % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
            }

            Pions Pion = FindPion(x, y);
            // Test si cette case est une case de mouvement possible
            bool checksPossibleMoove = false;
            foreach (int[] aCase in possibleMoove)
            {
                if (aCase[0] == x && aCase[1] == y)
                {
                    checksPossibleMoove = true;
                    break;
                }
            }

            if (Pion != null)
            {
                if (Pion.Color == "White")
                {
                    if(checksPossibleMoove && Pion.Color != color[player])
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                }
                else
                {
                    if (checksPossibleMoove && Pion.Color != color[player])
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                }
                Console.Write(" " + Pion.NamePions.Substring(0, 1) + " ");
            }
            else
            {
                if (checksPossibleMoove)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" + ");
                }
                else
                {
                    Console.Write("   ");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        // Fonction d'affichage du plateau
        public void ShowChessboard()
        {
            // Netoyage de la console
            Console.Clear();

            Console.WriteLine("   A  B  C  D  E  F  G  H");
            for (byte i = 1; i <= 8; i++)
            {
                ligne(i);
            }
            Console.WriteLine("   A  B  C  D  E  F  G  H");

            void ligne(int n)
            {
                Console.Write(n.ToString()+" ");
                for (byte i = 1; i <= 8; i++)
                {
                    BoxChessboard(i, n);
                }
                Console.WriteLine(" "+n.ToString());
            }
        }
        // Surcharge de l'affichage du plateau pour afficher la list des coups possibles
        public void ShowChessboard(List<int[]> possibleMoove)
        {
            // Netoyage de la console
            Console.Clear();

            Console.WriteLine("   A  B  C  D  E  F  G  H");
            for (byte i = 1; i <= 8; i++)
            {
                ligne(i);
            }
            Console.WriteLine("   A  B  C  D  E  F  G  H");

            void ligne(int n)
            {
                Console.Write(n.ToString() + " ");
                for (byte i = 1; i <= 8; i++)
                {
                    BoxChessboard(i, n, possibleMoove);
                }
                Console.WriteLine(" " + n.ToString());
            }
        }

        // Verification de l'entrer clavier d'une case
        public static bool ChecksCase(string selectPion)
        {
            if (selectPion.Length == 2)
            {
                if (int.TryParse(selectPion.Substring(1, 1), out _))
                {
                    switch (Convert.ToInt32(selectPion.Substring(1, 1)))
                    {
                        case (1): break;
                        case (2): break;
                        case (3): break;
                        case (4): break;
                        case (5): break;
                        case (6): break;
                        case (7): break;
                        case (8): break;
                        default:
                            return false;
                    }
                }
                else
                {
                    return false;
                }
                switch (selectPion.Substring(0, 1))
                {
                    case ("A"): break;
                    case ("B"): break;
                    case ("C"): break;
                    case ("D"): break;
                    case ("E"): break;
                    case ("F"): break;
                    case ("G"): break;
                    case ("H"): break;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        // Fonction de recherche d'un pion
        private Pions FindPion(int x, int y)
        {
            foreach (Pions Pion in this.ListPions)
            {
                if (Pion.PositionX == x && Pion.PositionY == y)
                {
                    return Pion;
                }
            }
            return null;
        }
    }
}
