using System;
using Wumpus.Environment;

namespace Wumpus
{
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
