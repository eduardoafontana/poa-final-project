using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Wumpus
{
    public enum CaseOdeur
    {
        [Description("om")]
        Mauvaise,
        [Description("on")]
        Neutre
    }

    public enum CaseVitesseVent
    {
        [Description("vf")]
        Faible,
        [Description("vt")]
        Fort
    }

    public enum CaseLuminosite
    {
        [Description("lf")]
        Faible,
        [Description("lt")]
        Fort
    }

    public class Case
    {
        public CaseOdeur Odeur { get; set; }
        public CaseVitesseVent VitesseVent { get; set; }
        public CaseLuminosite Luminosite { get; set; }
        public CaseType Type { get; set; }

        public Case(CaseType type)
        {
            this.Type = type;

            this.Odeur = CaseOdeur.Neutre;
            this.VitesseVent = CaseVitesseVent.Faible;
            this.Luminosite = CaseLuminosite.Faible;
        }
    }
}
