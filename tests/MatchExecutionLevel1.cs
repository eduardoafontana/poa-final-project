using System;
using NUnit.Framework;
using Wumpus;
using System.Linq;
using System.Collections.Generic;

namespace tests
{
    public class MatchExecutionLevel1
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestExecution_Level1_Disposition1()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 0};
            configuration.PortalPosition = new int[2] {1, 2};
            configuration.CavesPosition = new List<int[]>() { new int[] {2, 1} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 1} };

            string matchMessage = "\n\nforet magique : vide" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monstre" + "\n" +
                                    "O : portail" + "\n\n" +
                                    "..." + "\n" +
                                    ".MO" + "\n" +
                                    ".V." + "\n\n" +
                                    "  | om |  |" + "\n" +
                                    " om | vt | ltom |" + "\n" +
                                    " vt | om | vt |" + "\n" +
                                    Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob lance une pierre vers le E" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob lance une pierre vers le E" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob prend le portail et passe au niveau suivant." + Environment.NewLine;

            int level = 1;

            Partie partie = new Partie(level, configuration);
            int score_global = partie.Jouer();
            
            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(52, score_global);
        }

        [Test]
        public void TestExecution_Level1_Disposition2()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {2, 2};
            configuration.PortalPosition = new int[2] {0, 0};
            configuration.CavesPosition = new List<int[]>() { new int[] {2, 1} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 1} };

            string matchMessage = "\n\nforet magique : vide" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monstre" + "\n" +
                                    "O : portail" + "\n\n" +
                                    "O.." + "\n" +
                                    ".M." + "\n" +
                                    ".V." + "\n\n" +
                                    " lt | om |  |" + "\n" +
                                    " om | vt | om |" + "\n" +
                                    " vt | om | vt |" + "\n" +
                                    Environment.NewLine +
                                    "Bob est apparu en case [2,2]" + Environment.NewLine +
                                    "Bob va vers W" + Environment.NewLine +
                                    "Bob est mort" + Environment.NewLine +
                                    "Bob est apparu en case [2,2]" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob va vers W" + Environment.NewLine +
                                    "Bob lance une pierre vers le W" + Environment.NewLine +
                                    "Bob va vers W" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob lance une pierre vers le N" + Environment.NewLine +
                                    "Bob prend le portail et passe au niveau suivant." + Environment.NewLine;


            int level = 1;

            Partie partie = new Partie(level, configuration);
            int score_global = partie.Jouer();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(-38, score_global);
        }

        [Test]
        public void TestExecution_Level1_Disposition3()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 1};
            configuration.PortalPosition = new int[2] {2, 0};
            configuration.CavesPosition = new List<int[]>() { new int[] {1, 0} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 2} };

            string matchMessage = "\n\nforet magique : vide" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monstre" + "\n" +
                                    "O : portail" + "\n\n" +
                                    "..." + "\n" +
                                    "V.M" + "\n" +
                                    "O.." + "\n\n" +
                                    " vt |  | om |" + "\n" +
                                    "  | omvt |  |" + "\n" +
                                    " ltvt |  | om |" + "\n" +
                                    Environment.NewLine +
                                    "Bob est apparu en case [0,1]" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers W" + Environment.NewLine +
                                    "Bob lance une pierre vers le W" + Environment.NewLine +
                                    "Bob va vers W" + Environment.NewLine +
                                    "Bob prend le portail et passe au niveau suivant." + Environment.NewLine;

            int level = 1;

            Partie partie = new Partie(level, configuration);
            int score_global = partie.Jouer();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(62, score_global);
        }

        [Test]
        public void TestExecution_Level1_Disposition4_Block()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 0};
            configuration.PortalPosition = new int[2] {2, 2};
            configuration.CavesPosition = new List<int[]>() { new int[] {1, 0} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {0, 1} };

            string matchMessage = "\n\nforet magique : vide" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monstre" + "\n" +
                                    "O : portail" + "\n\n" +
                                    ".M." + "\n" +
                                    "V.." + "\n" +
                                    "..O" + "\n\n" +
                                    " omvt |  | om |" + "\n" +
                                    "  | omvt |  |" + "\n" +
                                    " vt |  | lt |" + "\n" +
                                    Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob est mort" + Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob lance une pierre vers le E" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob prend le portail et passe au niveau suivant." + Environment.NewLine;

            int level = 1;

            Partie partie = new Partie(level, configuration);
            int score_global = partie.Jouer();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(-36, score_global);
        }
    }
}