using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Wumpus.Character;

namespace Wumpus.Environment
{
    public enum CellOdour
    {
        [Description("om")]
        Mauvaise,
        [Description("on")]
        Neutral
    }

    public enum CellVitesseVent
    {
        [Description("vf")]
        Low,
        [Description("vt")]
        Fort
    }

    public enum CellLuminosity
    {
        [Description("lf")]
        Low,
        [Description("lt")]
        Fort
    }

    public class Cell
    {
        public CellOdour Odour { get; set; }
        public CellVitesseVent VitesseVent { get; set; }
        public CellLuminosity Luminosity { get; set; }
        public CellType Type { get; set; }

        public Cell(CellType type)
        {
            this.Type = type;

            this.Odour = CellOdour.Neutral;
            this.VitesseVent = CellVitesseVent.Low;
            this.Luminosity = CellLuminosity.Low;
        }

        internal CellMemory GetPlayerForestState()
        {
            CellMemory cellMemory = new CellMemory();

            cellMemory.ProbabilityMonster = this.Type == CellType.Monstre ? 100 : 0;
            cellMemory.ProbabilityCave = this.Type == CellType.Crevasse ? 100 : 0;

            cellMemory.ExistOdour = this.Odour == CellOdour.Mauvaise ? 1 : 0;
            cellMemory.ExistVent = this.VitesseVent == CellVitesseVent.Fort ? 1 : 0;
            cellMemory.ExistLuminosity = this.Luminosity == CellLuminosity.Fort ? 1 : 0;

            return cellMemory;
        }
    }
}
