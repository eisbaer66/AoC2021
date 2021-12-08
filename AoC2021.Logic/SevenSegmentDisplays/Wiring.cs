using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.SevenSegmentDisplays
{
    public class Wiring
    {
        private readonly Display[] _displays;

        public Wiring(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            _displays = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(line =>
                                     {
                                         var parts = line.Split("|", StringSplitOptions.RemoveEmptyEntries)
                                                         .Select(s => s.Trim())
                                                         .Select(parts => parts
                                                                          .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                                          .Select(x => { return new string(x.OrderBy(s => s).ToArray()); })
                                                                          .ToArray()
                                                                )
                                                         .ToArray();

                                         return new Display(parts[0], parts[1]);
                                     })
                             .ToArray();
        }

        public int CountUniqueDigits()
        {
            return _displays.Sum(display => display.Digits.Count(digit => digit.Length is 2 or 3 or 4 or 7));
        }

        public IEnumerable<int> TranslateDisplays()
        {
            return _displays.Select(display => display.Translate());
        }
    }
}