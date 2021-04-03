using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Wumpus.Environment;

namespace Wumpus.Character
{
    public class CellMemory
    {
        public float ProbabilityMonster { get; set; }
        public float ProbabilityCave { get; set; }
        public float ExistOdeur { get; set; }
        public float ExistVent { get; set; }
        public float ExistLuminosity { get; set; }
    }
}
