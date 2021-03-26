using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Wumpus
{
    public class MemoryManager
    {
        public class Node : IComparable<Node>
        {
            private int line;
            private int column;
            public char Direction { get; private set; }
            public int Distance { get; private set; }

            public Node(int line, int column, char direction)
            {
                this.line = line;
                this.column = column;
                this.Direction = direction;
            }

            public Node(char direction, int distance)
            {
                this.Direction = direction;
                this.Distance = distance;
            }

            public Node(int line, int column, char direction, int distance)
            {
                this.line = line;
                this.column = column;
                this.Direction = direction;
                this.Distance = distance;
            }

            public int GetLine(int delta)
            {
                return this.line + delta;
            }

            public int GetLine()
            {
                return this.line;
            }

            public int GetColumn(int delta)
            {
                return this.column + delta;
            }

            public int GetColumn()
            {
                return this.column;
            }

            public int CompareTo([AllowNull] Node other)
            {
                return this.Distance.CompareTo(other.Distance);
            }
        }

        private List<Node> listNodes = new List<Node>();

        private void InitNodes()
        {
            listNodes.Add(new Node(-1, 0, 'N'));
            listNodes.Add(new Node(1, 0, 'S'));
            listNodes.Add(new Node(0, -1, 'W'));
            listNodes.Add(new Node(0, 1, 'E'));
        }

        public List<Node> OnNeighbors()
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
