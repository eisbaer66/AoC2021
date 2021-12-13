using System.Diagnostics;
using System.IO;
using AoC2021.Logic.TransparentOrigami;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day13
{
    public class Tests
    {
        [Test]
        [TestCase("Day13\\Example.txt", 17)]
        [TestCase("Day13\\Input.txt",   684)]
        public void Part1(string filename, long expectedScore)
        {
            var input     = File.ReadAllText(filename);
            var chunks    = new Sheet(input);
            chunks.ExecuteOneFold();

            Assert.AreEqual(expectedScore, chunks.DotCount);
        }

        [Test]
        [TestCase("Day13\\Example.txt", @"#####
#...#
#...#
#...#
#####
")]
        [TestCase("Day13\\Input.txt", @"..##.###..####.###..#.....##..#..#.#..#
...#.#..#....#.#..#.#....#..#.#.#..#..#
...#.#..#...#..###..#....#....##...####
...#.###...#...#..#.#....#.##.#.#..#..#
#..#.#.#..#....#..#.#....#..#.#.#..#..#
.##..#..#.####.###..####..###.#..#.#..#
")]
        public void Part2(string filename, string expectedPattern)
        {
            var input     = File.ReadAllText(filename);
            var sheet    = new Sheet(input);
            sheet.ExecuteFolds();

            var dotPattern = sheet.GetDotPattern();

            Assert.AreEqual(expectedPattern, dotPattern);
        }
    }
}