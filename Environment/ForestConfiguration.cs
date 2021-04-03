using System.Collections.Generic;

namespace Wumpus.Environment
{
    public class ForestConfiguration
    {
        public int[] PlayerPosition = new int[2];
        public int[] PortalPosition = new int[2];
        public List<int[]> MonstersPosition = new List<int[]>();
        public List<int[]> CrevassesPosition = new List<int[]>();
    }
}
