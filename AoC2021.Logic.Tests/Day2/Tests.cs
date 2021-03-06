using System.IO;
using AoC2021.Logic.Routeing;
using NUnit.Framework;
using StepFactory = AoC2021.Logic.Routeing.Part1.StepFactory;

namespace AoC2021.Logic.Tests.Day2
{
    public class Tests
    {
        [Test]
        [TestCase("Day2\\Example.txt", 150)]
        [TestCase("Day2\\Input.txt",   2322630)]
        public void Part1(string filename, int expectedSlope)
        {
            TestRoute(filename, expectedSlope, new StepFactory());
        }

        [Test]
        [TestCase("Day2\\Example.txt", 900)]
        [TestCase("Day2\\Input.txt",   2105273490)]
        public void Part2(string filename, int expectedSlope)
        {
            TestRoute(filename, expectedSlope, new Routeing.StepFactory());
        }

        private static void TestRoute(string filename, int expectedSlope, IStepFactory stepFactory)
        {
            var input    = File.ReadAllText(filename);
            var route    = new Route(stepFactory);
            var position = route.Follow(input);

            Assert.AreEqual(expectedSlope, position.HorizontalPosition * position.Depth, "incorrect position/depth");
        }
    }
}