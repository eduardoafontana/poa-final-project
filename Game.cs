using System;
using Wumpus.Character;
using Wumpus.Environment;

namespace Wumpus
{
    /// <summary>
    /// This class is responsible for handle the game.
    /// The purpose of this class is to control and to initialize the classes that are the main entities of the game and to control the score.
    /// As well as allowing the game to continue as the user progresses to the next level.
    /// </summary>
    public class Game
    {
        public void Start()
        {
            int level = 1;
            bool nextLevel = true;
            int globalScore = 0;

            while(nextLevel)
            {
                Match match = new Match(level, new ForestFactory(), new PlayerFactory());
                globalScore += match.PlayMatch();

                Console.WriteLine("the current score at the end of the level " + level + " is " + globalScore);
                Console.Write("Continue? (Y/N) : ");
                
                string r = Console.ReadLine();

                if(r.ToUpper().Equals("N"))
                    nextLevel = false;
                
                level++;
            }
        }
    }
}
