using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AoC2021.Logic.SyntaxScoring;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day10
{
    public class Tests
    {
        [Test]
        [TestCase("Day10\\Example.txt", 26397)]
        [TestCase("Day10\\Input.txt",   436497)]
        public void Part1(string filename, long expectedScore)
        {
            var input     = File.ReadAllText(filename);
            var chunks    = new Chunks(input);
            var corruptionScore = chunks.FindCorruptionScore();

            Assert.AreEqual(expectedScore, corruptionScore);
        }

        [Test]
        [TestCase("Day10\\Example.txt", 288957)]
        [TestCase("Day10\\Input.txt",   2377613374)]
        public void Part2(string filename, long expectedScore)
        {
            var input     = File.ReadAllText(filename);
            var chunks    = new Chunks(input);
            var autoCompleteScore = chunks.FindAutoCompleteScore();

            Assert.AreEqual(expectedScore, autoCompleteScore);
        }
    }
}