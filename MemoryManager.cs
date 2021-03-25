using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wumpus
{
    public class MemoryManager
    {
        public class Node
        {
            private int line;
            private int column;

            public Node(int line, int column)
            {
                this.line = line;
                this.column = column;
            }

            public int GetLine(int delta)
            {
                return this.line + delta;
            }

            public int GetColumn(int delta)
            {
                return this.column + delta;
            }
        }

        private List<Node> listNodes = new List<Node>();

        private void InitNodes()
        {
            listNodes.Add(new Node(-1, 0));
            listNodes.Add(new Node(1, 0));
            listNodes.Add(new Node(0, -1));
            listNodes.Add(new Node(0, 1));
        }

        public List<Node> OnNeighborhoods()
        {
            return listNodes;
        }

        public static MemoryManager GetInstance()
        {
            MemoryManager memoryManager = new MemoryManager();
            memoryManager.InitNodes();

            return memoryManager;
        }
    }
}
