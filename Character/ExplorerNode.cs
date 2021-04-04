using System;
using System.Diagnostics.CodeAnalysis;

namespace Wumpus.Character
{
    /// <summary>
    /// This class is responsible for calculating the direction relative to a given position.
    /// In other words, it finds the neighbor node at an informed initial position.
    /// This class aims to ensure that the neighboring node is always a valid node, that is, that it has a position in the matrix.
    /// This class implements the IComparable interface, which allows an ExplorerNode to be compared to another based on a custom rule.
    /// </summary>
    public class ExplorerNode : IComparable<ExplorerNode>
    {
        private int line;
        private int column;
        public NodeDirection Direction { get; private set; }
        public int Distance { get; private set; }

        public ExplorerNode(int line, int column, NodeDirection direction)
        {
            this.line = line;
            this.column = column;
            this.Direction = direction;
        }

        public ExplorerNode(NodeDirection direction, int distance)
        {
            this.Direction = direction;
            this.Distance = distance;
        }

        public ExplorerNode(int line, int column, NodeDirection direction, int distance)
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

        /// <summary>
        /// This method is the implementation of the IComparable interface that allows two nodes to be compared considering the distance between them.
        /// </summary>
        public int CompareTo([AllowNull] ExplorerNode other)
        {
            return this.Distance.CompareTo(other.Distance);
        }

        /// <summary>
        /// This method performs the validation of the neighboring cell from an informed cell.
        /// </summary>
        internal bool IsValidPosition(int l, int c, int size)
        {
            int limitRight = size;
            int limitLeft = 0;
            int limitTop = 0;
            int limitDown = size;

            if((c >= limitRight) || (c < limitLeft))
                return false;

            if((l < limitTop) || (l >= limitDown))
                return false;

            return true;
        }
    }
}
