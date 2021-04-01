using System;
using Wumpus.Environment;

namespace Wumpus
{
    public interface EnvironmentAbstractFactory
    {
        ForestInterface CreateNewForest(int size);
    }
}
