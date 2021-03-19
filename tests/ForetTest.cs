using System;
using NUnit.Framework;
using Wumpus;
using System.Linq;
using System.Collections.Generic;

namespace tests
{
    public class ForetTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateForet_Level1_Elements()
        {
            int niveau = 1;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(1, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(1, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level2_Elements()
        {
            int niveau = 2;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(3, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(2, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level3_Elements()
        {
            int niveau = 3;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(5, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(3, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level4_Elements()
        {
            int niveau = 4;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(7, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(5, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level5_Elements()
        {
            int niveau = 5;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(9, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(7, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level6_Elements()
        {
            int niveau = 6;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(12, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(9, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level7_Elements()
        {
            int niveau = 7;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(16, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(12, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level8_Elements()
        {
            int niveau = 8;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(20, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(15, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level9_Elements()
        {
            int niveau = 9;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(24, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(18, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level10_Elements()
        {
            int niveau = 10;
            Foret foret = new Foret(niveau);
            
            List<Case> monstersFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Monstre).ToList();
            Assert.AreEqual(28, monstersFounded.Count());

            List<Case> cavesFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Crevasse).ToList();
            Assert.AreEqual(21, cavesFounded.Count());

            List<Case> portalsFounded = foret.Grille.Cast<Case>().Where(i => i.Type == CaseType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void TestForet()
        {
            int niveau = 3;

            Foret foret = new Foret(niveau);

            Console.WriteLine(foret.ToString());
        }
    }
}