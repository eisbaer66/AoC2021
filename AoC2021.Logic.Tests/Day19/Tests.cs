using NUnit.Framework;
using System.IO;
using System.Linq;
using AoC2021.Logic.BeaconScanner;
using AoC2021.Logic.Utility;

namespace AoC2021.Logic.Tests.Day19
{
    public class Tests
    {
        [Test]
        [TestCase("Day19\\Example.txt", 79)]
        [TestCase("Day19\\Input.txt",   332)]
        public void Part1(string filename, int expectedMagnitude)
        {
            var input = File.ReadAllText(filename);
            var map   = new Map(input);

            var uniqueBeacons = map.FindUniqueBeacons();

            Assert.AreEqual(expectedMagnitude, uniqueBeacons.Count, "filename: " + filename);
        }

        [Test]
        [TestCase("Day19\\Example.txt", 3621)]
        [TestCase("Day19\\Input.txt",   8507)]
        public void Part2(string filename, int expectedMagnitude)
        {
            var input = File.ReadAllText(filename);
            var map   = new Map(input);

            var maxDistance = map.FindScannerCoordinates()
                                 .ToArray()
                                 .CombinationsWithoutPermutation()
                                 .Select(x => x.a.ManhattanDistanceTo(x.b))
                                 .Max();


            Assert.AreEqual(expectedMagnitude, maxDistance, "filename: " + filename);
        }
    }
}