using NUnit.Framework;
using Wumpus.Environment;

namespace Wumpus.Tests
{
    public class CellTest
    {
        [Test]
        public void CellTypeDescription()
        {
            Assert.AreEqual("O",  CellType.Portal.GetDescription());
            Assert.AreEqual("V",  CellType.Crevasse.GetDescription());
            Assert.AreEqual("M",  CellType.Monster.GetDescription());
            Assert.AreEqual(".",  CellType.Empty.GetDescription());
        }

        [Test]
        public void CreateCell()
        {
            Cell cell = new Cell(CellType.Portal);
            Assert.AreEqual(CellType.Portal, cell.Type);
            Assert.AreEqual(CellLuminosity.Low, cell.Luminosity);
            Assert.AreEqual(CellOdour.Neutral, cell.Odour);
            Assert.AreEqual(CellSpeedWind.Low, cell.SpeedWind);
        }
    }
}