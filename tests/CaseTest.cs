using System;
using NUnit.Framework;
using Wumpus;

namespace tests
{
    public class CaseTest
    {
        [Test]
        public void CaseTypeDescription()
        {
            Assert.AreEqual("O",  CaseType.Portail.GetDescription());
            Assert.AreEqual("V",  CaseType.Crevasse.GetDescription());
            Assert.AreEqual("M",  CaseType.Monstre.GetDescription());
            Assert.AreEqual(".",  CaseType.Vide.GetDescription());
        }

        [Test]
        public void CreateCase()
        {
            Case case1 = new Case(CaseType.Portail);
            Assert.AreEqual(CaseType.Portail, case1.Type);
            Assert.AreEqual(CaseLuminosite.Faible, case1.Luminosite);
            Assert.AreEqual(CaseOdeur.Neutre, case1.Odeur);
            Assert.AreEqual(CaseVitesseVent.Faible, case1.VitesseVent);
        }
    }
}