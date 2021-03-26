using System;
using NUnit.Framework;
using Wumpus;
using System.Linq;
using System.Collections.Generic;

namespace tests
{
    public class MatchExecutionLevel2
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestExecution_Level2_Disposition1()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 1};
            configuration.PortalPosition = new int[2] {1, 0};
            configuration.CavesPosition = new List<int[]>() { new int[] {0, 0}, new int[] {2, 2} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 2}, new int[] {2, 3}, new int[] {3, 1} };

            string matchMessage = "\n\nforet magique : vide" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monstre" + "\n" +
                                    "O : portail" + "\n\n" +
                                    "V..." + "\n" +
                                    "O.M." + "\n" +
                                    "..VM" + "\n" +
                                    ".M.." + "\n\n" +
                                    "  | vt | om |  |" + "\n" +
                                    " ltvt | om | vt | om |" + "\n" +
                                    "  | omvt | om | vt |" + "\n" +
                                    " om |  | omvt | om |" + "\n" +
                                    Environment.NewLine +
                                    "Bob est apparu en case [0,1]" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob lance une pierre vers le E" + Environment.NewLine +
                                    "Bob va vers W" + Environment.NewLine +
                                    "Bob va vers W" + Environment.NewLine +
                                    "Bob lance une pierre vers le W" + Environment.NewLine +
                                    "Bob prend le portail et passe au niveau suivant." + Environment.NewLine;

            int level = 2;

            Match partie = new Match(level, configuration);
            int score_global = partie.PlayMatch();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(123, score_global);
        }

        [Test]
        public void TestExecution_Level2_Disposition2_Block()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 0};
            configuration.PortalPosition = new int[2] {3, 3};
            configuration.CavesPosition = new List<int[]>() { new int[] {2, 3}, new int[] {2, 2} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 2}, new int[] {1, 1}, new int[] {3, 2} };

            string matchMessage = "\n\nforet magique : vide" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monstre" + "\n" +
                                    "O : portail" + "\n\n" +
                                    "...." + "\n" +
                                    ".MM." + "\n" +
                                    "..VV" + "\n" +
                                    "..MO" + "\n\n" +
                                    "  | om | om |  |" + "\n" +
                                    " om | om | omvt | omvt |" + "\n" +
                                    "  | omvt | omvt | vt |" + "\n" +
                                    "  | om | vt | ltomvt |" + "\n" +
                                    Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob lance une pierre vers le E" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob est mort" + Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob lance une pierre vers le E" + Environment.NewLine +
                                    "Bob va vers N" + Environment.NewLine +
                                    "Bob lance une pierre vers le N" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob lance une pierre vers le E" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob lance une pierre vers le S" + Environment.NewLine +
                                    "Bob est mort" + Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers S" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob va vers E" + Environment.NewLine +
                                    "Bob prend le portail et passe au niveau suivant." + Environment.NewLine;

            int level = 2;

            Match partie = new Match(level, configuration);
            int score_global = partie.PlayMatch();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(-252, score_global);
        }
    }
}