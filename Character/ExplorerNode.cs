using System;
using System.Diagnostics.CodeAnalysis;

namespace Wumpus.Character
{
    public class ExplorerNode : IComparable<ExplorerNode>
    {
        private int line;
        private int column;
        public char Direction { get; private set; }
        public int Distance { get; private set; }

        public ExplorerNode(int line, int column, char direction)
        {
            this.line = line;
            this.column = column;
            this.Direction = direction;
        }

        public ExplorerNode(char direction, int distance)
        {
            this.Direction = direction;
            this.Distance = distance;
        }

        public ExplorerNode(int line, int column, char direction, int distance)
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

        public int CompareTo([AllowNull] ExplorerNode other)
        {
            return this.Distance.CompareTo(other.Distance);
        }

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
