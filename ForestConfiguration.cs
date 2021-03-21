using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wumpus
{
    public class ForestConfiguration
    {
        public int[] PlayerPosition = new int[2];
        public int[] PortalPosition = new int[2];
        public List<int[]> MonstersPosition = new List<int[]>();
        public List<int[]> CavesPosition = new List<int[]>();
    }
}
