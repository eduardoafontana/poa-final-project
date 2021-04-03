using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using Wumpus.Environment;
using Wumpus.Character;

namespace Wumpus.Tests
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
            configuration.CrevassesPosition = new List<int[]>() { new int[] {2, 1} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 1} };

            string matchMessage = "\n\nforet magique : empty" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monster" + "\n" +
                                    "O : portal" + "\n\n" +
                                    "..." + "\n" +
                                    ".MO" + "\n" +
                                    ".V." + "\n\n" +
                                    "  | om |  |" + "\n" +
                                    " om | vt | ltom |" + "\n" +
                                    " vt | om | vt |" + "\n" +
                                    System.Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le E" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le E" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob prend le portal et passe au niveau suivant." + System.Environment.NewLine;

            int level = 1;

            Match partie = new Match(level, configuration, new ForestFactory(), new PlayerFactory());
            int score_global = partie.PlayMatch();
            
            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(52, score_global);
        }

        [Test]
        public void TestExecution_Level1_Disposition2()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {2, 2};
            configuration.PortalPosition = new int[2] {0, 0};
            configuration.CrevassesPosition = new List<int[]>() { new int[] {2, 1} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 1} };

            string matchMessage = "\n\nforet magique : empty" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monster" + "\n" +
                                    "O : portal" + "\n\n" +
                                    "O.." + "\n" +
                                    ".M." + "\n" +
                                    ".V." + "\n\n" +
                                    " lt | om |  |" + "\n" +
                                    " om | vt | om |" + "\n" +
                                    " vt | om | vt |" + "\n" +
                                    System.Environment.NewLine +
                                    "Bob est apparu en case [2,2]" + System.Environment.NewLine +
                                    "Bob va vers W" + System.Environment.NewLine +
                                    "Bob est mort" + System.Environment.NewLine +
                                    "Bob est apparu en case [2,2]" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob va vers W" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le W" + System.Environment.NewLine +
                                    "Bob va vers W" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le N" + System.Environment.NewLine +
                                    "Bob prend le portal et passe au niveau suivant." + System.Environment.NewLine;


            int level = 1;

            Match partie = new Match(level, configuration, new ForestFactory(), new PlayerFactory());
            int score_global = partie.PlayMatch();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(-38, score_global);
        }

        [Test]
        public void TestExecution_Level1_Disposition3()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 1};
            configuration.PortalPosition = new int[2] {2, 0};
            configuration.CrevassesPosition = new List<int[]>() { new int[] {1, 0} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {1, 2} };

            string matchMessage = "\n\nforet magique : empty" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monster" + "\n" +
                                    "O : portal" + "\n\n" +
                                    "..." + "\n" +
                                    "V.M" + "\n" +
                                    "O.." + "\n\n" +
                                    " vt |  | om |" + "\n" +
                                    "  | omvt |  |" + "\n" +
                                    " ltvt |  | om |" + "\n" +
                                    System.Environment.NewLine +
                                    "Bob est apparu en case [0,1]" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers N" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers W" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le W" + System.Environment.NewLine +
                                    "Bob va vers W" + System.Environment.NewLine +
                                    "Bob prend le portal et passe au niveau suivant." + System.Environment.NewLine;

            int level = 1;

            Match partie = new Match(level, configuration, new ForestFactory(), new PlayerFactory());
            int score_global = partie.PlayMatch();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(62, score_global);
        }

        [Test]
        public void TestExecution_Level1_Disposition4_Block()
        {
            ForestConfiguration configuration = new ForestConfiguration();
            configuration.PlayerPosition = new int[2] {0, 0};
            configuration.PortalPosition = new int[2] {2, 2};
            configuration.CrevassesPosition = new List<int[]>() { new int[] {1, 0} };
            configuration.MonstersPosition = new List<int[]>() { new int[] {0, 1} };

            string matchMessage = "\n\nforet magique : empty" + "\n" +
                                    "v : crevasse" + "\n" +
                                    "M : monster" + "\n" +
                                    "O : portal" + "\n\n" +
                                    ".M." + "\n" +
                                    "V.." + "\n" +
                                    "..O" + "\n\n" +
                                    " omvt |  | om |" + "\n" +
                                    "  | omvt |  |" + "\n" +
                                    " vt |  | lt |" + "\n" +
                                    System.Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob est mort" + System.Environment.NewLine +
                                    "Bob est apparu en case [0,0]" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le E" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob va vers S" + System.Environment.NewLine +
                                    "Bob lance une pierre vers le S" + System.Environment.NewLine +
                                    "Bob va vers E" + System.Environment.NewLine +
                                    "Bob prend le portal et passe au niveau suivant." + System.Environment.NewLine;

            int level = 1;

            Match partie = new Match(level, configuration, new ForestFactory(), new PlayerFactory());
            int score_global = partie.PlayMatch();

            Assert.AreEqual(matchMessage, partie.messages);
            Assert.AreEqual(-36, score_global);
        }
    }
}