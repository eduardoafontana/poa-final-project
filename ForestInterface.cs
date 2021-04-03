using System;
using Wumpus.Environment;

namespace Wumpus
{
    /// This class is responsible for interfacing the forest class.
    /// The purpose of this class is to allow the classes to be decoupled.
    /// Through this interface, the general module determines what the forest interface that consumes it should be.
    public interface ForestInterface
    {
        int Size { get; }
        Cell[,] Grid { get; }
        int PlayerSpawnL { get; }
        int PlayerSpawnC { get; }

        int InitForest();

        int InitForestForTests(ForestConfiguration forestConfiguration);

        String ToString();

        void HitMonsterWithStone(int l, int c);
    }
}
