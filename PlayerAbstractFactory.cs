namespace Wumpus
{
    /// This class is responsible for interfacing the creation of a player.
    /// The purpose of this class is to allow the modules to be decoupled.
    /// Through this interface, the general module determines what the interface of the player creation that consumes it should be.
    public interface PlayerAbstractFactory
    {
        PlayerInterface CreateNewPlayer(int forestDimension);
    }
}
