using System.IO;
using System.Linq;
using AoC2021.Logic.SevenSegmentDisplays;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day8
{
    public class Tests
    {
        [Test]
        [TestCase("Day8\\Example_small.txt", 0)]
        [TestCase("Day8\\Example.txt",       26)]
        [TestCase("Day8\\Input.txt",         539)]
        public void Part1(string filename, long expectedUniqueDigits)
        {
            var input        = File.ReadAllText(filename);
            var wiring       = new Wiring(input);
            var uniqueDigits = wiring.CountUniqueDigits();

            Assert.AreEqual(expectedUniqueDigits, uniqueDigits);
        }

        [Test]
        [TestCase("Day8\\Example_small.txt", 5353)]
        [TestCase("Day8\\Example.txt",       61229)]
        [TestCase("Day8\\Input.txt",         1084606)]
        public void Part2(string filename, long expectedDisplaySum)
        {
            var input    = File.ReadAllText(filename);
            var wiring   = new Wiring(input);
            var displays = wiring.TranslateDisplays();

            Assert.AreEqual(expectedDisplaySum, displays.Sum());
        }
    }
}