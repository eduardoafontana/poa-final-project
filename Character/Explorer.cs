using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

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
    }
}
