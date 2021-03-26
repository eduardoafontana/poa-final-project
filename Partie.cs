using System;

namespace Wumpus
{
    public class Partie
    {
        private int score;
        private int level;
        private Foret magicForest;
        public Player player;

        public String messages = String.Empty;

        public Partie(int level)
        {
            score = 0;
            this.level = level;

            magicForest = new Foret(level);
            magicForest.InitForest();

            player = new Player("Bob", magicForest.Size);
        }

        public Partie(int level, ForestConfiguration forestConfiguration)
        {
            score = 0;
            this.level = level;
            
            magicForest = new Foret(level);
            magicForest.InitForestForTests(forestConfiguration);

            player = new Player("Bob", magicForest.Size);
        }

        public int Jouer()
        {
            Console.WriteLine(magicForest);
            messages += magicForest + Environment.NewLine;

            bool partie_en_cours = true;

            do
            {
                player.UpdatePlayerPosition(magicForest.PlayerSpawnL, magicForest.PlayerSpawnC);
                bool joueur_en_vie = true;

                Console.WriteLine(player.Name + " est apparu en case [" + player.Pos_l + "," + player.Pos_c + "]");
                messages += player.Name + " est apparu en case [" + player.Pos_l + "," + player.Pos_c + "]" + Environment.NewLine;

                do
                {
                    MemoryManager.Node node = player.Play(magicForest);
                    partie_en_cours = !Bouger_vers(node);
                    joueur_en_vie = Etat_Joueur();
                }
                while(joueur_en_vie && partie_en_cours);
                
                if(joueur_en_vie == false)
                {
                    Console.WriteLine(player.Name + " est mort");
                    messages += player.Name + " est mort" + Environment.NewLine;

                    player.ObserveAndMemorizeCurrentPosition(magicForest.Grille); // Review this call later
                    score -= this.CalculateScoreFromLevel(); 
                }
            }
            while(partie_en_cours);

            score += this.CalculateScoreFromLevel(); 

            return score;
        }

        private int CalculateScoreFromLevel()
        {
            return (level + 2) * (level + 2) * 10;
        }

        public bool Etat_Joueur()
        {
            CaseType type_case_j = magicForest.Grille[player.Pos_l, player.Pos_c].Type;

            if(type_case_j == CaseType.Monstre || type_case_j == CaseType.Crevasse)
            {
                return false;
            }

            return true;
        }

        //bouge le joueur vers l'une des 4 directions, si la proba d'un monstre sur la case d'arrivee est non nul, le joueur jete une pierre
        public bool Bouger_vers(MemoryManager.Node d)
        {
            if(d.Direction == 'P'){
                Console.WriteLine(player.Name + " prend le portail et passe au niveau suivant.");
                messages += player.Name + " prend le portail et passe au niveau suivant." + Environment.NewLine;
            }
            else{
                Console.WriteLine(player.Name + " va vers " + d.Direction);
                messages += player.Name + " va vers " + d.Direction + Environment.NewLine;
            }

            score -= 1;

            if(d.Direction == 'X')
            {
                return false;
            }
            else if(d.Direction == 'P')
            {
                return true;
            }
            else
            {
                if(player.NeedThrowStone(d))
                    Jeter_pierre(d);

                player.UpdatePlayerPosition(d.GetLine(player.Pos_l), d.GetColumn(player.Pos_c));

                return false;
            }
        }

        public void Jeter_pierre(MemoryManager.Node d)
        {
            score -= 10;

            Console.WriteLine(player.Name + " lance une pierre vers le " + d.Direction);
            messages += player.Name + " lance une pierre vers le " + d.Direction + Environment.NewLine;

            magicForest.Utilisation_de_roches(d.GetLine(player.Pos_l), d.GetColumn(player.Pos_c));
        }
    }
}