using System;
using Wumpus.Character;
using Wumpus.Environment;

namespace Wumpus
{
    /// <summary>
    /// This class is responsible for controlling the match at each level.
    /// The purpose of this class is to initialize the forest and the player, process the player's reflection, execute the player's displacement and signal the screen exit.
    /// The execution of these steps is done at each level, with each level being an independent match.
    /// </summary>
    public class Match
    {
        private int score;
        private int level;
        private int[] playerPosition;
        private ForestInterface magicForest;
        public PlayerInterface player;

        public String messages = String.Empty;

        /// <summary>
        /// Constructor method that initializes the match.
        /// Create the forest and the player at each level. Each level is an independent match.
        /// Receives as a dependency injection a forest factory interface and a player factory interface.
        /// </summary>
        public Match(int level, ForestAbstractFactory forestFactory, PlayerAbstractFactory playerFactory)
        {
            score = 0;
            this.level = level;

            magicForest = forestFactory.CreateNewForest(level);
            int forestSize = magicForest.InitForest();

            player = playerFactory.CreateNewPlayer(forestSize);
        }

        /// <summary>
        /// Constructor method that initializes the match for tests methods.
        /// Receive a predetermined forest configuration object.
        /// Create the forest and the player at each level. Each level is an independent match.
        /// Receives as a dependency injection a forest factory interface and a player factory interface.
        /// </summary>
        public Match(int level, ForestConfiguration forestConfiguration, ForestAbstractFactory forestFactory, PlayerAbstractFactory playerFactory)
        {
            score = 0;
            this.level = level;
            
            magicForest = forestFactory.CreateNewForest(level);
            int forestSize = magicForest.InitForestForTests(forestConfiguration);

            player = playerFactory.CreateNewPlayer(forestSize);
        }

        /// <summary>
        /// Method that runs the match itself.
        /// </summary>
        public int PlayMatch()
        {
            RegisterOutPut(magicForest.ToString());

            bool ongoingMatch = true;

            do
            {
                playerPosition = player.UpdatePlayerPosition(magicForest.PlayerSpawnL, magicForest.PlayerSpawnC);
                bool playerIsAlive = true;

                RegisterOutPut("Bob appeared in cell [" + playerPosition[0] + "," + playerPosition[1] + "]");

                ProcessMatch(out ongoingMatch, out playerIsAlive);

                ProcessPlayer(playerIsAlive);
            }
            while (ongoingMatch);

            score += this.CalculateScoreFromLevel(); 

            return score;
        }

        /// <summary>
        /// Processes player action at each new position within the match.
        /// </summary>
        private void ProcessPlayer(bool playerIsAlive)
        {
            if (playerIsAlive == false)
            {
                RegisterOutPut("Bob is dead");

                UpdatePlayerForestMemory();
                score -= this.CalculateScoreFromLevel();
            }
        }

        /// <summary>
        /// Method that checks the player's status to allow the match to continue or end.
        /// </summary>
        private void ProcessMatch(out bool ongoingMatch, out bool playerIsAlive)
        {
            do
            {
                UpdatePlayerForestMemory();

                ExplorerNode node = player.Play();

                ongoingMatch = !MoveTowards(node);
                playerIsAlive = GetPlayerStatus();
            }
            while (playerIsAlive && ongoingMatch);
        }

        private void UpdatePlayerForestMemory()
        {
            CellMemory cellMemory = magicForest.Grid[playerPosition[0], playerPosition[1]].GetPlayerForestState();
            player.ObserveAndMemorizeCurrentPosition(cellMemory);
        }

        /// <summary>
        /// Method that calculates the player's score when the match ends for that level.
        /// </summary>
        private int CalculateScoreFromLevel()
        {
            return (level + GameConfiguration.ForestMinimumDimension) * (level + GameConfiguration.ForestMinimumDimension) * GameConfiguration.ScoreConstantMultiplier;
        }

        /// <summary>
        /// Method that queries the forest to determine whether the player's position makes him alive or dead.
        /// </summary>
        public bool GetPlayerStatus()
        {
            CellType cellType = magicForest.Grid[playerPosition[0], playerPosition[1]].Type;

            if(cellType == CellType.Monster || cellType == CellType.Crevasse)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Move the player towards one of the 4 directions, if the probability of a monster on the arrival cell is non-zero, the player throws a stone
        /// </summary>
        public bool MoveTowards(ExplorerNode d)
        {
            if(d.Direction == NodeDirection.Portal)
                RegisterOutPut("Bob takes the portal and goes to the next level.");
            else
                RegisterOutPut("Bob goes to " + d.Direction.GetDescription());

            score -= 1;

            if(d.Direction == NodeDirection.NotFound)
            {
                return false;
            }
            else if(d.Direction == NodeDirection.Portal)
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

        /// <summary>
        /// Write screen output. Text messages from the forest, the player and the situation of the match.
        /// </summary>
        private void RegisterOutPut(string message)
        {
            Console.Write(message + System.Environment.NewLine);
            messages += message + System.Environment.NewLine;
        }

        /// <summary>
        /// Method that tells a certain cell in the forest that they must kill the monster because the player throws a stone.
        /// </summary>
        public void ThrowStone(ExplorerNode d)
        {
            score -= 10;

            RegisterOutPut("Bob throws a stone to " + d.Direction.GetDescription());

            magicForest.HitMonsterWithStone(d.GetLine(playerPosition[0]), d.GetColumn(playerPosition[1]));
        }
    }
}