using System.IO;
using System.Linq;
using AoC2021.Logic.BITS.Parsing;
using AoC2021.Logic.BITS.Packets;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day16
{
    public class Tests
    {
        [Test]
        [TestCase("Day16\\Example.txt",  6)]
        [TestCase("Day16\\Example2.txt", 9)]
        [TestCase("Day16\\Example3.txt", 14)]
        [TestCase("Day16\\Example4.txt", 16)]
        [TestCase("Day16\\Example5.txt", 12)]
        [TestCase("Day16\\Example6.txt", 23)]
        [TestCase("Day16\\Example7.txt", 31)]
        [TestCase("Day16\\Input.txt",    991)]
        public void Part1(string filename, long expectedVersionSum)
        {
            var input  = File.ReadAllText(filename);
            var parser = new Parser(input);
            var packet = parser.ReadPacket();
            
            var versionSum = SumVersion(packet);

            Assert.AreEqual(expectedVersionSum, versionSum);
        }

        private int SumVersion(PacketBase packet)
        {
            return packet.Version + packet.SubPackets.Sum(SumVersion);
        }

        [Test]
        [TestCase("Day16\\ExamplePart2.txt",   3)]
        [TestCase("Day16\\ExamplePart2_2.txt", 54)]
        [TestCase("Day16\\ExamplePart2_3.txt", 7)]
        [TestCase("Day16\\ExamplePart2_4.txt", 9)]
        [TestCase("Day16\\ExamplePart2_5.txt", 1)]
        [TestCase("Day16\\ExamplePart2_6.txt", 0)]
        [TestCase("Day16\\ExamplePart2_7.txt", 0)]
        [TestCase("Day16\\ExamplePart2_8.txt", 1)]
        [TestCase("Day16\\Input.txt",          1264485568252)]
        public void Part2(string filename, long expectedResult)
        {
            var input  = File.ReadAllText(filename);
            var parser = new Parser(input);
            var packet = parser.ReadPacket();

            Assert.AreEqual(expectedResult, packet.Execute());
        }
    }
}