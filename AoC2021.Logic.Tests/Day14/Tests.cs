using System.IO;
using AoC2021.Logic.ExtendedPolymerization;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day14
{
    public class Tests
    {
        [Test]
        [TestCase("Day14\\Example.txt", 1588)]
        [TestCase("Day14\\Input.txt",   2975)]
        public void Part1(string filename, long expectedScore)
        {
            var input  = File.ReadAllText(filename);
            var chunks = new Polymers(input);
            chunks.InsertSteps(10);

            Assert.AreEqual(expectedScore, chunks.GetScore());
        }

        [Test]
        [TestCase("Day14\\Example.txt", 2188189693529)]
        [TestCase("Day14\\Input.txt",   3015383850689)]
        public void Part2(string filename, long expectedScore)
        {
            var input  = File.ReadAllText(filename);
            var polymers = new Polymers(input);
            polymers.InsertSteps(40);

            Assert.AreEqual(expectedScore, polymers.GetScore());
        }
    }

}