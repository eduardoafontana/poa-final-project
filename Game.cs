using System;

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
                Partie partie = new Partie(level);
                globalScore += partie.Jouer();

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
