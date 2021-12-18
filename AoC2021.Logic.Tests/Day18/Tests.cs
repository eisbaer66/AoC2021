using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using AoC2021.Logic.Snailfish;

namespace AoC2021.Logic.Tests.Day18
{
    public class Tests
    {
        [Test]
        [TestCase("Day18\\ExplodeExamples.txt")]
        public void ExplodeTests(string filename)
        {
            var values = File.ReadAllText(filename)
                             .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(line =>
                                     {
                                         var segments = line.Split(" : ", StringSplitOptions.RemoveEmptyEntries);
                                         return (input: segments[0], expectedResult: segments[1]);
                                     });
            foreach (var (input, expectedResult) in values)
            {
                var number = Number.Parse(input);
                Assert.AreEqual(input, number.ToString(), "parse failed");

                number.ReduceOnce();
                Assert.AreEqual(expectedResult, number.ToString(), "original: " + input);
            }
        }

        [Test]
        [TestCase("[[[[0,7],4],[15,[0,13]]],[1,1]]",    "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")]
        [TestCase("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")]
        public void SplitTests(string input, string expectedOutput)
        {
            var number = Number.Parse(input);
            Assert.AreEqual(input, number.ToString(), "parse failed");

            number.ReduceOnce();
            Assert.AreEqual(expectedOutput, number.ToString(), "original: " + input);
        }

        [Test]
        [TestCase("[[[[4,3],4],4],[7,[[8,4],9]]]",             "[1,1]",                                             "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
        [TestCase("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]", "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",                 "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
        [TestCase("[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",     "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]", "[[[[7,8],[6,6]],[[6,0],[7,7]]],[[[7,8],[8,8]],[[7,9],[0,6]]]]")]
        public void AdditionTests(string aRaw, string bRaw, string expectedResult)
        {
            var a = Number.Parse(aRaw);
            Assert.AreEqual(aRaw, a.ToString(), "parse failed on a");
            var b = Number.Parse(bRaw);
            Assert.AreEqual(bRaw, b.ToString(), "parse failed on b");

            var sum = a.Add(b);
            Assert.AreEqual(expectedResult, sum.ToString(), "original a: " + aRaw + " b: " + bRaw);
        }

        [Test]
        [TestCase("Day18\\AdditionExample.txt",  "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
        [TestCase("Day18\\AdditionExample2.txt", "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
        [TestCase("Day18\\AdditionExample3.txt", "[[[[5,0],[7,4]],[5,5]],[6,6]]")]
        [TestCase("Day18\\AdditionExample4.txt", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
        public void AdditionsTests(string filename, string expectedResult)
        {
            var sum = File.ReadAllText(filename)
                          .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(Number.Parse)
                          .Aggregate((a, b) => a.Add(b));

            Assert.AreEqual(expectedResult, sum!.ToString(), "filename: " + filename);
        }

        [Test]
        [TestCase("[9,1]",                                                 29)]
        [TestCase("[[9,1],[1,9]]",                                         129)]
        [TestCase("[[1,2],[[3,4],5]]",                                     143)]
        [TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]",                     1384)]
        [TestCase("[[[[1,1],[2,2]],[3,3]],[4,4]]",                         445)]
        [TestCase("[[[[3,0],[5,3]],[4,4]],[5,5]]",                         791)]
        [TestCase("[[[[5,0],[7,4]],[5,5]],[6,6]]",                         1137)]
        [TestCase("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
        public void MagnitudeTests(string input, int expectedMagnitude)
        {
            var magnitude = Number.Parse(input).GetMagnitude();
            Assert.AreEqual(expectedMagnitude, magnitude, "original: " + input);
        }

        [Test]
        [TestCase("Day18\\MagnitudeExample.txt", "[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]", 4140)]
        public void SumMagnitudeTests(string filename, string expectedResult, int expectedMagnitude)
        {
            var sum = File.ReadAllText(filename)
                          .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(Number.Parse)
                          .Aggregate((a, b) => a.Add(b));

            Assert.AreEqual(expectedResult,    sum!.ToString(),     "filename: " + filename);
            Assert.AreEqual(expectedMagnitude, sum!.GetMagnitude(), "filename: " + filename);
        }

        [Test]
        [TestCase("Day18\\MagnitudeExample.txt", 4140)]
        [TestCase("Day18\\Input.txt",            2907)]
        public void Part1(string filename, int expectedMagnitude)
        {
            var sum = File.ReadAllText(filename)
                          .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(Number.Parse)
                          .Aggregate((a, b) => a.Add(b));

            Assert.AreEqual(expectedMagnitude, sum!.GetMagnitude(), "filename: " + filename);
        }

        [Test]
        [TestCase("Day18\\MagnitudeExample.txt", 3993)]
        [TestCase("Day18\\Input.txt",            4690)]
        public void Part2(string filename, int expectedMagnitude)
        {
            var values = File.ReadAllText(filename)
                             .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(Number.Parse)
                             .ToArray();

            var maxMagnitude = values.SelectMany(a => values.Where(v => v != a)
                                                            .Select(b => (a, b)))
                                     .Select(x => (a: x.a.Copy(), b: x.b.Copy()))
                                     .Select(x => x.a.Add(x.b))
                                     .Select(sum => sum.GetMagnitude())
                                     .Max();

            Assert.AreEqual(expectedMagnitude, maxMagnitude, "filename: " + filename);
        }
    }
}