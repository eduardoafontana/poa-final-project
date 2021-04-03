namespace Wumpus
{
    /// <summary>
    /// This class is responsible for starting the system.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
