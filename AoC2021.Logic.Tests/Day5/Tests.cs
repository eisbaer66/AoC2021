using System.IO;
using AoC2021.Logic.HydrothermalVentsMapping;
using AoC2021.Logic.HydrothermalVentsMapping.LineGenerators;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day5
{
    public class Tests
    {
        [Test]
        [TestCase("Day5\\Example.txt", 5)]
        [TestCase("Day5\\Input.txt",   6005)]
        public void Part1(string filename, int expectedCount)
        {
            var input  = File.ReadAllText(filename);
            var vents = new HydrothermalVents(input, new HorizontalAndVerticalLineGenerator());
            var count = vents.CountGreaterThen(2);

            Assert.AreEqual(expectedCount, count);
        }

        [Test]
        [TestCase("Day5\\Example.txt", 12)]
        [TestCase("Day5\\Input.txt",   23864)]
        public void Part2(string filename, int expectedCount)
        {
            var input  = File.ReadAllText(filename);
            var vents = new HydrothermalVents(input, new HorizontalVerticalAndDiagonalLineGenerator());
            var count = vents.CountGreaterThen(2);

            Assert.AreEqual(expectedCount, count);
        }
    }
}