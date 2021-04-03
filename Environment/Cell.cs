using System.ComponentModel;
using Wumpus.Character;

namespace Wumpus.Environment
{
    public enum CellOdour
    {
        [Description("om")]
        Bad,
        [Description("on")]
        Neutral
    }

    public enum CellSpeedWind
    {
        [Description("vf")]
        Low,
        [Description("vt")]
        Strong
    }

    public enum CellLuminosity
    {
        [Description("lf")]
        Low,
        [Description("lt")]
        Strong
    }

    public class Cell
    {
        public CellOdour Odour { get; set; }
        public CellSpeedWind SpeedWind { get; set; }
        public CellLuminosity Luminosity { get; set; }
        public CellType Type { get; set; }

        public Cell(CellType type)
        {
            this.Type = type;

            this.Odour = CellOdour.Neutral;
            this.SpeedWind = CellSpeedWind.Low;
            this.Luminosity = CellLuminosity.Low;
        }

        internal CellMemory GetPlayerForestState()
        {
            CellMemory cellMemory = new CellMemory();

            cellMemory.ProbabilityMonster = this.Type == CellType.Monster ? 100 : 0;
            cellMemory.ProbabilityCrevasse = this.Type == CellType.Crevasse ? 100 : 0;

            cellMemory.ExistOdour = this.Odour == CellOdour.Bad ? 1 : 0;
            cellMemory.ExistWind = this.SpeedWind == CellSpeedWind.Strong ? 1 : 0;
            cellMemory.ExistLuminosity = this.Luminosity == CellLuminosity.Strong ? 1 : 0;

            return cellMemory;
        }
    }
}
