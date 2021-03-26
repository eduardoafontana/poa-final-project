using System;
using NUnit.Framework;
using Wumpus;
using System.Linq;
using System.Collections.Generic;

namespace tests
{
    public class ForetCreateTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateForet_Level1_Elements()
        {
            int niveau = 1;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(1, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(1, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level2_Elements()
        {
            int niveau = 2;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(3, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(2, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level3_Elements()
        {
            int niveau = 3;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(5, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(3, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level4_Elements()
        {
            int niveau = 4;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(7, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(5, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level5_Elements()
        {
            int niveau = 5;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(9, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(7, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level6_Elements()
        {
            int niveau = 6;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(12, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(9, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level7_Elements()
        {
            int niveau = 7;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(16, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(12, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level8_Elements()
        {
            int niveau = 8;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(20, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(15, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level9_Elements()
        {
            int niveau = 9;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(24, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(18, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForet_Level10_Elements()
        {
            int niveau = 10;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            List<Cell> monstersFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Monstre).ToList();
            Assert.AreEqual(28, monstersFounded.Count());

            List<Cell> cavesFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Crevasse).ToList();
            Assert.AreEqual(21, cavesFounded.Count());

            List<Cell> portalsFounded = foret.Grid.Cast<Cell>().Where(i => i.Type == CellType.Portail).ToList();
            Assert.AreEqual(1, portalsFounded.Count());
        }

        [Test]
        public void CreateForest_PreConfigured()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 0};
            configuration.PortalPosition = new int[2] {2, 2};
            configuration.CavesPosition = new List<int[]>() { new int[] {0, 2} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {2, 0} };

            Forest foret = new Forest(1);
            foret.InitForestForTests(configuration);

            Assert.AreEqual(foret.PlayerSpawnL, 0);
            Assert.AreEqual(foret.PlayerSpawnC, 0);
            Assert.AreEqual(CellType.Portail, foret.Grid[2,2].Type);
            Assert.AreEqual(CellType.Monstre, foret.Grid[2,0].Type);
            Assert.AreEqual(CellType.Crevasse, foret.Grid[0,2].Type);
        }

        [Test]
        public void TestForet()
        {
            int niveau = 3;

            Forest foret = new Forest(niveau);
            foret.InitForest();

            Console.WriteLine(foret.ToString());
        }
    }
}