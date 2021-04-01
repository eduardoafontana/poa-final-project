using System;

namespace Wumpus.Environment
{
    public class ForestFactory : EnvironmentAbstractFactory
    {
        public ForestInterface CreateNewForest(int size)
        {
            return new Forest(size);
        }
    }
}
