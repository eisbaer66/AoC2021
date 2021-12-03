using System.IO;
using AoC2021.Logic.Sensors;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day1
{
    public class Tests
    {
        [Test]
        [TestCase("Day1\\Example.txt", 7)]
        [TestCase("Day1\\Input.txt",   1564)]
        public void Part1(string filename, int expectedSlope)
        {
            var input      = File.ReadAllText(filename);
            var sonarSweep = new SonarSweep();
            sonarSweep.Load(input);

            Assert.AreEqual(expectedSlope, sonarSweep.GetSlope(), "incorrect slope");
        }

        [Test]
        [TestCase("Day1\\Example.txt", 5)]
        [TestCase("Day1\\Input.txt",   1611)]
        public void Part2(string filename, int expectedSlope)
        {
            var input      = File.ReadAllText(filename);
            var sonarSweep = new SonarSweep();
            sonarSweep.Load(input);

            Assert.AreEqual(expectedSlope, sonarSweep.GetSlidingWindowSlope(3), "incorrect sliding window slope");
        }
    }
}