using System;
using NUnit.Framework;
using Wumpus;

namespace tests
{
    public class CellTest
    {
        [Test]
        public void CellTypeDescription()
        {
            Assert.AreEqual("O",  CellType.Portail.GetDescription());
            Assert.AreEqual("V",  CellType.Crevasse.GetDescription());
            Assert.AreEqual("M",  CellType.Monstre.GetDescription());
            Assert.AreEqual(".",  CellType.Vide.GetDescription());
        }

        [Test]
        public void CreateCell()
        {
            Cell cell = new Cell(CellType.Portail);
            Assert.AreEqual(CellType.Portail, cell.Type);
            Assert.AreEqual(CellLuminosite.Faible, cell.Luminosite);
            Assert.AreEqual(CellOdeur.Neutre, cell.Odeur);
            Assert.AreEqual(CellVitesseVent.Faible, cell.VitesseVent);
        }
    }
}