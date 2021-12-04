using System.IO;
using AoC2021.Logic.BingoGame;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day4
{
    public class Tests
    {
        [Test]
        [TestCase("Day4\\Example.txt", 4512)]
        [TestCase("Day4\\Input.txt",   51776)]
        public void Part1(string filename, int expectedFinalScore)
        {
            var input  = File.ReadAllText(filename);
            var bingo = new Bingo(input);
            var board = bingo.FindFirstWinningBoard();

            Assert.AreEqual(expectedFinalScore, board.GetFinalScore());
        }

        [Test]
        [TestCase("Day4\\Example.txt", 1924)]
        [TestCase("Day4\\Input.txt",   16830)]
        public void Part2(string filename, int expectedFinalScore)
        {
            var input  = File.ReadAllText(filename);
            var bingo = new Bingo(input);
            var board = bingo.FindLastWinningBoard();

            Assert.AreEqual(expectedFinalScore, board.GetFinalScore());
        }
    }
}