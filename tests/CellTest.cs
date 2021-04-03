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
            Assert.AreEqual("M",  CellType.Monstre.GetDescription());
            Assert.AreEqual(".",  CellType.Vide.GetDescription());
        }

        [Test]
        public void CreateCell()
        {
            Cell cell = new Cell(CellType.Portal);
            Assert.AreEqual(CellType.Portal, cell.Type);
            Assert.AreEqual(CellLuminosite.Low, cell.Luminosite);
            Assert.AreEqual(CellOdeur.Neutre, cell.Odeur);
            Assert.AreEqual(CellVitesseVent.Low, cell.VitesseVent);
        }
    }
}