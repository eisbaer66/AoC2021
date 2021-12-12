using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day11
{
    public class Tests
    {
        [Test]
        [TestCase("Day11\\Example.txt", 1656)]
        [TestCase("Day11\\Input.txt",   1613)]
        public void Part1(string filename, long expectedScore)
        {
            var input     = File.ReadAllText(filename);
            var chunks    = new OctopusFlashes(input);
            var octopuses = chunks.NextTicks(100);

            Assert.AreEqual(expectedScore, octopuses.Sum(o => o.CountFlashes()));
        }

        [Test]
        [TestCase("Day11\\Example.txt", 195)]
        [TestCase("Day11\\Input.txt",   510)]
        public void Part2(string filename, long expectedScore)
        {
            var input  = File.ReadAllText(filename);
            var chunks = new OctopusFlashes(input);
            var octopuses  = chunks.TickUntilSync();

            Assert.AreEqual(expectedScore, octopuses.Tick);
        }
    }
}