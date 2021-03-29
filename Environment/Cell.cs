using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Wumpus.Interfaces;

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
        Faible,
        [Description("vt")]
        Fort
    }

    public enum CellLuminosite
    {
        [Description("lf")]
        Faible,
        [Description("lt")]
        Fort
    }

    public class Cell : CellInterface
    {
        public CellOdeur Odeur { get; set; }
        public CellVitesseVent VitesseVent { get; set; }
        public CellLuminosite Luminosite { get; set; }
        public CellType Type { get; set; }

        public Cell(CellType type)
        {
            this.Type = type;

            this.Odeur = CellOdeur.Neutre;
            this.VitesseVent = CellVitesseVent.Faible;
            this.Luminosite = CellLuminosite.Faible;
        }
    }
}
