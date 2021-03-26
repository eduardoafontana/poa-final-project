using System;
using Wumpus.Character;
using Wumpus.Environment;

namespace Wumpus
{
    public class Match
    {
        private int score;
        private int level;
        private Forest magicForest;
        public Player player;

        public String messages = String.Empty;

        public Match(int level)
        {
            score = 0;
            this.level = level;

            magicForest = new Forest(level);
            magicForest.InitForest();

            player = new Player("Bob", magicForest.Size);
        }

        public Match(int level, ForestConfiguration forestConfiguration)
        {
            score = 0;
            this.level = level;
            
            magicForest = new Forest(level);
            magicForest.InitForestForTests(forestConfiguration);

            player = new Player("Bob", magicForest.Size);
        }

        public int PlayMatch()
        {
            RegisterOutPut(magicForest.ToString());

            bool ongoingMatch = true;

            do
            {
                player.UpdatePlayerPosition(magicForest.PlayerSpawnL, magicForest.PlayerSpawnC);
                bool playerIsAlive = true;

                RegisterOutPut(player.Name + " est apparu en case [" + player.PlayerPositionL + "," + player.PlayerPositionC + "]");

                do
                {
                    player.ObserveAndMemorizeCurrentPosition(magicForest.Grid);
                    ExplorerNode node = player.Play();

                    ongoingMatch = !MoveTowards(node);
                    playerIsAlive = GetPlayerStatus();
                }
                while(playerIsAlive && ongoingMatch);
                
                if(playerIsAlive == false)
                {
                    RegisterOutPut(player.Name + " est mort");

                    player.ObserveAndMemorizeCurrentPosition(magicForest.Grid); // Review this call later
                    score -= this.CalculateScoreFromLevel(); 
                }
            }
            while(ongoingMatch);

            score += this.CalculateScoreFromLevel(); 

            return score;
        }

        private int CalculateScoreFromLevel()
        {
            return (level + GameConfiguration.ForestMinimumDimension) * (level + GameConfiguration.ForestMinimumDimension) * GameConfiguration.ScoreConstantMultiplier;
        }

        public bool GetPlayerStatus()
        {
            CellType cellType = magicForest.Grid[player.PlayerPositionL, player.PlayerPositionC].Type;

            if(cellType == CellType.Monstre || cellType == CellType.Crevasse)
            {
                return false;
            }

            return true;
        }

        //bouge le joueur vers l'une des 4 directions, si la proba d'un monstre sur la case d'arrivee est non nul, le joueur jete une pierre
        public bool MoveTowards(ExplorerNode d)
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
                    ThrowStone(d);

                player.UpdatePlayerPosition(d.GetLine(player.PlayerPositionL), d.GetColumn(player.PlayerPositionC));

                return false;
            }
        }

        private void RegisterOutPut(string message)
        {
            Console.Write(message + System.Environment.NewLine);
            messages += message + System.Environment.NewLine;
        }

        public void ThrowStone(ExplorerNode d)
        {
            score -= 10;

            RegisterOutPut(player.Name + " lance une pierre vers le " + d.Direction);

            magicForest.HitMonsterWithStone(d.GetLine(player.PlayerPositionL), d.GetColumn(player.PlayerPositionC));
        }
    }
}