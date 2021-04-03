namespace Wumpus.Character
{
    public class PlayerFactory : PlayerAbstractFactory
    {
        public PlayerInterface CreateNewPlayer(int forestDimension)
        {
            return new Player(forestDimension);
        }
    }
}
