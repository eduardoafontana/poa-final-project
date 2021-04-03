using System.Collections.Generic;

namespace Wumpus.Environment
{
    /// <summary>
    /// This class is responsible for storing initial forest configuration data.
    /// The purpose of this class is to set a fixed forest configuration, because if not set, the forest configuration is generated randomly.
    /// This class is used in testing methods.
    /// </summary>
    public class ForestConfiguration
    {
        public int[] PlayerPosition = new int[2];
        public int[] PortalPosition = new int[2];
        public List<int[]> MonstersPosition = new List<int[]>();
        public List<int[]> CrevassesPosition = new List<int[]>();
    }
}
