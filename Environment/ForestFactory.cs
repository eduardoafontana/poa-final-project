namespace Wumpus.Environment
{
    public class ForestFactory : ForestAbstractFactory
    {
        public ForestInterface CreateNewForest(int size)
        {
            return new Forest(size);
        }
    }
}
