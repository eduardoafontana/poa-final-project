using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Wumpus.Environment;

namespace Wumpus.Tests
{
    public class ForetNeighborStatusTest
    {
        [Test]
        public void Neighbor_Level1_Portal()
        {
            int niveau = 1;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            for (int l = 0; l < foret.Grid.GetLength(0); l++)
            {
                for (int c = 0; c < foret.Grid.GetLength(1); c++)
                {
                    if(foret.Grid[l,c].Type == CellType.Portal)
                        Assert.AreEqual(CellLuminosity.Fort, foret.Grid[l, c].Luminosity);
                    else
                        Assert.AreEqual(CellLuminosity.Low, foret.Grid[l, c].Luminosity);
                }
            }
        }

        [Test]
        public void Neighbor_Level1_Monster()
        {
            int niveau = 1;
            Forest foret = new Forest(niveau);
            foret.InitForest();

            int limitRight = foret.Size - 1;
            int limitLeft = 0;
            int limitTop = 0;
            int limitDown = foret.Size - 1;
            
            for (int l = 0; l < foret.Grid.GetLength(0); l++)
            {
                for (int c = 0; c < foret.Grid.GetLength(1); c++)
                {
                    if(foret.Grid[l,c].Type != CellType.Monstre)
                        continue;

                    if(c + 1 <= limitRight)
                        Assert.AreEqual(CellOdour.Mauvaise, foret.Grid[l, c + 1].Odour);

                    if(c - 1 >= limitLeft)
                        Assert.AreEqual(CellOdour.Mauvaise, foret.Grid[l, c - 1].Odour);

                    if(l - 1 >= limitTop)
                        Assert.AreEqual(CellOdour.Mauvaise, foret.Grid[l - 1, c].Odour);

                    if(l + 1 <= limitDown)
                        Assert.AreEqual(CellOdour.Mauvaise, foret.Grid[l + 1, c].Odour);
                }
            }
        }

        [Test]
        public void Neighbor_Level1_WindSpeed()
        {
            int niveau = 1;
            Forest foret = new Forest(niveau);
            foret.InitForest();

            int limitRight = foret.Size - 1;
            int limitLeft = 0;
            int limitTop = 0;
            int limitDown = foret.Size - 1;
            
            for (int l = 0; l < foret.Grid.GetLength(0); l++)
            {
                for (int c = 0; c < foret.Grid.GetLength(1); c++)
                {
                    if(foret.Grid[l,c].Type != CellType.Crevasse)
                        continue;

                    if(c + 1 <= limitRight)
                        Assert.AreEqual(CellVitesseVent.Fort, foret.Grid[l, c + 1].VitesseVent);

                    if(c - 1 >= limitLeft)
                        Assert.AreEqual(CellVitesseVent.Fort, foret.Grid[l, c - 1].VitesseVent);

                    if(l - 1 >= limitTop)
                        Assert.AreEqual(CellVitesseVent.Fort, foret.Grid[l - 1, c].VitesseVent);

                    if(l + 1 <= limitDown)
                        Assert.AreEqual(CellVitesseVent.Fort, foret.Grid[l + 1, c].VitesseVent);
                }
            }
        }

        [Test]
        public void TestEnumReflectionProperty()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 0};
            configuration.PortalPosition = new int[2] {2, 2};
            configuration.CavesPosition = new List<int[]>() { new int[] {0, 2} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {2, 0} };

            PropertyInfo property = typeof(Cell).GetProperty("VitesseVent");

            Enum value = CellVitesseVent.Fort;

            Forest foret = new Forest(1);
            foret.InitForestForTests(configuration);
            
            Assert.AreEqual(CellVitesseVent.Low,  foret.Grid[0,0].VitesseVent);

            property.SetValue(foret.Grid[0,0], value);

            Assert.AreEqual(CellVitesseVent.Fort, foret.Grid[0,0].VitesseVent);
        }

        [Test]
        public void TestPlayerSpawnPosition()
        {
            Forest foret = new Forest(1);
            foret.InitForest();

            Assert.AreEqual(CellType.Vide, foret.Grid[foret.PlayerSpawnL, foret.PlayerSpawnC].Type);
        }
    }
}