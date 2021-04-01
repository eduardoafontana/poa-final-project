using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Wumpus.Environment;

namespace Wumpus.Character
{
    public class CellMemory
    {
        public float ProbabilityMonster { get; private set; }
        public float ProbabilityCave { get; private set; }
        public float ExistOdeur { get; private set; }
        public float ExistVent { get; private set; }
        public float ExistLuminosite { get; private set; }

        public static CellMemory MemorizeForest(Cell cellForest) {
            CellMemory cellMemory = new CellMemory();

            cellMemory.ProbabilityMonster = cellForest.Type == CellType.Monstre ? 100 : 0;
            cellMemory.ProbabilityCave = cellForest.Type == CellType.Crevasse ? 100 : 0;

            cellMemory.ExistOdeur = cellForest.Odeur == CellOdeur.Mauvaise ? 1 : 0;
            cellMemory.ExistVent = cellForest.VitesseVent == CellVitesseVent.Fort ? 1 : 0;
            cellMemory.ExistLuminosite = cellForest.Luminosite == CellLuminosite.Fort ? 1 : 0;

            return cellMemory;
        }
    }
}
