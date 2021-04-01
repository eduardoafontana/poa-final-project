using System;
using Wumpus.Environment;

namespace Wumpus
{
    public interface ForestAbstractFactory
    {
        ForestInterface CreateNewForest(int size);
    }
}
