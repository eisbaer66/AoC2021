using System.IO;
using AoC2021.Logic.Populations;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day6
{
    public class Tests
    {
        [Test]
        [TestCase("Day6\\Example.txt", 5934)]
        [TestCase("Day6\\Input.txt",   371379)]
        public void Part1(string filename, long expectedCount)
        {
            var input  = File.ReadAllText(filename);
            var population = new LanternfishPopulation(input);
            population.Tick(80);

            Assert.AreEqual(expectedCount, population.Count);
        }

        [Test]
        [TestCase("Day6\\Example.txt", 26984457539)]
        [TestCase("Day6\\Input.txt",   1674303997472)]
        public void Part2(string filename, long expectedCount)
        {
            var input  = File.ReadAllText(filename);
            var population = new LanternfishPopulation(input);
            population.Tick(256);

            Assert.AreEqual(expectedCount, population.Count);
        }
    }
}