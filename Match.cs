using System;

namespace Wumpus
{
    public class Match
    {
        private int score;
        private int level;
        private Foret magicForest;
        public Player player;

        public String messages = String.Empty;

        public Match(int level)
        {
            score = 0;
            this.level = level;

            magicForest = new Foret(level);
            magicForest.InitForest();

            player = new Player("Bob", magicForest.Size);
        }

        public Match(int level, ForestConfiguration forestConfiguration)
        {
            score = 0;
            this.level = level;
            
            magicForest = new Foret(level);
            magicForest.InitForestForTests(forestConfiguration);

            player = new Player("Bob", magicForest.Size);
        }

        public int PlayMatch()
        {
            RegisterOutPut(magicForest.ToString());

            bool partie_en_cours = true;

            do
            {
                player.UpdatePlayerPosition(magicForest.PlayerSpawnL, magicForest.PlayerSpawnC);
                bool joueur_en_vie = true;

                RegisterOutPut(player.Name + " est apparu en case [" + player.Pos_l + "," + player.Pos_c + "]");

                do
                {
                    MemoryManager.Node node = player.Play(magicForest);
                    partie_en_cours = !MoveTowards(node);
                    joueur_en_vie = GetPlayerStatus();
                }
                while(joueur_en_vie && partie_en_cours);
                
                if(joueur_en_vie == false)
                {
                    RegisterOutPut(player.Name + " est mort");

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

        public bool GetPlayerStatus()
        {
            CaseType type_case_j = magicForest.Grille[player.Pos_l, player.Pos_c].Type;

            if(type_case_j == CaseType.Monstre || type_case_j == CaseType.Crevasse)
            {
                return false;
            }

            return true;
        }

        //bouge le joueur vers l'une des 4 directions, si la proba d'un monstre sur la case d'arrivee est non nul, le joueur jete une pierre
        public bool MoveTowards(MemoryManager.Node d)
        {
            if(d.Direction == 'P')
                RegisterOutPut(player.Name + " prend le portail et passe au niveau suivant.");
            else
                RegisterOutPut(player.Name + " va vers " + d.Direction);

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

        private void RegisterOutPut(string message)
        {
            Console.Write(message + Environment.NewLine);
            messages += message + Environment.NewLine;
        }

        public void Jeter_pierre(MemoryManager.Node d)
        {
            score -= 10;

            RegisterOutPut(player.Name + " lance une pierre vers le " + d.Direction);

            magicForest.Utilisation_de_roches(d.GetLine(player.Pos_l), d.GetColumn(player.Pos_c));
        }
    }
}