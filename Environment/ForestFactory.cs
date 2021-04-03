namespace Wumpus.Environment
{
    /// <summary>
    /// This class is responsible for abstracting the creation of a new forest.
    /// The objective of this class is to make the creation of the forest a responsibility of the environment module, not of the class that uses the forest.
    /// This way the environment module is decoupled from the other modules in the system.
    /// </summary>
    public class ForestFactory : ForestAbstractFactory
    {
        public ForestInterface CreateNewForest(int size)
        {
            return new Forest(size);
        }
    }
}
