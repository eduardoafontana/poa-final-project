namespace Wumpus
{
    /// <summary>
    /// This class is responsible for interfacing the creation of a forest.
    /// The purpose of this class is to allow the modules to be decoupled.
    /// Through this interface, the general module determines what the interface of the forest creation that consumes it should be.
    /// </summary>
    public interface ForestAbstractFactory
    {
        ForestInterface CreateNewForest(int size);
    }
}
