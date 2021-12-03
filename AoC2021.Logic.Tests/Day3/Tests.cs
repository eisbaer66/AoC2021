using System.IO;
using AoC2021.Logic.Diagnostics;
using NUnit.Framework;

namespace AoC2021.Logic.Tests.Day3
{
    public class Tests
    {
        [Test]
        [TestCase("Day3\\Example.txt", 198)]
        [TestCase("Day3\\Input.txt",   2743844)]
        public void Part1(string filename, int expectedPowerConsumption)
        {
            var input  = File.ReadAllText(filename);
            var diagnosticReport = new DiagnosticReport(input);
            var report = diagnosticReport.CalculatePowerConsumption();

            Assert.AreEqual(expectedPowerConsumption, report.PowerConsumption);
        }

        [Test]
        [TestCase("Day3\\Example.txt", 230)]
        [TestCase("Day3\\Input.txt",   6677951)]
        public void Part2(string filename, int expectedLifeSupportRating)
        {
            var input  = File.ReadAllText(filename);
            var diagnosticReport = new DiagnosticReport(input);
            var report = diagnosticReport.CalculateLifeSupportRating();
            
            Assert.AreEqual(expectedLifeSupportRating, report.LifeSupportRating);
        }
    }
}