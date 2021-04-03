using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Wumpus.Character;

namespace Wumpus.Environment
{
    public enum CellOdeur
    {
        [Description("om")]
        Mauvaise,
        [Description("on")]
        Neutre
    }

    public enum CellVitesseVent
    {
        [Description("vf")]
        Low,
        [Description("vt")]
        Fort
    }

    public enum CellLuminosite
    {
        [Description("lf")]
        Low,
        [Description("lt")]
        Fort
    }

    public class Cell
    {
        public CellOdeur Odeur { get; set; }
        public CellVitesseVent VitesseVent { get; set; }
        public CellLuminosite Luminosite { get; set; }
        public CellType Type { get; set; }

        public Cell(CellType type)
        {
            this.Type = type;

            this.Odeur = CellOdeur.Neutre;
            this.VitesseVent = CellVitesseVent.Low;
            this.Luminosite = CellLuminosite.Low;
        }

        internal CellMemory GetPlayerForestState()
        {
            CellMemory cellMemory = new CellMemory();

            cellMemory.ProbabilityMonster = this.Type == CellType.Monstre ? 100 : 0;
            cellMemory.ProbabilityCave = this.Type == CellType.Crevasse ? 100 : 0;

            cellMemory.ExistOdeur = this.Odeur == CellOdeur.Mauvaise ? 1 : 0;
            cellMemory.ExistVent = this.VitesseVent == CellVitesseVent.Fort ? 1 : 0;
            cellMemory.ExistLuminosite = this.Luminosite == CellLuminosite.Fort ? 1 : 0;

            return cellMemory;
        }
    }
}
