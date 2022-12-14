using JeuxEcheque;

Fonction Chessboard = new Fonction();

// Menu
do
{ 
    bool checksCase = true;
    bool checksMoove = true;
    string selectPion;
    do
    {
        do
        {
            // Affichage du plateau de jeu
            Chessboard.ShowChessboard();

            // Mise en place du menu de selection de piece
            Console.WriteLine("Au tour du joueur " + Fonction.ConvertPlayer(Fonction.color[Fonction.player]));
            Console.WriteLine(" - Quelle pièce selectionner ?");
            selectPion = Console.ReadLine();

            // Verif de l'entrer de la console, a verifier
            checksCase = Fonction.ChecksCase(selectPion);
        } while (!checksCase);

        // Application des mouvements demandees
        checksMoove = Chessboard.Moove(Fonction.ConvertCase(selectPion.Substring(0, 1)), Convert.ToInt32(selectPion.Substring(1, 1)));
    } while (!checksMoove);

    // Passage au joueur suivant
    Fonction.player++;
    Fonction.player %= 2;

} while (!Fonction.EndGame);

Fonction.player++;
Fonction.player %= 2;

Chessboard.ShowChessboard();

Console.WriteLine("Fin du jeu, victoir du joueur " + Fonction.ConvertPlayer(Fonction.color[Fonction.player]) +  " !!");
Console.ReadKey();


