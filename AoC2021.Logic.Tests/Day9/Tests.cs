using System.IO;
using System.Linq;
using AoC2021.Logic.HeightMaps;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day9
{
    public class Tests
    {
        [Test]
        [TestCase("Day9\\Example.txt", 15)]
        [TestCase("Day9\\Input.txt",   491)]
        public void Part1(string filename, long expectedRiskLevel)
        {
            var input     = File.ReadAllText(filename);
            var wiring    = new HeightMap(input);
            var lowPoints = wiring.LowPoints();

            Assert.AreEqual(expectedRiskLevel, lowPoints.Select(p => p.Value + 1).Sum());
        }

        [Test]
        [TestCase("Day9\\Example.txt",  1134)]
        [TestCase("Day9\\Example2.txt", 499968)]
        [TestCase("Day9\\Input.txt",    1075536)]
        public void Part2(string filename, long expectedRiskLevel)
        {
            var input     = File.ReadAllText(filename);
            var lavaTubes = new HeightMap(input);
            var basinSize = lavaTubes.FindBasinSizes()
                                     .OrderByDescending(s => s)
                                     .Take(3);

            Assert.AreEqual(expectedRiskLevel, basinSize.Aggregate(1, (product, value) => product * value));
        }
    }
}