using System;
using NUnit.Framework;
using Wumpus;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace tests
{
    public class ForetNeighborStatusTest
    {
        [Test]
        public void Neighbor_Level1_Portal()
        {
            int niveau = 1;
            Forest foret = new Forest(niveau);
            foret.InitForest();
            
            for (int l = 0; l < foret.Grille.GetLength(0); l++)
            {
                for (int c = 0; c < foret.Grille.GetLength(1); c++)
                {
                    if(foret.Grille[l,c].Type == CaseType.Portail)
                        Assert.AreEqual(CaseLuminosite.Fort, foret.Grille[l, c].Luminosite);
                    else
                        Assert.AreEqual(CaseLuminosite.Faible, foret.Grille[l, c].Luminosite);
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
            
            for (int l = 0; l < foret.Grille.GetLength(0); l++)
            {
                for (int c = 0; c < foret.Grille.GetLength(1); c++)
                {
                    if(foret.Grille[l,c].Type != CaseType.Monstre)
                        continue;

                    if(c + 1 <= limitRight)
                        Assert.AreEqual(CaseOdeur.Mauvaise, foret.Grille[l, c + 1].Odeur);

                    if(c - 1 >= limitLeft)
                        Assert.AreEqual(CaseOdeur.Mauvaise, foret.Grille[l, c - 1].Odeur);

                    if(l - 1 >= limitTop)
                        Assert.AreEqual(CaseOdeur.Mauvaise, foret.Grille[l - 1, c].Odeur);

                    if(l + 1 <= limitDown)
                        Assert.AreEqual(CaseOdeur.Mauvaise, foret.Grille[l + 1, c].Odeur);
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
            
            for (int l = 0; l < foret.Grille.GetLength(0); l++)
            {
                for (int c = 0; c < foret.Grille.GetLength(1); c++)
                {
                    if(foret.Grille[l,c].Type != CaseType.Crevasse)
                        continue;

                    if(c + 1 <= limitRight)
                        Assert.AreEqual(CaseVitesseVent.Fort, foret.Grille[l, c + 1].VitesseVent);

                    if(c - 1 >= limitLeft)
                        Assert.AreEqual(CaseVitesseVent.Fort, foret.Grille[l, c - 1].VitesseVent);

                    if(l - 1 >= limitTop)
                        Assert.AreEqual(CaseVitesseVent.Fort, foret.Grille[l - 1, c].VitesseVent);

                    if(l + 1 <= limitDown)
                        Assert.AreEqual(CaseVitesseVent.Fort, foret.Grille[l + 1, c].VitesseVent);
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

            PropertyInfo property = typeof(Case).GetProperty("VitesseVent");

            Enum value = CaseVitesseVent.Fort;

            Forest foret = new Forest(1);
            foret.InitForestForTests(configuration);
            
            Assert.AreEqual(CaseVitesseVent.Faible,  foret.Grille[0,0].VitesseVent);

            property.SetValue(foret.Grille[0,0], value);

            Assert.AreEqual(CaseVitesseVent.Fort, foret.Grille[0,0].VitesseVent);
        }

        [Test]
        public void TestPlayerSpawnPosition()
        {
            Forest foret = new Forest(1);
            foret.InitForest();

            Assert.AreEqual(CaseType.Vide, foret.Grille[foret.PlayerSpawnL, foret.PlayerSpawnC].Type);
        }
    }
}