using System;

namespace Wumpus
{
    public interface PlayerAbstractFactory
    {
        PlayerInterface CreateNewPlayer(int forestDimension);
    }
}
