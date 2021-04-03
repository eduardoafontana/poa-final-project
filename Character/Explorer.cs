using System.Collections.Generic;
using System.Linq;

namespace Wumpus.Character
{
    public class Explorer
    {
        private List<ExplorerNode> listNodes = new List<ExplorerNode>();

        private void InitNodes()
        {
            listNodes.Add(new ExplorerNode(-1, 0, 'N'));
            listNodes.Add(new ExplorerNode(1, 0, 'S'));
            listNodes.Add(new ExplorerNode(0, -1, 'W'));
            listNodes.Add(new ExplorerNode(0, 1, 'E'));
        }

        public List<ExplorerNode> OnNeighbors()
        {
            return listNodes;
        }

        public static Explorer GetInstance()
        {
            Explorer memoryManager = new Explorer();
            memoryManager.InitNodes();

            return memoryManager;
        }

        public static IEnumerable<ExplorerNode> GetNeighborsFilted(Memory[,] forestMemory, int[] l0c0)
        {
            int l0 = l0c0[0];
            int c0 = l0c0[1];

            return Explorer.GetInstance().OnNeighbors()
            .Where(x => x.IsValidPosition(x.GetLine(l0), x.GetColumn(c0), forestMemory.GetLength(0)))
            .Where(x => forestMemory[x.GetLine(l0), x.GetColumn(c0)].ProbabilityCrevasse < 100);
        }
    }
}
