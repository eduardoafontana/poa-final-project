using Wumpus.Character;

namespace Wumpus
{
    /// This class is responsible for interfacing the player class.
    /// The purpose of this class is to allow the classes to be decoupled.
    /// Through this interface, the general module determines what the player interface that consumes it should be.
    public interface PlayerInterface
    {
        ExplorerNode Play();
        void ObserveAndMemorizeCurrentPosition(CellMemory cellMemory);
        bool NeedThrowStone(ExplorerNode explorerNode);
        int[] UpdatePlayerPosition(int magicForestPlayerSpawnL, int magicForestPlayerSpawnC);
    }
}
