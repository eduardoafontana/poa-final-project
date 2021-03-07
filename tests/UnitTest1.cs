using NUnit.Framework;
using Wumpus;

namespace tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Case caseVar = new Case("", "");

            Assert.Pass();
        }
    }
}