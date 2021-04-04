using System.Collections.Generic;
using System.Linq;

namespace Wumpus.Character
{
    /// <summary>
    /// This class is responsible for managing the displacement nodes.
    /// This class aims to deliver the group of nodes with the differences in the four possible directions of displacement.
    /// In addition it has filters with application of rules such as finding neighboring nodes that do not contain crevasse.
    /// </summary>
    public class Explorer
    {
        private List<ExplorerNode> listNodes = new List<ExplorerNode>();

        private void InitNodes()
        {
            listNodes.Add(new ExplorerNode(-1, 0, NodeDirection.North));
            listNodes.Add(new ExplorerNode(1, 0, NodeDirection.South));
            listNodes.Add(new ExplorerNode(0, -1, NodeDirection.West));
            listNodes.Add(new ExplorerNode(0, 1, NodeDirection.East));
        }

        public List<ExplorerNode> OnNeighbors()
        {
            return listNodes;
        }

        /// <summary>
        /// This method is a custom constructor that allows you to obtain an instance of Explorer through factoring pattern.
        /// </summary>
        public static Explorer GetInstance()
        {
            Explorer memoryManager = new Explorer();
            memoryManager.InitNodes();

            return memoryManager;
        }

        /// <summary>
        /// This method allows to obtain a list not processed of valid neighboring cells considering the crevasse probability filter below 100%.
        /// </summary>
        public static IEnumerable<ExplorerNode> GetNeighborsFiltered(Memory[,] forestMemory, int[] l0c0)
        {
            int l0 = l0c0[0];
            int c0 = l0c0[1];

            return Explorer.GetInstance().OnNeighbors()
            .Where(x => x.IsValidPosition(x.GetLine(l0), x.GetColumn(c0), forestMemory.GetLength(0)))
            .Where(x => forestMemory[x.GetLine(l0), x.GetColumn(c0)].ProbabilityCrevasse < 100);
        }
    }
}
