using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.Diagnostics
{
    public class DiagnosticReport
    {
        private readonly bool[][] _words;

        public DiagnosticReport(string input)
        {
            _words = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(line => line.Select(c => c != '0').ToArray())
                          .ToArray();
        }

        public PowerConsumptionReport CalculatePowerConsumption()
        {
            var wordLength  = _words[0].Length;
            var gammaRate   = new bool[wordLength];
            var epsilonRate = new bool[wordLength];
            for (var i = 0; i < wordLength; i++)
            {
                var moreZeros = MoreZerosAtIndex(_words, i);

                gammaRate[i]   = !moreZeros;
                epsilonRate[i] = moreZeros;
            }

            return new PowerConsumptionReport
                   {
                       GammaRate   = ToInt(gammaRate),
                       EpsilonRate = ToInt(epsilonRate),
                   };
        }

        public LifeSupportReport CalculateLifeSupportRating()
        {
            var oxy = FilterData(moreZeros => !moreZeros);
            var co2 = FilterData(moreZeros => moreZeros);

            return new LifeSupportReport
                   {
                       OxygenGeneratorRating = ToInt(oxy),
                       Co2ScrubberRating     = ToInt(co2),
                   };
        }

        private bool[] FilterData(Func<bool, bool> lookingForSelector)
        {
            var words = _words;
            for (var i = 0; i < _words[0].Length; i++)
            {
                var moreZeros  = MoreZerosAtIndex(words, i);
                var lookingFor = lookingForSelector(moreZeros);

                words = words.Where(line => line[i] == lookingFor).ToArray();
                if (words.Length == 1)
                    return words[0];
            }

            throw new InvalidOperationException("could not find a single matching word");
        }

        private static bool MoreZerosAtIndex(IEnumerable<bool[]> words, int index)
        {
            var zeros = 0;
            var ones  = 0;
            foreach (var word in words)
            {
                if (!word[index])
                    ++zeros;
                else
                    ++ones;
            }

            return zeros > ones;
        }

        private static int ToInt(IReadOnlyList<bool> word)
        {
            var number = 0;

            for (var i = 0; i < word.Count; i++)
            {
                if (!word[i])
                    continue;

                number |= 1 << (word.Count - i - 1);
            }

            return number;
        }
    }
}