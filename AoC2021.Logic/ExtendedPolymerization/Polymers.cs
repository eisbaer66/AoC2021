using System;
using System.Collections.Generic;
using System.Linq;
using AoC2021.Logic.Utility;

namespace AoC2021.Logic.ExtendedPolymerization
{
    public class Polymers
    {
        private readonly string                     _lastChar;
        private readonly Dictionary<string, string> _pairs;
        private          List<PairOccurrence>       _occurrences;

        public Polymers(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .ToArray();

            _lastChar = new string(new[] { lines[0][^1] });
            _pairs = lines.Skip(1)
                          .Select(line => line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries))
                          .ToDictionary(x => x[0], x => x[1]);
            _occurrences = _pairs
                           .Select(x => new PairOccurrence(x.Key, lines[0].IndexOfAll(x.Key).Count()))
                           .ToList();
        }

        public long GetScore()
        {
            var occurrences = _occurrences.Concat(new[] { new PairOccurrence(_lastChar, 1) })
                                          .GroupBy(x => x.Key[0], x => x.Occurrence)
                                          .Select(g => g.Sum())
                                          .OrderBy(x => x)
                                          .ToArray();
            var leastCommon = occurrences.First();
            var mostCommon  = occurrences.Last();

            return mostCommon - leastCommon;
        }

        public void InsertSteps(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _occurrences = _occurrences.Where(o => o.Occurrence > 0)
                                           .SelectMany(Insert)
                                           .GroupBy(x => x.Key)
                                           .Select(g => new PairOccurrence(g.Key, g.Select(x => x.Occurrence).Sum()))
                                           .ToList();
            }
        }

        private IEnumerable<PairOccurrence> Insert(PairOccurrence occurrence)
        {
            if (occurrence.Occurrence == 0)
            {
                yield return occurrence;
                yield break;
            }

            var insert   = _pairs[occurrence.Key];
            var leftKey  = occurrence.Key[0] + insert;
            var rightKey = insert            + occurrence.Key[1];

            yield return new PairOccurrence(leftKey,  occurrence.Occurrence);
            yield return new PairOccurrence(rightKey, occurrence.Occurrence);
        }
    }
}