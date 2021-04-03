using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using Wumpus.Environment;
using Wumpus.Character;

namespace Wumpus.Tests
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
            configuration.CrevassesPosition = new List<int[]>() { new int[] {0, 0}, new int[] {2, 2} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 2}, new int[] {2, 3}, new int[] {3, 1} };

            string matchMessage = "\n\nforet magique : empty" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monster" + "\n" +
                                    "O : portal" + "\n\n" +
                                    "V..." + "\n" +
                                    "O.M." + "\n" +
                                    "..VM" + "\n" +
                                    ".M.." + "\n\n" +
                                    "  | vt | om |  |" + "\n" +
                                    " ltvt | om | vt | om |" + "\n" +
                                    "  | omvt | om | vt |" + "\n" +
                                    " om |  | omvt | om |" + "\n" +
                                    System.Environment.NewLine +
                                    "Bob est apparu en case [0,1]" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le E" + System.Environment.NewLine +
                                    "Bob va vers W" + System.Environment.NewLine +
                                    "Bob va vers W" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le W" + System.Environment.NewLine +
                                    "Bob prend le portal et passe au niveau suivant." + System.Environment.NewLine;

            int level = 2;

            Match partie = new Match(level, configuration, new ForestFactory(), new PlayerFactory());
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
            configuration.CrevassesPosition = new List<int[]>() { new int[] {2, 3}, new int[] {2, 2} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 2}, new int[] {1, 1}, new int[] {3, 2} };

            string matchMessage = "\n\nforet magique : empty" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monster" + "\n" +
                                    "O : portal" + "\n\n" +
                                    "...." + "\n" +
                                    ".MM." + "\n" +
                                    "..VV" + "\n" +
                                    "..MO" + "\n\n" +
                                    "  | om | om |  |" + "\n" +
                                    " om | om | omvt | omvt |" + "\n" +
                                    "  | omvt | omvt | vt |" + "\n" +
                                    "  | om | vt | ltomvt |" + "\n" +
                                    System.Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le E" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob est mort" + System.Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le E" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le N" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le E" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob est mort" + System.Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob prend le portal et passe au niveau suivant." + System.Environment.NewLine;

            int level = 2;

            Match partie = new Match(level, configuration, new ForestFactory(), new PlayerFactory());
            int score_global = partie.PlayMatch();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(-252, score_global);
        }
    }
}