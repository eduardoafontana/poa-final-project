namespace Wumpus.Character
{
    /// <summary>
    /// This class is responsible for abstracting the creation of a new player.
    /// The objective of this class is to make the creation of the player a responsibility of the character module, not of the class that uses the player.
    /// This way the character module is decoupled from the other modules in the system.
    /// </summary>
    public class PlayerFactory : PlayerAbstractFactory
    {
        public PlayerInterface CreateNewPlayer(int forestDimension)
        {
            return new Player(forestDimension);
        }
    }
}
