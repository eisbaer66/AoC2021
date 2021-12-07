using System.IO;
using AoC2021.Logic.CrabSubmarines;
using AoC2021.Logic.CrabSubmarines.FuelConsumptions;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day7
{
    public class Tests
    {
        [Test]
        [TestCase("Day7\\Example.txt", 37)]
        [TestCase("Day7\\Input.txt",   340987)]
        public void Part1(string filename, long expectedFuel)
        {
            var input      = File.ReadAllText(filename);
            var population = new Alignment(input, new LinearFuelConsumption());
            var usedFuel   = population.Align();
            
            Assert.AreEqual(expectedFuel, usedFuel);
        }

        [Test]
        [TestCase("Day7\\Example.txt", 168)]
        [TestCase("Day7\\Input.txt",   96987874)]
        public void Part2(string filename, long expectedFuel)
        {
            var input      = File.ReadAllText(filename);
            var population = new Alignment(input, new RaisingFuelConsumption());
            var usedFuel   = population.Align();
            
            Assert.AreEqual(expectedFuel, usedFuel);
        }
    }
}