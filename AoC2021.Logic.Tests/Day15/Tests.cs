using System.IO;
using System.Linq;
using AoC2021.Logic.Chiton;
using AoC2021.Logic.Chiton.MapExpanders;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day15
{
    public class Tests
    {
        [Test]
        [TestCase("Day15\\Example.txt", 40)]
        [TestCase("Day15\\Input.txt",   472)]
        public void Part1(string filename, long expectedScore)
        {
            var input  = File.ReadAllText(filename);
            var chunks = new Maze(input, new NullExpander());
            var path   = chunks.FindPath(new Coordinate(0, 0), chunks.MaxCoord);

            Assert.AreEqual(expectedScore, path.Skip(1).Select(n => n.Risk).Sum());
        }

        [Test]
        [TestCase("Day15\\Example.txt", 315)]
        [TestCase("Day15\\Input.txt",   2851)]
        public void Part2(string filename, long expectedScore)
        {
            var input  = File.ReadAllText(filename);
            var chunks = new Maze(input, new TimesFiveExpander());
            var path   = chunks.FindPath(new Coordinate(0, 0), chunks.MaxCoord);

            Assert.AreEqual(expectedScore, path.Skip(1).Select(n => n.Risk).Sum());
        }
    }
}