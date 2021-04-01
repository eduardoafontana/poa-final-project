using System;
using Wumpus.Character;
using Wumpus.Environment;

namespace Wumpus
{
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

                Console.WriteLine("le score actuel à la fin du niveau " + level + " est de " + globalScore);
                Console.Write("continuer ? (Y/N) : ");
                
                string r = Console.ReadLine();

                if(r.ToUpper().Equals("N"))
                    nextLevel = false;
                
                level++;
            }
        }
    }
}
