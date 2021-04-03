using Wumpus.Character;

namespace Wumpus
{
    public interface PlayerInterface
    {
        ExplorerNode Play();
        void ObserveAndMemorizeCurrentPosition(CellMemory cellMemory);
        bool NeedThrowStone(ExplorerNode explorerNode);
        int[] UpdatePlayerPosition(int magicForestPlayerSpawnL, int magicForestPlayerSpawnC);
    }
}
