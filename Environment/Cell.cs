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

    public enum CellSpeedVent
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
        public CellSpeedVent SpeedVent { get; set; }
        public CellLuminosity Luminosity { get; set; }
        public CellType Type { get; set; }

        public Cell(CellType type)
        {
            this.Type = type;

            this.Odour = CellOdour.Neutral;
            this.SpeedVent = CellSpeedVent.Low;
            this.Luminosity = CellLuminosity.Low;
        }

        internal CellMemory GetPlayerForestState()
        {
            CellMemory cellMemory = new CellMemory();

            cellMemory.ProbabilityMonster = this.Type == CellType.Monstre ? 100 : 0;
            cellMemory.ProbabilityCave = this.Type == CellType.Crevasse ? 100 : 0;

            cellMemory.ExistOdour = this.Odour == CellOdour.Mauvaise ? 1 : 0;
            cellMemory.ExistVent = this.SpeedVent == CellSpeedVent.Fort ? 1 : 0;
            cellMemory.ExistLuminosity = this.Luminosity == CellLuminosity.Fort ? 1 : 0;

            return cellMemory;
        }
    }
}
