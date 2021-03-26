// using System;
// using System.IO;
// using NUnit.Framework;
// using Wumpus;

// namespace Wumpus.Tests
// {
//     public class EngineOutput
//     {
//         StringWriter sw = new StringWriter();

//         [SetUp]
//         public void Setup()
//         {
//             Console.SetOut(sw);
//         }

//         [Test]
//         public void Test1()
//         {
//             Engine engine = new Engine();
//             engine.Start();

//             string expected = string.Format("Hello World!{0}", Environment.NewLine);

//             Assert.AreEqual(expected, sw.ToString());
//         }
//     }
// }