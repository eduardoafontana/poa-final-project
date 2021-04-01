using System;
using Wumpus.Character;
using Wumpus.Environment;

namespace Wumpus
{
    public class Match
    {
        private int score;
        private int level;
        private int[] playerPosition;
        private ForestInterface magicForest;
        public PlayerInterface player;

        public String messages = String.Empty;

        public Match(int level, EnvironmentAbstractFactory forestFactory)
        {
            score = 0;
            this.level = level;

            magicForest = forestFactory.CreateNewForest(level);
            int forestSize = magicForest.InitForest();

            player = new Player(forestSize);
        }

        public Match(int level, ForestConfiguration forestConfiguration, EnvironmentAbstractFactory forestFactory)
        {
            score = 0;
            this.level = level;
            
            magicForest = forestFactory.CreateNewForest(level);
            int forestSize = magicForest.InitForestForTests(forestConfiguration);

            player = new Player(forestSize);
        }

        public int PlayMatch()
        {
            RegisterOutPut(magicForest.ToString());

            bool ongoingMatch = true;

            do
            {
                playerPosition = player.UpdatePlayerPosition(magicForest.PlayerSpawnL, magicForest.PlayerSpawnC);
                bool playerIsAlive = true;

                RegisterOutPut("Bob est apparu en case [" + playerPosition[0] + "," + playerPosition[1] + "]");

                do
                {
                    UpdatePlayerForestMemory();

                    ExplorerNode node = player.Play();

                    ongoingMatch = !MoveTowards(node);
                    playerIsAlive = GetPlayerStatus();
                }
                while(playerIsAlive && ongoingMatch);
                
                if(playerIsAlive == false)
                {
                    RegisterOutPut("Bob est mort");

                    UpdatePlayerForestMemory();
                    score -= this.CalculateScoreFromLevel(); 
                }
            }
            while(ongoingMatch);

            score += this.CalculateScoreFromLevel(); 

            return score;
        }

        private void UpdatePlayerForestMemory()
        {
            CellMemory cellMemory = magicForest.Grid[playerPosition[0], playerPosition[1]].GetPlayerForestState();
            player.ObserveAndMemorizeCurrentPosition(cellMemory);
        }

        private int CalculateScoreFromLevel()
        {
            return (level + GameConfiguration.ForestMinimumDimension) * (level + GameConfiguration.ForestMinimumDimension) * GameConfiguration.ScoreConstantMultiplier;
        }

        public bool GetPlayerStatus()
        {
            CellType cellType = magicForest.Grid[playerPosition[0], playerPosition[1]].Type;

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
                RegisterOutPut("Bob prend le portail et passe au niveau suivant.");
            else
                RegisterOutPut("Bob va vers " + d.Direction);

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

                playerPosition = player.UpdatePlayerPosition(d.GetLine(playerPosition[0]), d.GetColumn(playerPosition[1]));

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

            RegisterOutPut("Bob lance une pierre vers le " + d.Direction);

            magicForest.HitMonsterWithStone(d.GetLine(playerPosition[0]), d.GetColumn(playerPosition[1]));
        }
    }
}