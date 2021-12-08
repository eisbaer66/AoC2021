using System;
using System.Linq;

namespace AoC2021.Logic.SevenSegmentDisplays
{
    internal record Display
    {
        public Display(string[] wires, string[] digits)
        {
            Wires  = wires;
            Digits = digits;
        }

        public string[] Wires  { get; init; }
        public string[] Digits { get; init; }

        public void Deconstruct(out string[] wires, out string[] digits)
        {
            wires  = Wires;
            digits = Digits;
        }

        /*
         * 0: ABC EFG   6
         * 1:   C  F    2   unique
         * 2: A CDE G   5
         * 3: A CD FG   5
         * 4:  BCD F    4   unique
         * 5: AB D FG   5
         * 6: AB DEFG   6
         * 7: A C  F    3   unique
         * 8: ABCDEFG   7   unique
         * 9: ABCD FG   6
         *
         * by length:
         * 1:   C  F    2   unique
         * 7: A C  F    3   unique
         * 4:  BCD F    4   unique
         * 2: A CDE G   5
         * 3: A CD FG   5
         * 5: AB D FG   5
         * 0: ABC EFG   6
         * 6: AB DEFG   6
         * 9: ABCD FG   6
         * 8: ABCDEFG   7   unique
         */
        public int Translate()
        {
            var lengthDict   = Wires.ToLookup(w => w.Length, w => w);
            var onePattern   = lengthDict[2].Single();
            var fourPattern  = lengthDict[4].Single();
            var sevenPattern = lengthDict[3].Single();
            var eightPattern = lengthDict[7].Single();
            var ninePattern  = lengthDict[6].Single(w => fourPattern.All(w.Contains));
            var zeroPattern  = lengthDict[6].Single(w => onePattern.All(w.Contains)   && !ninePattern.All(w.Contains));
            var sixPattern   = lengthDict[6].Single(w => !zeroPattern.All(w.Contains) && !ninePattern.All(w.Contains));
            var threePattern = lengthDict[5].Single(w => onePattern.All(w.Contains));
            var fivePattern  = lengthDict[5].Single(w => w.All(sixPattern.Contains));
            var twoPattern   = lengthDict[5].Single(w => !threePattern.All(w.Contains) && !fivePattern.All(w.Contains));

            var patterns = new[]
                           {
                               zeroPattern,
                               onePattern,
                               twoPattern,
                               threePattern,
                               fourPattern,
                               fivePattern,
                               sixPattern,
                               sevenPattern,
                               eightPattern,
                               ninePattern
                           }
                           .Select((p, i) => (p, i))
                           .ToDictionary(x => x.p, x => x.i);

            return Digits
                   .Select((digit, index) =>
                           {
                               var factor = (int)Math.Pow(10, Digits.Length - index - 1);
                               return patterns[digit] * factor;
                           })
                   .Sum();
        }
    }
}